using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_MoreHp : MonoBehaviour
{
    Animal_Change animalC;
    CameraFollow cf;
    float nhp = 0;

    void Start()
    {
        animalC = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        cf = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (cf.p_ani == 4)
        {
            nhp = 0;
            if (animalC.elephant.Count > 0)
            {
                for (int i = 0; i < animalC.elephant.Count; i++)
                {
                    Hpbar elephanthp = animalC.elephant[i].GetComponent<Hpbar>();
                    if (nhp <= elephanthp.nowHp)
                    {
                        nhp = elephanthp.nowHp;
                        cf.eidx = i;
                    }
                }
            }
        }
        if (cf.p_ani == 1)
        {
            nhp = 0;
            if (animalC.deer.Count > 0)
            {
                for (int i = 0; i < animalC.deer.Count; i++)
                {
                    Hpbar deerhp = animalC.deer[i].GetComponent<Hpbar>();
                    if (nhp <= deerhp.nowHp)
                    {
                        nhp = deerhp.nowHp;
                        cf.didx = i;
                    }
                }
            }
        }
        if (cf.p_ani == 2)
        {
            nhp = 0;
            if (animalC.wolf.Count > 0)
            {
                for (int i = 0; i < animalC.wolf.Count; i++)
                {
                    Hpbar wolfhp = animalC.wolf[i].GetComponent<Hpbar>();
                    if (nhp <= wolfhp.nowHp)
                    {
                        nhp = wolfhp.nowHp;
                        cf.widx = i;
                    }
                }
            }
        }
        if (cf.p_ani == 3)
        {
            nhp = 0;
            if (animalC.bear.Count > 0)
            {
                for (int i = 0; i < animalC.bear.Count; i++)
                {
                    Hpbar bearhp = animalC.bear[i].GetComponent<Hpbar>();
                    if (nhp <= bearhp.nowHp)
                    {
                        nhp = bearhp.nowHp;
                        cf.bidx = i;
                    }
                }
            }
        }
    }
}
