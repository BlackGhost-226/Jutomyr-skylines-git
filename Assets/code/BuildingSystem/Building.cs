using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public float coast, elecrisityProduse, moneyProduse;
    public bool roadNeeded, elecrisityNeeded;
    public int person;

    [HideInInspector] public bool conected = false;


    private GridLayout grid;
    private Tilemap mainTm;
    private Vector3Int posInGrid;
    private List<Vector3Int> tilesPosAround = new List<Vector3Int>();
    [HideInInspector] public GameObject connectedBuilding;


    void Start()
    {
        // if a electric producer
        if (elecrisityProduse > 0)
        {
            conected = true;
        }

        // grid setup
        grid = FindObjectOfType<GridLayout>();

        // mainTm setup
        foreach (var tilemap in FindObjectsOfType<Tilemap>())
        {
            if (tilemap.name == "MainTilemap")
            {
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

    void FixedUpdate()
    {
        if (elecrisityNeeded)
        {
            if (connectedBuilding != null)
            {
                conected = connectedBuilding.GetComponent<Building>().conected;
            }
            else
            {
                conected = false;
                FindConnection();
            }
        }
    }

    void FindConnection()
    {
        foreach (var tilePos in tilesPosAround)
        {
            foreach (var building in FindObjectsOfType<GameObject>())
            {
                if (building.tag == "bildings")
                {
                    if (grid.WorldToCell(building.transform.position) == tilePos)
                    {
                        if (building.GetComponent<Building>().conected == true)
                        {
                            connectedBuilding = building;
                        }
                    }
                }
            }
        }
    }

    bool Road()
    {
        foreach (var tilePos in tilesPosAround)
        {
            if (mainTm.GetTile(tilePos) == Resources.Load<Tile>("palette/build/road"))
            {
                return true;
            }
        }
        return false;
    }

    public void OutputStatistics(out float ele, out float mon, out int per)
    {
        if ((elecrisityNeeded && conected) || !elecrisityNeeded)
        {
            if ((roadNeeded && Road()) || !roadNeeded)
            {
                ele = elecrisityProduse;
                mon = moneyProduse;
                per = person;
            }
            else
            {
                ele = 0f;
                mon = 0f;
                per = 0;
            }
        }
        else
        {
            ele = 0f;
            mon = 0f;
            per = 0;
        }
    }

}
