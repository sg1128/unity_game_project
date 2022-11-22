using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    Synergy_S S_S;
    Synergy_P S_P;
    Command cmd;
    public GameObject buff_cooldown;
    void Start()
    {
        S_P = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_P>();
        S_S = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_S>();
        cmd = GameObject.FindWithTag("MainCamera").GetComponent<Command>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "CRI_DOWN")
        {
            if (S_P.h_buff1)
            {
                buff_cooldown.SetActive(false);
            }
            else
            {
                buff_cooldown.SetActive(true);
            }
        }else if(gameObject.name == "DEX_DOWN")
        {
            if (S_P.H_buff1)
            {
                buff_cooldown.SetActive(false);
            }
            else
            {
                buff_cooldown.SetActive(true);
            }
        }
        else if (gameObject.name == "ATK_UP")
        {
            if (S_P.m_buff1 || cmd.bearbuffon)
            {
                buff_cooldown.SetActive(false);
            }
            else
            {
                buff_cooldown.SetActive(true);
            }
        }
        else if (gameObject.name == "CRI_UP")
        {
            if (S_S.lg_buff)
            {
                buff_cooldown.SetActive(false);
            }
            else
            {
                buff_cooldown.SetActive(true);
            }

        }
        else if (gameObject.name == "DEX_UP")
        {
            if(S_P.l_buff1 || S_S.s_buff || cmd.wolfbuffon)
            {
                buff_cooldown.SetActive(false);
            }else //if(!S_P.l_buff1 && !S_S.s_buff && !cmd.wolfbuffon)
            {
                buff_cooldown.SetActive(true);
            }
        }
        else if (gameObject.name == "HP_UP")
        {
            if (S_S.y_buff || cmd.elephantbuffon)
            {
                buff_cooldown.SetActive(false);
            }
            else
            {
                buff_cooldown.SetActive(true);
            }
        }
        else if (gameObject.name == "DEF_UP")
        {
            if (S_P.H_buff1)
            {
                buff_cooldown.SetActive(false);
            }
            else
            {
                buff_cooldown.SetActive(true);
            }
        }
    }
}
