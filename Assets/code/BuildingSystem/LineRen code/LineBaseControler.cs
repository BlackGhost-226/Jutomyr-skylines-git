using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class LineBaseControler : MonoBehaviour
{
    public Vector3 priviosePoint;
    public Tile tile;
    private Vector3 pos;
    private float dis;
    private float steps = 0.5f;
    private List<Vector3> moveList;
    private GridLayout grid;
    private Tilemap mainTm;

    void Start()
    {
        // grid setup
        grid = FindObjectOfType<GridLayout>();

        // mainTm setup
        foreach (var tilemap in FindObjectsOfType<Tilemap>()) {
            if (tilemap.name == "MainTilemap"){
                mainTm = tilemap;
            }
        }

        //
        pos = this.transform.position;
        dis = Vector3.Distance(pos, priviosePoint);
        //for (float i = 0f; i <= dis / steps; i += steps)
        //{
        //    Debug.Log("step");
        //    moveList.Add(grid.WorldToCell(Vector3.MoveTowards(pos, priviosePoint, i)));
        //}
        //moveList.Distinct();
        //foreach (var cell in moveList)
        //{
        //    mainTm.SetTile(Vector3Int.FloorToInt(cell), tile);
        //}
    }
}
