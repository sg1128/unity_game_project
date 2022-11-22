using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuffUI_Info : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public int num = 0;
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
    Synergy_S S_S;
    Synergy_P S_P;
    Command cmd;

    void Start()
    {
        S_P = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_P>();
        S_S = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_S>();
        cmd = GameObject.FindWithTag("MainCamera").GetComponent<Command>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.name == "CRI_DOWN")
        {
            num = 1;
        }
        else if (gameObject.name == "DEX_DOWN")
        {
            num = 1;
        }
        else if (gameObject.name == "ATK_UP")
        {
            num = 2;
        }
        else if(gameObject.name == "CRI_UP")
        {
            num = 1;
        }
        else if (gameObject.name == "DEX_UP")
        {
            num = 3;
        }
        else if (gameObject.name == "HP_UP")
        {
            num = 2;
        }
        else if (gameObject.name == "DEF_UP")
        {
            num = 1;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (num == 1)
        {
            info1.SetActive(true);
        }
        else if (num == 2)
        {
            info1.SetActive(true);
            info2.SetActive(true);
        }
        else if (num == 3)
        {
            info1.SetActive(true);
            info2.SetActive(true);
            info3.SetActive(true);


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (num == 1)
        {
            info1.SetActive(false);
        }
        else if (num == 2)
        {
            info1.SetActive(false);
            info2.SetActive(false);
        }
        else if (num == 3)
        {
            info1.SetActive(false);
            info2.SetActive(false);
            info3.SetActive(false);
        }
    }
}
