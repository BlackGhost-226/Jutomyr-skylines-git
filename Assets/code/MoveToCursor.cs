using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCursor : MonoBehaviour
{
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }
    void FixedUpdate() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mouse.x, mouse.y, 0);
    }
}
