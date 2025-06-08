using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControler : MonoBehaviour
{
    public GridLayout grid;
    private Dictionary<Building, GameObject> buildingDic = new Dictionary<Building, GameObject>();
    private Dictionary<Line, GameObject> lineDic = new Dictionary<Line, GameObject>();
    private Dictionary<LineBase, GameObject> lineBaseDic = new Dictionary<LineBase, GameObject>();

    void Start()
    {
        buildingDic.Add(Building.Home, Resources.Load<GameObject>("buildings/Home"));
        buildingDic.Add(Building.Store, Resources.Load<GameObject>("buildings/Store"));
        buildingDic.Add(Building.PowerStation, Resources.Load<GameObject>("buildings/PowerStation"));

        lineDic.Add(Line.Road, Resources.Load<GameObject>("road/LineRenRoad"));
        lineDic.Add(Line.Wire, Resources.Load<GameObject>("wire/LineRenWire"));

        lineBaseDic.Add(LineBase.Road, Resources.Load<GameObject>("road/RoadBase"));
        lineBaseDic.Add(LineBase.Wire, Resources.Load<GameObject>("wire/WireBase"));
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeToBuildHome()
    {
        grid.GetComponent<Distroy>().enabled = false;
        grid.GetComponent<BuildingSystem>().enabled = true;

        grid.GetComponent<BuildingSystem>().useLines = false;
        grid.GetComponent<BuildingSystem>().building = buildingDic[Building.Home];
    }

    public void ChangeToBuildStore()
    {
        grid.GetComponent<Distroy>().enabled = false;
        grid.GetComponent<BuildingSystem>().enabled = true;

        grid.GetComponent<BuildingSystem>().useLines = false;
        grid.GetComponent<BuildingSystem>().building = buildingDic[Building.Store];
    }

    public void ChangeToBuildPowerStation()
    {
        grid.GetComponent<Distroy>().enabled = false;
        grid.GetComponent<BuildingSystem>().enabled = true;

        grid.GetComponent<BuildingSystem>().useLines = false;
        grid.GetComponent<BuildingSystem>().building = buildingDic[Building.PowerStation];
    }

    public void ChangeToLineRoad()
    {
        grid.GetComponent<Distroy>().enabled = false;
        grid.GetComponent<BuildingSystem>().enabled = true;

        grid.GetComponent<BuildingSystem>().useLines = true;
        grid.GetComponent<BuildingSystem>().line = lineDic[Line.Road];
        grid.GetComponent<BuildingSystem>().lineBase = lineBaseDic[LineBase.Road];
    }
    public void ChangeToLineWire()
    {
        grid.GetComponent<Distroy>().enabled = false;
        grid.GetComponent<BuildingSystem>().enabled = true;

        grid.GetComponent<BuildingSystem>().useLines = true;
        grid.GetComponent<BuildingSystem>().line = lineDic[Line.Wire];
        grid.GetComponent<BuildingSystem>().lineBase = lineBaseDic[LineBase.Wire];
    }

    public void DistroyMode()
    {
        grid.GetComponent<Distroy>().enabled = true;
        grid.GetComponent<BuildingSystem>().enabled = false;
    }

    public enum Building
    {
        Home,
        Store,
        PowerStation
    }

    public enum Line
    {
        Road,
        Wire
    }

    public enum LineBase
    {
        Road,
        Wire
    }
}
