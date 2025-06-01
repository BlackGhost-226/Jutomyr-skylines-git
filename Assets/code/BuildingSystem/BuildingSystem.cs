using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{

    public GameObject building;
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
        buildingTileDic.Add(BuildingType.Raycast, building);
    }

    void FixedUpdate() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = grid.WorldToCell(mousePos);

        // visual
        if (oldCellPos != cellPos) {
            if (IsPlaceble()) {
                tempTm.SetTile(cellPos, buildTileDic[TileType.Green]);
            }else {
                tempTm.SetTile(cellPos, buildTileDic[TileType.Red]);
            }

            tempTm.SetTile(oldCellPos, buildTileDic[TileType.Empty]);
            // mouse.transform.position = grid.CellToWorld(cellPos)+offset; // mouse movement
            oldCellPos = cellPos;
        }

        // create building
        if (false) {
            if (Input.GetMouseButton(0) && IsPlaceble()) {
                    Instantiate(buildingTileDic[BuildingType.Build], grid.CellToWorld(cellPos)+offset, Quaternion.identity);
                    mainTm.SetTile(cellPos, buildTileDic[TileType.Empty]);
            }
        }else {
            Vector3 pointA = Vector3.zero;
            Vector3 pointB = Vector3.zero;
            if (Input.GetMouseButton(0) && IsPlaceble()) {
                pointA = mousePos;
                while (pointB == Vector3.zero) {
                    if (Input.GetMouseButton(0) && IsPlaceble()) {
                        pointB = mousePos;
                        Vector3 Direction = (pointB-pointA).normalized;
                    }
                }
            }
        }
    }

    private bool IsPlaceble() {
        if (mainTm.GetTile<Tile>(cellPos) == buildTileDic[TileType.White]) {
            return true;
        }else {
            return false;
        }
    }

    public enum TileType {
        Empty,
        White,
        Red,
        Green,
        Road
    }

    public enum BuildingType {
        Build,
        Raycast
    }
}
