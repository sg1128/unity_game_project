using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_P : MonoBehaviour
{
    Animal_Change animalchange;
    public bool l_buff1, l_buff2, l_buff3;
    public bool h_buff1, h_buff2, h_buff3;
    public bool m_buff1, m_buff2, m_buff3;
    public bool H_buff1, H_buff2, H_buff3;
    float startTime;
    float hptime;
    bool st = true;
    int random;
    void Start()
    {
        animalchange = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        l_buff1 = false;
        l_buff2 = false;
        l_buff3 = false;
        h_buff1 = false;
        h_buff2 = false;
        h_buff3 = false;
        m_buff1 = false;
        m_buff2 = false;
        m_buff3 = false;
        H_buff1 = false;
        H_buff2 = false;
        H_buff3 = false;
    }

    void Update()
    {
        if (animalchange.deer.Count + animalchange.wolf.Count >= 10)
        {
            l_buff1 = true;
        }
        else
        {
            l_buff1 = false;
        }
        if (animalchange.deer.Count + animalchange.wolf.Count >= 20)
        {
            l_buff2 = true;
        }
        else
        {
            l_buff2 = false;
        }
        if (animalchange.deer.Count + animalchange.wolf.Count >= 30)
        {
            l_buff3 = true;
        }
        else
        {
            l_buff3 = false;
        }


        if (animalchange.bear.Count + animalchange.elephant.Count >= 10)
        {
            H_buff1 = true;
        }
        else
        {
            H_buff1 = false;
        }
        if (animalchange.bear.Count + animalchange.elephant.Count >= 20)
        {
            H_buff2 = true;
        }
        else
        {
            H_buff2 = false;
        }
        if (animalchange.bear.Count + animalchange.elephant.Count >= 30)
        {
            H_buff3 = true;
        }
        else
        {
            H_buff3 = false;
        }
        if (animalchange.bear.Count + animalchange.wolf.Count >= 10)
        {
            m_buff1 = true;
            if (st)
            {
                startTime = Time.time;
                st = false;
            }
            hptime = Time.time - startTime;
            //Debug.Log(((int)hptime).ToString());
            if (hptime >= 20)
            {
                while (true)
                {
                    startTime = Time.time;
                    random = Random.Range(0, animalchange.asd.Count);
                    if (animalchange.asd[random].tag != "Player")
                    {
                        if (animalchange.asd[random].GetComponent<Hpbar>().nowHp - 50 <= 0)
                        {
                            Destroy(animalchange.asd[random].GetComponent<Hpbar>().delete_hp.hpBar.gameObject);
                            Destroy(animalchange.asd[random].gameObject);
                        }
                        else
                        {
                            animalchange.asd[random].GetComponent<Hpbar>().nowHp -= 50;
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            st = true;
            m_buff1 = false;
        }
        if (animalchange.bear.Count + animalchange.wolf.Count >= 20)
        {
            m_buff2 = true;
        }
        else
        {
            m_buff2 = false;
        }
        if (animalchange.bear.Count + animalchange.wolf.Count >= 30)
        {
            m_buff3 = true;
        }
        else
        {
            m_buff3 = false;
        }

        if (animalchange.deer.Count + animalchange.elephant.Count >= 10)
        {
            h_buff1 = true;
        }
        else
        {
            h_buff1 = false;
        }
        if (animalchange.deer.Count + animalchange.elephant.Count >= 20)
        {
            h_buff2 = true;
        }
        else
        {
            h_buff2 = false;
        }
        if (animalchange.deer.Count + animalchange.elephant.Count >= 30)
        {
            h_buff3 = true;
        }
        else
        {
            h_buff3 = false;
        }
    }

}
    //void synergyOn()
    //{
    //    playerName = GameObject.FindWithTag("Player");
    //    if (playerName.name == "deer(Clone)")
    //    {
    //        lightness = true;
    //        herbivore = true;
    //        meateat = false;
    //        Heavyness = false;
    //    }
    //    else if (playerName.name == "wolf(Clone)")
    //    {
    //        lightness = true;
    //        herbivore = false;
    //        meateat = true;
    //        Heavyness = false;
    //    }
    //    else if (playerName.name == "bear(Clone)")
    //    {
    //        lightness = false;
    //        herbivore = false;
    //        meateat = true;
    //        Heavyness = true;
    //    }
    //    else if (playerName.name == "elephant(Clone)")
    //    {
    //        lightness = false;
    //        herbivore = true;
    //        meateat = false;
    //        Heavyness = true;
    //    }
    //}

