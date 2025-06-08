using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class City : MonoBehaviour
{
    private int day = 0;
    public float cycleTime;

    private float moneyIncome;
    public int money;

    private float elecrisityLeft;
    private int personLeft;
    void Start()
    {
        Invoke("Cycle", cycleTime);
    }

    void FixedUpdate()
    {
        this.GetComponent<BuildingsRec>().OutputStatistics(out elecrisityLeft, out moneyIncome, out personLeft);
    }

    void Cycle()
    {
        this.GetComponent<BuildingsRec>().OutputStatistics(out elecrisityLeft, out moneyIncome, out personLeft);
        money += (int)Math.Round(moneyIncome, 0);
        day++;
        Invoke("Cycle", cycleTime);
    }
}
