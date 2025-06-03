using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControler : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3[] points;

    void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    public void SetUpLine(Vector3[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }
}
