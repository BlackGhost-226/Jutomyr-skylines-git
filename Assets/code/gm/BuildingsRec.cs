using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsRec : MonoBehaviour
{
    private List<GameObject> buildingsList = new List<GameObject>();

    private float elecrisityProduse, moneyProduse;
    private int personLeft;
    void FixedUpdate()
    {

        BuidingsUpdate();
        IncomeUpdate();
    }

    void BuidingsUpdate()
    {
        GameObject[] allBuildings = GameObject.FindGameObjectsWithTag("bildings");

        foreach (var building in allBuildings)
        {
            if (!buildingsList.Contains(building))
            {
                buildingsList.Add(building);
            }
        }
    }

    void IncomeUpdate()
    {
        float ele = 0f;
        float mon = 0f;
        int per = 0;
        if (buildingsList.Count > 0)
        {
            foreach (var buildingFromList in buildingsList)
            {
                float eleF = 0f;
                float monF = 0f;
                int perF = 0;
                buildingFromList.GetComponent<Building>().OutputStatistics(out eleF, out monF, out perF);
                ele += eleF;
                mon += monF;
                per += perF;
            }
        }
        elecrisityProduse = ele;
        moneyProduse = mon;
        personLeft = per;
    }

    public void OutputStatistics(out float ele, out float mon, out int per)
    {
        if (elecrisityProduse >= 0)
        {
            if (personLeft >= 0)
            {
                ele = elecrisityProduse;
                mon = moneyProduse;
                per = personLeft;
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
