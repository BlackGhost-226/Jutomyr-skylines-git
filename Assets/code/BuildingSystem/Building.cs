using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public float elecrisityProduse, moneyProduse;
    public bool roadNeeded, elecrisityNeeded;
    public int coast, person;

    private bool conected = false;


    private GridLayout grid;
    private Tilemap mainTm;
    private Vector3Int posInGrid;
    private List<Tile> tilesAround = new List<Tile>();


    void Start() {
        // if a electric producer
        if (elecrisityProduse > 0) {
            conected = true;
        }

        // grid setup
        grid = FindObjectOfType<GridLayout>();

        // mainTm setup
        foreach (var tilemap in FindObjectsOfType<Tilemap>()) {
            if (tilemap.name == "MainTilemap"){
                mainTm = tilemap;
            }
        }

        // posInGrid setup
        posInGrid = grid.WorldToCell(this.transform.position);

        // tilesAround setup
            // [*][*][*]
            // [ ] 0 [ ]
            // [ ][ ][ ]
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(0, 1, 0)));
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(1, 1, 0)));
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(-1, 1, 0)));

            // [ ][ ][ ]
            // [*] 0 [*]
            // [ ][ ][ ]
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(1, 0, 0)));
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(-1, 0, 0)));

            // [ ][ ][ ]
            // [ ] 0 [ ]
            // [*][*][*]
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(0, -1, 0)));
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(1, -1, 0)));
            tilesAround.Add(mainTm.GetTile<Tile>(posInGrid + new Vector3Int(-1, -1, 0)));
    }

    void FixedUpdate() {
        foreach (var tile in tilesAround) {
            if (tile == null) {
                Debug.Log(tile);
            }
        }
    }

}
