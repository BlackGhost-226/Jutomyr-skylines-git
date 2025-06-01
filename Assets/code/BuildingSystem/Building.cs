using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public float elecrisityProduse, moneyProduse;
    public bool roadNeeded, elecrisityNeeded;
    public int coast, person;

    public bool conected = false;


    private GridLayout grid;
    private Tilemap mainTm;
    private Vector3Int posInGrid;
    private List<Vector3Int> tilesPosAround = new List<Vector3Int>();


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
            tilesPosAround.Add(posInGrid + new Vector3Int(0, 1, 0));
            tilesPosAround.Add(posInGrid + new Vector3Int(1, 1, 0));
            tilesPosAround.Add(posInGrid + new Vector3Int(-1, 1, 0));

            // [ ][ ][ ]
            // [*] 0 [*]
            // [ ][ ][ ]
            tilesPosAround.Add(posInGrid + new Vector3Int(1, 0, 0));
            tilesPosAround.Add(posInGrid + new Vector3Int(-1, 0, 0));

            // [ ][ ][ ]
            // [ ] 0 [ ]
            // [*][*][*]
            tilesPosAround.Add(posInGrid + new Vector3Int(0, -1, 0));
            tilesPosAround.Add(posInGrid + new Vector3Int(1, -1, 0));
            tilesPosAround.Add(posInGrid + new Vector3Int(-1, -1, 0));
    }

    void FixedUpdate() {
        foreach (var tilePos in tilesPosAround) {
            foreach (var building in FindObjectsOfType<GameObject>()){
                if (building.tag == "bildings") {
                    if (grid.WorldToCell(building.transform.position) == tilePos) {
                        if (building.GetComponent<Building>().conected == true) {
                            conected = true;
                        }
                    }
                }
            }
        }
    }

}
