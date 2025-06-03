using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEditor.ProjectWindowCallback;

public class BuildingSystem : MonoBehaviour
{

    public bool useLines;
    public GameObject building;
    public GameObject line;
    public GameObject lineBase;
    public Tilemap tempTm;
    public Tilemap mainTm;
    public GameObject mouse;

    private Vector3 mousePos;
    private Vector3Int cellPos;
    private GridLayout grid;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0f);
    private Dictionary<TileType, Tile> buildTileDic = new Dictionary<TileType, Tile>();
    private Dictionary<BuildingType, GameObject> buildingTileDic = new Dictionary<BuildingType, GameObject>();
    private Vector3Int oldCellPos;

    private SpriteRenderer mouseRen;
    private SpriteRenderer buildingRen;
    private List<Vector3Int> points = new List<Vector3Int>();


    void Start()
    {
        grid = this.GetComponent<GridLayout>();
        mouseRen = mouse.GetComponent<SpriteRenderer>();
        buildingRen = building.GetComponent<SpriteRenderer>();

        mouseRen.sprite = buildingRen.sprite;
        mouseRen.color = buildingRen.color;

        buildTileDic.Add(TileType.Empty, null);
        buildTileDic.Add(TileType.White, Resources.Load<Tile>("palette/build/white"));
        buildTileDic.Add(TileType.Red, Resources.Load<Tile>("palette/build/red"));
        buildTileDic.Add(TileType.Green, Resources.Load<Tile>("palette/build/green"));
        buildTileDic.Add(TileType.Road, Resources.Load<Tile>("palette/build/road"));

        buildingTileDic.Add(BuildingType.Build, building);
        buildingTileDic.Add(BuildingType.Line, line);
        buildingTileDic.Add(BuildingType.LineBase, lineBase);
    }

    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = grid.WorldToCell(mousePos);

        // visual
        if (oldCellPos != cellPos)
        {
            if (IsPlaceble())
            {
                tempTm.SetTile(cellPos, buildTileDic[TileType.Green]);
            }
            else
            {
                tempTm.SetTile(cellPos, buildTileDic[TileType.Red]);
            }

            tempTm.SetTile(oldCellPos, buildTileDic[TileType.Empty]);
            // mouse.transform.position = grid.CellToWorld(cellPos)+offset; // mouse movement
            oldCellPos = cellPos;
        }

        // create building
        if (!useLines)
        {
            if (Input.GetMouseButton(0) && IsPlaceble())
            {
                Instantiate(buildingTileDic[BuildingType.Build], grid.CellToWorld(cellPos) + offset, Quaternion.identity);
                mainTm.SetTile(cellPos, buildTileDic[TileType.Empty]);
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && IsPlaceble() && (points.Count() == 0 || points.Last() != cellPos))
            {
                Debug.Log("Add");
                points.Add(cellPos);
            }
            else if (Input.GetKey(KeyCode.Space) && points.Count > 1)
            {
                // setup
                Debug.Log("Ren");
                Vector3Int[] pointsArray = points.ToArray();
                Vector3[] linePointsArray = new Vector3[pointsArray.Length];
                for (int i = 0; i < pointsArray.Length; i++)
                {
                    linePointsArray[i] = grid.CellToWorld(pointsArray[i]) + offset;
                }

                // line render
                GameObject lineRef;
                lineRef = Instantiate(buildingTileDic[BuildingType.Line], grid.CellToWorld(cellPos) + offset, Quaternion.identity);
                lineRef.GetComponent<LineControler>().SetUpLine(linePointsArray);
                points = new List<Vector3Int>();

                // line base
                GameObject lineBaseRef;
                for (int i = 0; i < linePointsArray.Length; i++)
                {
                    lineBaseRef = Instantiate(buildingTileDic[BuildingType.LineBase], linePointsArray[i], Quaternion.identity);
                    if (i - 1 >= 0)
                    {
                        lineBaseRef.GetComponent<LineBaseControler>().priviosePoint = linePointsArray[i - 1];
                    }
                }
            }
        }
    }
    private bool IsPlaceble()
    {
        if (mainTm.GetTile<Tile>(cellPos) == buildTileDic[TileType.White])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawLine(po);
    // }

    public enum TileType
    {
        Empty,
        White,
        Red,
        Green,
        Road
    }

    public enum BuildingType
    {
        Build,
        Line,
        LineBase
    }
}
