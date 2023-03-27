using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;

    public static UnitSelections Instance { get { return _instance; } }
    public bool a_click = false;
    public int unitCnt;
    public int unitLimit;
    public Text unitLimitUI;
    public bool unitOver;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        unitCnt = 0;
        unitLimit = 10;
        unitOver = false;
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        if (a_click == false)
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            Check_Tag(unitToAdd, true);
        }
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            Check_Tag(unitToAdd, true);
            // unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
        else
        {
            //unitToAdd.GetComponent<UnitMovement>().enabled = false;
            Check_Tag(unitToAdd, false);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
        }
    }

    public void ControlClickSelect(GameObject unitToAdd)
    {
        if (unitToAdd.tag == "heal")
        {
            for (int i = unitList.Count - 1; i >= 0; i--)
            {
                if (unitList[i].tag == "heal")
                {
                    if (unitsSelected.Contains(unitList[i].gameObject))
                    {
                        continue;
                    }
                    unitsSelected.Add(unitList[i]);
                    unitList[i].transform.GetChild(0).gameObject.SetActive(true);
                    Check_Tag(unitList[i], true);
                }
            }
        }
        else if (unitToAdd.tag == "tank")
        {
            for (int i = unitList.Count - 1; i >= 0; i--)
            {
                if (unitList[i].tag == "tank")
                {
                    if (unitsSelected.Contains(unitList[i].gameObject))
                    {
                        continue;
                    }
                    unitsSelected.Add(unitList[i]);
                    unitList[i].transform.GetChild(0).gameObject.SetActive(true);
                    Check_Tag(unitList[i], true);
                }
            }
        }
        else if (unitToAdd.tag == "dps")
        {
            for (int i = unitList.Count - 1; i >= 0; i--)
            {
                if (unitList[i].tag == "dps")
                {
                    if (unitsSelected.Contains(unitList[i].gameObject))
                    {
                        continue;
                    }
                    unitsSelected.Add(unitList[i]);
                    unitList[i].transform.GetChild(0).gameObject.SetActive(true);
                    Check_Tag(unitList[i], true);
                }
            }
        }
    }

    public void ControlClickSelect_Shift(GameObject unitToAdd)
    {
        if (unitToAdd.tag == "heal")
        {
            for (int i = unitList.Count - 1; i >= 0; i--)
            {
                if (unitList[i].tag == "heal")
                {
                    if (unitsSelected.Contains(unitList[i].gameObject))
                    {
                        continue;
                    }
                    unitsSelected.Add(unitList[i]);
                    unitList[i].transform.GetChild(0).gameObject.SetActive(true);
                    Check_Tag(unitList[i], true);
                }
            }
        }
        else if (unitToAdd.tag == "tank")
        {
            for (int i = unitList.Count - 1; i >= 0; i--)
            {
                if (unitList[i].tag == "tank")
                {
                    if (unitsSelected.Contains(unitList[i].gameObject))
                    {
                        continue;
                    }
                    unitsSelected.Add(unitList[i]);
                    unitList[i].transform.GetChild(0).gameObject.SetActive(true);
                    Check_Tag(unitList[i], true);
                }
            }
        }
        else if (unitToAdd.tag == "dps")
        {
            for (int i = unitList.Count - 1; i >= 0; i--)
            {
                if (unitList[i].tag == "dps")
                {
                    if (unitsSelected.Contains(unitList[i].gameObject))
                    {
                        continue;
                    }
                    unitsSelected.Add(unitList[i]);
                    unitList[i].transform.GetChild(0).gameObject.SetActive(true);
                    Check_Tag(unitList[i], true);
                }
            }
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            Check_Tag(unitToAdd, true);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            //unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }

    public void DeselectAll()
    {
        if (a_click == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var unit in unitsSelected)
                {
                    //unit.GetComponent<UnitMovement>().enabled = false;
                    Check_Tag(unit, false);
                    unit.transform.GetChild(0).gameObject.SetActive(false);
                }
                unitsSelected.Clear();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                a_click = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (unitsSelected.Count > 0)
                a_click = true;
        }

        //유닛 수 계산해서 UI에 출력
        unitList = unitList.Distinct().ToList();
        unitCnt = unitList.Count;
        unitLimitUI.text = unitCnt.ToString() + "/" + unitLimit.ToString();
        if(unitCnt >= unitLimit) { unitOver = true; }
        else { unitOver = false; }
    }


    void Check_Tag(GameObject col, bool TF)
    {
        if (col.tag == "dps")
        {
            col.GetComponent<UnitMovement>().selected = TF;
        }
        else if (col.tag == "heal")
        {
            col.GetComponent<Heal_Unitmovement>().selected = TF;
        }
        else if (col.tag == "tank")
        {
            col.GetComponent<Tank_UnitMovement>().selected = TF;
        }
    }
}