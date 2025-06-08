using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiText2 : MonoBehaviour
{
    private Text text;
    public City city;

    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void FixedUpdate()
    {
        text.text = city.elecrisityLeft.ToString();
    }
}
