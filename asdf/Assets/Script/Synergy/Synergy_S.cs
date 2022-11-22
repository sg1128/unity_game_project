using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_S : MonoBehaviour
{
    Animal_Change animalchange;
    bool lightgreen, yellow, sky, orange, green, purple;
    public bool lg_buff = false;
    public bool y_buff = false;
    public bool s_buff = false;
    public bool o_buff = false;
    public bool g_buff = false;
    public bool p_buff = false;
    public int crinum = 0;
    public int healnum = 0;
    GameObject playerName;
    void Start()
    {
        animalchange = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
    }

    void Update()
    {
        synergyOn();
        // 복수--------------------------------------------------------------
        if (lightgreen)
        {
            if (animalchange.deer.Count >= 5 && animalchange.wolf.Count >= 5)
            {
                lg_buff = true;
            }
            else
            {
                lg_buff = false;
            }
        }
        else
        {
            lg_buff = false;
        }

        // 생존 ----------------------------------------------------------------
        if (yellow)
        {
            if (animalchange.deer.Count >= 5 && animalchange.bear.Count >= 5)
            {
                y_buff = true;
            }
            else
            {
                y_buff = false;
            }
        }
        else
        {
            y_buff = false;
        }
        // 직감 -------------------------------------------------------------------
        if (sky)
        {
            if (animalchange.deer.Count >= 5 && animalchange.elephant.Count >= 5)
            {
                s_buff = true;
            }
            else
            {
                s_buff = false;
            }
        }
        else
        {
            s_buff = false;
        }

        // 사냥----------------------------------------------------------------------
        if (orange)
        {
            if (animalchange.wolf.Count >= 5 && animalchange.bear.Count >= 5)
            {
                o_buff = true;
            }
            else
            {
                o_buff = false;
            }
        }
        else
        {
            o_buff = false;
        }

        // 위협 ---------------------------------------------------------------------------
        if (green)
        {
            if (animalchange.wolf.Count >= 5 && animalchange.elephant.Count >= 5)
            {
                g_buff = true;
            }
            else
            {
                g_buff = false;
            }
        }
        else
        {
            g_buff = false;
        }
        // 압도 --------------------------------------------------------------------------------
        if (purple)
        {
            if (animalchange.bear.Count >= 5 && animalchange.elephant.Count >= 5)
            {
                p_buff = true;
            }
            else
            {
                p_buff = false;
            }
        }
        else
        {
            p_buff = false;
        }
    }

    void synergyOn()
    {
        playerName = GameObject.FindWithTag("Player");
        if (playerName.name == "deer(Clone)")
        {
            lightgreen = true;
            yellow = true;
            sky = true;
            orange = false;
            green = false;
            purple = false;
        }
        else if (playerName.name == "wolf(Clone)")
        {
            lightgreen = true;
            yellow = false;
            sky = false;
            orange = true;
            green = true;
            purple = false;
        }
        else if (playerName.name == "bear(Clone)")
        {
            lightgreen = false;
            yellow = true;
            sky = false;
            orange = true;
            green = false;
            purple = true;
        }
        else if (playerName.name == "elephant(Clone)")
        {
            lightgreen = false;
            yellow = false;
            sky = true;
            orange = false;
            green = true;
            purple = true;
        }
    }
}