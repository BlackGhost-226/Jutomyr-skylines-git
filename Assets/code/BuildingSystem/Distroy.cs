using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distroy : MonoBehaviour
{
    private bool clicked = false;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "bildings") {
            if (Input.GetMouseButton(0) && !clicked) {
                Debug.Log("clicked");
                collision.GetComponent<Building>().Distory();
                clicked = true;
            }else if (!Input.GetMouseButton(0) && clicked) {
                clicked = false;
            }
        }
    }
}
