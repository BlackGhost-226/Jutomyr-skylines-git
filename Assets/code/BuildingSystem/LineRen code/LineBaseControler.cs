using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class LineBaseControler : MonoBehaviour
{
    [HideInInspector] public Vector3Int priviosePointPos;
    private GameObject priviosePoint;
    public Tile tile;
    private Vector3Int pos;
    private Tilemap mainTm;
    private GridLayout grid;

    public int coast;
    public bool filIn, eleTransform;

    void Start()
    {
        grid = FindObjectOfType<GridLayout>();

        pos = grid.WorldToCell(this.transform.position);

        foreach (var tilemap in FindObjectsOfType<Tilemap>())
        {
            if (tilemap.name == "MainTilemap")
            {
                mainTm = tilemap;
            }
        }

        foreach (var lineBase in FindObjectsOfType<GameObject>())
        {
            if (lineBase.GetComponent<LineBaseControler>() != null && lineBase.transform.position == priviosePointPos)
            {
                priviosePoint = lineBase;
            }
        }

        if (filIn)
        {
            if (priviosePointPos != Vector3Int.zero)
            {
                pos = grid.WorldToCell(pos);
                priviosePointPos = grid.WorldToCell(priviosePointPos);

                DrawLine(priviosePointPos, pos);
                Debug.Log(priviosePointPos + " | " + pos);
            }
            else
            {
                mainTm.SetTile(pos, tile);
                Debug.Log("zero");
            }
        }

        if (eleTransform)
        {
            this.GetComponent<Building>().connectedBuilding = priviosePoint;
        }
    }

    public void DrawLine(Vector3Int start, Vector3Int end)
    {
        int x0 = start.x;
        int y0 = start.y;
        int x1 = end.x;
        int y1 = end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);

        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;

        int err = dx - dy;

        while (true)
        {
            mainTm.SetTile(new Vector3Int(x0, y0, 0), tile);

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;

            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }
}
