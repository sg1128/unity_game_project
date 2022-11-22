using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNG_P : MonoBehaviour
{
    Synergy_P S_p;
    Animal_Change animalchange;
    Hpbar status;
    Radius enemyRadius;
    public bool l_finish1, l_finish2, l_finish3;
    public bool h_finish1, h_finish2, h_finish3;
    public bool m_finish1, m_finish2, m_finish3;
    public bool H_finish1, H_finish2, H_finish3;
    bool h_on1 = false, h_on2 = false, h_on3 = false;
    float startTime;
    float hptime;
    float debuffDex = 0f;
    float debuffDex1 = 0f;
    float debuffDex2 = 0f;
    int random;
    void Start()
    {
        enemyRadius = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).GetComponent<Radius>();
        S_p = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Synergy_P>();
        animalchange = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        status = GetComponent<Hpbar>();
        l_finish1 = false;
        l_finish2 = false;
        l_finish3 = false;
        h_finish1 = false;
        h_finish2 = false;
        h_finish3 = false;
        m_finish1 = false;
        m_finish2 = false;
        m_finish3 = false;
        H_finish1 = false;
        H_finish2 = false;
        H_finish3 = false;
    }
    void Update()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            //°¡º­¿ò-------------------------------------------------------------
            if (S_p.l_buff1)
            {
                if (!l_finish1)
                {
                    status.Dex += 2f;
                    l_finish1 = true;
                }
            }
            else
            {
                if (l_finish1)
                {
                    l_finish1 = false;
                    status.Dex -= 2f;
                }
            }
            if (S_p.l_buff2)
            {
                if (!l_finish2)
                {
                    status.Dex += 1f;
                    l_finish2 = true;
                }
            }
            else
            {
                if (l_finish2)
                {
                    l_finish2 = false;
                    status.Dex -= 1f;
                }
            }
            if (S_p.l_buff3)
            {
                if (!l_finish3)
                {
                    status.Dex += 2f;
                    l_finish3 = true;
                }
            }
            else
            {
                if (l_finish3)
                {
                    l_finish3 = false;
                    status.Dex -= 2f;
                }
            }
        }
        //ÃÊ½Ä---------------------------------------------------------------------

        if (S_p.h_buff1)
        {
            if (!h_finish1 && !h_on1)
            {
                if (gameObject.tag == "Player" || gameObject.tag == "team")
                {
                    status.cri = status.cri / 2;
                    h_on1 = true;
                }
                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
                {
                    if (status.inRadius && !h_on1)
                    {
                        debuffDex = status.Def - 5f;
                        if (debuffDex >= 0)
                        {
                            status.Def -= 5f;
                        }
                        else
                        {
                            status.Def = 0;
                        }
                        h_on1 = true;
                    }
                    else if (!status.inRadius && h_on1)
                    {
                        if (debuffDex >= 0)
                        {
                            status.Def += 5f;
                        }
                        else
                        {
                            status.Def = debuffDex + 5f;
                        }
                        h_on1 = false;
                    }
                }
            }
        }
        else
        {
            if (h_on1)
                h_finish1 = true;
            if (h_finish1)
            {
                if (gameObject.tag == "Player" || gameObject.tag == "team")
                {
                    if (gameObject.name == "deer(Clone)")
                        status.cri = 25;
                    if (gameObject.name == "wolf(Clone)")
                        status.cri = 10;
                    if (gameObject.name == "bear(Clone)")
                        status.cri = 5;
                    if (gameObject.name == "elephant(Clone)")
                        status.cri = 2;
                    h_on1 = false;
                    h_finish1 = false;
                }
                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
                {
                    h_finish1 = false;
                    if (debuffDex >= 0)
                    {
                        status.Def += 5f;
                    }
                    else
                    {
                        status.Def = debuffDex + 5f;
                    }
                    h_on1 = false;
                }
            }
        }
        if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
        {
            if (S_p.h_buff2)
            {
                if (!h_finish2)
                {
                    if (status.inRadius && !h_on2)
                    {
                        debuffDex1 = status.Def - 5;
                        if (debuffDex1 >= 0)
                        {
                            status.Def -= 5;
                        }
                        else
                        {
                            status.Def = 0;
                        }
                        h_on2 = true;
                    }
                    else if (!status.inRadius && h_on2)
                    {
                        if (debuffDex1 >= 0)
                        {
                            status.Def += 5f;
                        }
                        else
                        {
                            status.Def = debuffDex1 + 5f;
                        }
                        h_on2 = false;
                    }
                }
            }
            else
            {
                if (h_on2)
                    h_finish2 = true;
                if (h_finish2)
                {
                    h_finish2 = false;
                    if (debuffDex1 >= 0)
                    {
                        status.Def += 5;
                    }
                    else
                    {
                        status.Def = debuffDex1 + 5f;
                    }
                    h_on2 = false;
                }
            }
            if (S_p.h_buff3)
            {
                if (!h_finish3)
                {
                    if (status.inRadius && !h_on3)
                    {
                        debuffDex2 = status.Def - 10;
                        if (debuffDex2 >= 0)
                        {
                            status.Def -= 10;
                        }
                        else
                        {
                            status.Def = 0;
                        }
                        h_on3 = true;
                    }
                    else if (!status.inRadius && h_on3)
                    {
                        if (debuffDex2 >= 0)
                        {
                            status.Def += 10;
                        }
                        else
                        {
                            status.Def = debuffDex2 + 10;
                        }
                        h_on3 = false;
                    }
                }
            }
            else
            {
                if (h_on3)
                    h_finish3 = true;
                if (h_finish3)
                {
                    h_finish3 = false;
                    if (debuffDex2 >= 0)
                    {
                        status.Def += 10;
                    }
                    else
                    {
                        status.Def = debuffDex2 + 10;
                    }
                    h_on3 = false;
                }
            }
        }


        // À°½Ä-------------------------------------------------------------------------
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (S_p.m_buff1)
            {
                if (!m_finish1)
                {
                    if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                        status.atkDmg += 3f;
                    m_finish1 = true;
                }
            }
            else
            {
                if (m_finish1)
                {
                    m_finish1 = false;
                    if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                        status.atkDmg -= 3f;
                }
            }
            if (S_p.m_buff2)
            {
                if (!m_finish2)
                {
                    if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                        status.atkDmg += 5f;
                    m_finish2 = true;
                }
            }
            else
            {
                if (m_finish2)
                {
                    m_finish2 = false;
                    if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                        status.atkDmg -= 5f;
                }
            }
            if (S_p.m_buff3)
            {
                if (!m_finish3)
                {
                    if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                        status.atkDmg += 7f;
                    m_finish3 = true;
                }
            }
            else
            {
                if (m_finish3)
                {
                    m_finish3 = false;
                    if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                        status.atkDmg -= 7f;
                }
            }
        }

        // ¹«°Å¿ò--------------------------------------------------------------------
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (S_p.H_buff1)
            {
                if (!H_finish1)
                {
                    status.Def += 5f;
                    status.Dex -= 1.5f;
                    H_finish1 = true;
                }
            }
            else
            {
                if (H_finish1)
                {
                    H_finish1 = false;
                    status.Dex += 1.5f;
                    status.Def -= 5f;
                }
            }
            if (S_p.H_buff2)
            {
                if (!H_finish2)
                {
                    status.Def += 5f;
                    H_finish2 = true;
                }
            }
            else
            {
                if (H_finish2)
                {
                    H_finish2 = false;
                    status.Def -= 5f;
                }
            }
            if (S_p.H_buff3)
            {
                if (!H_finish3)
                {
                    status.Def += 10f;
                    H_finish3 = true;
                }
            }
            else
            {
                if (H_finish3)
                {
                    H_finish3 = false;
                    status.Def -= 10f;
                }
            }
        }
    }
}
