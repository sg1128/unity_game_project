using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Synergy_Info1 : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
    public int buff = 0;

    Synergy_P S_P;
    void Start()
    {
        S_P = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_P>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "sng_carnivore")
        {
            if (S_P.m_buff1)
            {
                buff = 1;
            }
            if (S_P.m_buff2)
            {
                buff = 2;
            }
            if (S_P.m_buff3)
            {
                buff = 3;
            }
        }
        else if(gameObject.name == "sng_dexterity")
        {
            if (S_P.l_buff1)
            {
                buff = 1;
            }
            if (S_P.l_buff2)
            {
                buff = 2;
            }
            if (S_P.l_buff3)
            {
                buff = 3;
            }
        }
        else if(gameObject.name == "sng_giant")
        {
            if (S_P.H_buff1)
            {
                buff = 1;
            }
            if (S_P.H_buff2)
            {
                buff = 2;
            }
            if (S_P.H_buff3)
            {
                buff = 3;
            }
        }
        else if(gameObject.name == "sng_herbivore")
        {
            if (S_P.h_buff1)
            {
                buff = 1;
            }
            if (S_P.h_buff2)
            {
                buff = 2;
            }
            if (S_P.h_buff3)
            {
                buff = 3;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(buff == 1)
        {
            info1.SetActive(true);
        }
        else if(buff == 2)
        {
            info2.SetActive(true);
        }
        else if(buff == 3)
        {
            info3.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buff == 1)
        {
            info1.SetActive(false);
        }
        else if (buff == 2)
        {
            info2.SetActive(false);
        }
        else if (buff == 3)
        {
            info3.SetActive(false);
        }
    }
}
