using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{

    public GameObject iso;
    public Tilemap tempTm;
    public Tilemap mainTm;

    private Vector3 mouse;
    private Vector3Int cellPos;
    private SpriteRenderer isoRen;
    private GridLayout grid;

    void Start()
    {
        grid = this.GetComponent<GridLayout>();
        isoRen = iso.GetComponent<SpriteRenderer>();
        // icoRen.enabled = !icoRen.enabled;
    }

    void FixedUpdate() {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = grid.WorldToCell(mouse);
        if (mainTm.HasTile(cellPos)) {
            Debug.Log(mainTm.GetTile<Tile>(cellPos).name);
        }

        iso.transform.position = grid.CellToWorld(cellPos);
    }
}
