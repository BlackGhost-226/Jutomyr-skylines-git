using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControler : MonoBehaviour
{
    public BuildingSystem BS;
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
        BS.useLines = false;
        BS.building = buildingDic[Building.Home];
    }

    public void ChangeToBuildStore()
    {
        BS.useLines = false;
        BS.building = buildingDic[Building.Store];
    }

    public void ChangeToBuildPowerStation()
    {
        BS.useLines = false;
        BS.building = buildingDic[Building.PowerStation];
    }

    public void ChangeToLineRoad()
    {
        BS.useLines = true;
        BS.line = lineDic[Line.Road];
        BS.lineBase = lineBaseDic[LineBase.Road];
    }
    public void ChangeToLineWire()
    {
        BS.useLines = true;
        BS.line = lineDic[Line.Wire];
        BS.lineBase = lineBaseDic[LineBase.Wire];
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
