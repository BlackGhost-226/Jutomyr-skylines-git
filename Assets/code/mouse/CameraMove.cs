using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime;
    public Transform target;
    private Vector3 velocity = Vector3.zero;


    public Vector2 boxSize;
    public float castDis;
    public LayerMask layer;

    void FixedUpdate() {
        Vector3 trgetPos = target.position + offset;
        if (!InBox()) {
            transform.position = Vector3.SmoothDamp(transform.position, trgetPos, ref velocity, smoothTime);
            }
    }

    private bool InBox() {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDis, layer);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position-transform.up * castDis, boxSize);
    }
}
