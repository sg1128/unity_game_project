using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyUI : MonoBehaviour
{
    public GameObject sng_carnivore;
    public GameObject sng_dexterity;
    public GameObject sng_giant;
    public GameObject sng_herbivore;
    public GameObject sng_hunt;
    public GameObject sng_intuition;
    public GameObject sng_overpower;
    public GameObject sng_revenge;
    public GameObject sng_survival;
    public GameObject sng_threat;
    int Count = 1;
    public Transform port1;
    public Transform port2;
    public Transform port3;
    public Transform port4;
    public Transform port5;
    public Transform port6;
    public Transform port7;
    Synergy_P S_P;
    Synergy_S S_S;
    bool buff1 = false;
    bool buff2 = false;
    bool buff3 = false;
    bool buff4 = false;
    bool buff5 = false;
    bool buff6 = false;
    bool buff7 = false;
    bool buff8 = false;
    bool buff9 = false;
    bool buff10 = false;
    public RectTransform car_1;
    public RectTransform car_2;
    public RectTransform car_3;
    public RectTransform dex_1;
    public RectTransform dex_2;
    public RectTransform dex_3;
    public RectTransform gin_1;
    public RectTransform gin_2;
    public RectTransform gin_3;
    public RectTransform her_1;
    public RectTransform her_2;
    public RectTransform her_3;
    public RectTransform hun_1;
    public RectTransform rev_1;
    public RectTransform sur_1;
    public RectTransform thr_1;
    public RectTransform int_1;
    public RectTransform ove_1;
    public List<GameObject> synergyUI = new List<GameObject>();
    public List<RectTransform> synergyINFO = new List<RectTransform>();
    void Start()
    {
        S_P = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_P>();
        S_S = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_S>();
    }

    // Update is called once per frame
    void Update()
    {
            where();
        for(int i=0; i<synergyUI.Count; i++)
        {
            Count = i + 1;
            AddSynergy(synergyUI[i]);
        }
        if (S_P.l_buff1)
        {
            if (!buff1)
            {
                synergyUI.Add(sng_dexterity);
                buff1 = true;
                synergyINFO.Add(dex_1);
            }
        }
        else
        {
            if (buff1)
            {
                sng_dexterity.SetActive(false);
                synergyUI.Remove(sng_dexterity);
                buff1 = false;
                synergyINFO.Remove(dex_1);
            }
        }
        /////////////////////////////////////////////////////////////
        if (S_P.h_buff1)
        {
            if (!buff2)
            {
                synergyUI.Add(sng_herbivore);
                buff2 = true;
                synergyINFO.Add(her_1);
            }
        }
        else
        {
            if (buff2)
            {
                sng_herbivore.SetActive(false);
                synergyUI.Remove(sng_herbivore);
                buff2 = false;
                synergyINFO.Remove(her_1);
            }
        }
        /////////////////////////////////////////////////////////////
        if (S_P.m_buff1)
        {
            if (!buff3)
            {
                synergyUI.Add(sng_carnivore);
                buff3 = true;
                synergyINFO.Add(car_1);

            }
        }
        else
        {
            if (buff3)
            {
                sng_carnivore.SetActive(false);
                synergyUI.Remove(sng_carnivore);
                buff3 = false;
               synergyINFO.Remove(car_1);
            }
        }
        ///////////////////////////////////////////////////////////
        if (S_P.H_buff1)
        {
            if (!buff4)
            {
                synergyUI.Add(sng_giant);
                buff4 = true;
                synergyINFO.Add(gin_1);
            }
        }
        else
        {
            if (buff4)
            {
                sng_giant.SetActive(false);
                synergyUI.Remove(sng_giant);
                buff4 = false;
                synergyINFO.Remove(gin_1);
            }
        }
        ////////////////////////////////////////////////////////
        if (S_S.lg_buff)
        {
            if (!buff5)
            {
                synergyUI.Add(sng_revenge);
                buff5 = true;
                synergyINFO.Add(rev_1);
            }
        }
        else
        {
            if (buff5)
            {
                sng_revenge.SetActive(false);
                synergyUI.Remove(sng_revenge);
                buff5 = false;
                synergyINFO.Remove(rev_1);
            }
        }
        /////////////////////////////////////////////////////////
        if (S_S.y_buff)
        {
            if (!buff6)
            {
                synergyUI.Add(sng_survival);
                buff6 = true;
                synergyINFO.Add(sur_1);
            }
        }
        else
        {
            if (buff6)
            {
                sng_survival.SetActive(false);
                synergyUI.Remove(sng_survival);
                buff6 = false;
                synergyINFO.Remove(sur_1);
            }
        }
        /////////////////////////////////////////////////////////////
        if (S_S.s_buff)
        {
            if (!buff7)
            {
                synergyUI.Add(sng_intuition);
                buff7 = true;
                synergyINFO.Add(int_1);
            }
        }
        else
        {
            if (buff7)
            {
                sng_intuition.SetActive(false);
                synergyUI.Remove(sng_intuition);
                buff7 = false;
                synergyINFO.Remove(int_1);
            }
        }
        //////////////////////////////////////////////////////////////
        if (S_S.o_buff)
        {
            if (!buff8)
            {
                synergyUI.Add(sng_hunt);
                buff8 = true;
                synergyINFO.Add(hun_1);
            }
        }
        else
        {
            if (buff8)
            {
                sng_hunt.SetActive(false);
                synergyUI.Remove(sng_hunt);
                buff8 = false;
                synergyINFO.Remove(hun_1);
            }
        }
        //////////////////////////////////////////////////////////////
        if (S_S.g_buff)
        {
            if (!buff9)
            {
                synergyUI.Add(sng_threat);
                buff9 = true;
                synergyINFO.Add(thr_1);
            }
        }
        else
        {
            if (buff9)
            {
                sng_threat.SetActive(false);
                synergyUI.Remove(sng_threat);
                buff9 = false;
                synergyINFO.Remove(thr_1);
            }
        }
        //////////////////////////////////////////////////////////////
        if (S_S.p_buff)
        {
            if (!buff10)
            {
                synergyUI.Add(sng_overpower);
                buff10 = true;
                synergyINFO.Add(ove_1);
            }
        }
        else
        {
            if (buff10)
            {
                sng_overpower.SetActive(false);
                synergyUI.Remove(sng_overpower);
                buff10 = false;
                synergyINFO.Remove(ove_1);
            }
        }
      

    }

    void AddSynergy(GameObject synergy)
    {
        if (Count == 1)
        {
            synergy.SetActive(true);
            synergy.transform.position = port1.position;
        }
        else if (Count == 2)
        {
            synergy.SetActive(true);
            synergy.transform.position = port2.position;
        }
        else if (Count == 3)
        {
            synergy.SetActive(true);
            synergy.transform.position = port3.position;
        }
        else if (Count == 4)
        {
            synergy.SetActive(true);
            synergy.transform.position = port4.position;
        }
        else if (Count == 5)
        {
            synergy.SetActive(true);
            synergy.transform.position = port5.position;
        }
        else if (Count == 6)
        {
            synergy.SetActive(true);
            synergy.transform.position = port6.position;
        }
        else if (Count == 7)
        {
            synergy.SetActive(true);
            synergy.transform.position = port7.position;
        }
    }

    void where()
    {
        if (synergyINFO.Count > 0)
        {
            synergyINFO[0].offsetMin = new Vector2(0, 0);
            synergyINFO[0].offsetMax = new Vector2(0, 0);
        }
        if (synergyINFO.Count > 1)
        {
            synergyINFO[1].offsetMin = new Vector2(55, 0);
            synergyINFO[1].offsetMax = new Vector2(55, 0);
        }
        if (synergyINFO.Count > 2)
        {
            synergyINFO[2].offsetMin = new Vector2(110, 0);
            synergyINFO[2].offsetMax = new Vector2(110, 0);
        }
        if (synergyINFO.Count > 3)
        {
            synergyINFO[3].offsetMin = new Vector2(165, 0);
            synergyINFO[3].offsetMax = new Vector2(165, 0);
        }
        if (synergyINFO.Count > 4)
        {
            synergyINFO[4].offsetMin = new Vector2(220, 0);
            synergyINFO[4].offsetMax = new Vector2(220, 0);
        }
        if (synergyINFO.Count > 5)
        {
            synergyINFO[5].offsetMin = new Vector2(275, 0);
            synergyINFO[5].offsetMax = new Vector2(275, 0);
        }
        if (synergyINFO.Count > 6)
        {
            synergyINFO[6].offsetMin = new Vector2(330, 0);
            synergyINFO[6].offsetMax = new Vector2(330, 0);
        }
        if (synergyINFO.Count > 7)
        {
            synergyINFO[7].offsetMin = new Vector2(385, 0);
            synergyINFO[7].offsetMax = new Vector2(55, 0);
        }
    }

}
