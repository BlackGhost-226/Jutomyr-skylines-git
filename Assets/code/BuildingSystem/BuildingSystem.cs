using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{

    public GameObject iso;
    public Tilemap tempTm;
    public Tilemap mainTm;
    public GameObject m;

    private Vector3 mousePos;
    private Vector3Int cellPos;
    private GridLayout grid;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0f);
    private Dictionary<BuildTileType, Tile> buildTileDic = new Dictionary<BuildTileType, Tile>();
    private Dictionary<BuildingTileType, GameObject> buildingTileDic = new Dictionary<BuildingTileType, GameObject>();
    private Vector3Int oldCellPos;

    private SpriteRenderer mR;
    private SpriteRenderer isoR;

    void Start()
    {
        grid = this.GetComponent<GridLayout>();
        mR = m.GetComponent<SpriteRenderer>();
        isoR = iso.GetComponent<SpriteRenderer>();

        mR.sprite = isoR.sprite;
        mR.color = isoR.color;

        buildTileDic.Add(BuildTileType.Empty, Resources.Load<Tile>("palette/build/empty"));
        buildTileDic.Add(BuildTileType.White, Resources.Load<Tile>("palette/build/white"));
        buildTileDic.Add(BuildTileType.Red, Resources.Load<Tile>("palette/build/red"));
        buildTileDic.Add(BuildTileType.Green, Resources.Load<Tile>("palette/build/green"));
        buildTileDic.Add(BuildTileType.Road, Resources.Load<Tile>("palette/build/road"));

        buildingTileDic.Add(BuildingTileType.Home, iso);
    }

    void FixedUpdate() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = grid.WorldToCell(mousePos);

        // visual
        if (oldCellPos != cellPos) {
            if (IsPlaceble()) {
                tempTm.SetTile(cellPos, buildTileDic[BuildTileType.Green]);
            }else {
                tempTm.SetTile(cellPos, buildTileDic[BuildTileType.Red]);
            }

            tempTm.SetTile(oldCellPos, buildTileDic[BuildTileType.Empty]);
            m.transform.position = grid.CellToWorld(cellPos)+offset;
            oldCellPos = cellPos;
        }

        // create building
        if (Input.GetMouseButton(0) && IsPlaceble()) {
                Instantiate(buildingTileDic[BuildingTileType.Home], grid.CellToWorld(cellPos)+offset, Quaternion.identity);
                mainTm.SetTile(cellPos, buildTileDic[BuildTileType.Empty]);
        }
    }

    private bool IsPlaceble() {
        if (mainTm.GetTile<Tile>(cellPos) == buildTileDic[BuildTileType.White]) {
            return true;
        }else {
            return false;
        }
    }

    public enum BuildTileType {
        Empty,
        White,
        Red,
        Green,
        Road
    }

    public enum BuildingTileType {
        Home
    }
}
