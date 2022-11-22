using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNG_S : MonoBehaviour
{
    Synergy_S S_s;
    Hpbar status;
    float debuffDex = 0f;
    Animal_Change animalchange;
    Radius enemyRadius;
    Animal_Change animal_change;
    public bool delete = false;
    public bool lg_finish, s_finish, y_finish, o_finish, g_finish, p_finish;
    Player p_bs;
    bool p_on = false, s_on = false, y_on = false, g_on = false;
    bool y_ok = false, lg_ok = false;
    int deercri, wolfcri, bearcri, elephantcri;
    Synergy_P S_p;
    void Start()
    {
        enemyRadius = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).GetComponent<Radius>();
        S_s = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Synergy_S>();
        S_p = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Synergy_P>();
        animal_change = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animal_Change>();
        status = GetComponent<Hpbar>();
        lg_finish = false;
        s_finish = false;
        y_finish = false;
        o_finish = false;
        g_finish = false;
        p_finish = false;
        if (gameObject.name == "deer(Clone)")
            deercri = 25;
        if (gameObject.name == "wolf(Clone)")
            wolfcri = 10;
        if (gameObject.name == "bear(Clone)")
            bearcri = 5;
        if (gameObject.name == "elephant(Clone)")
            elephantcri = 2;
    }

    // Update is called once per frame
    void Update()
    {
        p_bs = GameObject.FindWithTag("Player").GetComponent<Player>();
        //if (p_bs.b_state && fight_on)
        //{
        //    status.Dex -= 0.5f;
        //    fight_on = false;
        //}
        //else if (!p_bs.b_state && !fight_on)
        //{
        //    status.Dex += 0.5f;
        //    fight_on = true;
        //}


        // 복수-----------------------------------------------------------
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (S_s.lg_buff)
            {
                if (!lg_finish)
                {
                    if (p_bs.b_state)
                    {
                        if (S_p.h_buff1)
                        {
                            if (gameObject.name == "deer(Clone)")
                                status.cri = deercri/2 + 5 * S_s.crinum;
                            if (gameObject.name == "wolf(Clone)")
                                status.cri = wolfcri/2 + 5 * S_s.crinum;
                            if (gameObject.name == "bear(Clone)")
                                status.cri = bearcri/2 + 5 * S_s.crinum;
                            if (gameObject.name == "elephant(Clone)")
                                status.cri = elephantcri/2 + 5 * S_s.crinum;
                        }
                        else
                        {
                            if (gameObject.name == "deer(Clone)")
                                status.cri = deercri + 5 * S_s.crinum;
                            if (gameObject.name == "wolf(Clone)")
                                status.cri = wolfcri + 5 * S_s.crinum;
                            if (gameObject.name == "bear(Clone)")
                                status.cri = bearcri + 5 * S_s.crinum;
                            if (gameObject.name == "elephant(Clone)")
                                status.cri = elephantcri + 5 * S_s.crinum;
                        }
                        lg_ok = true;
                    }
                    else if (!p_bs.b_state)
                    {
                        if (S_p.h_buff1)
                        {
                            if (gameObject.name == "deer(Clone)")
                                status.cri = 25/2;
                            if (gameObject.name == "wolf(Clone)")
                                status.cri = 10/2;
                            if (gameObject.name == "bear(Clone)")
                                status.cri = 5/2;
                            if (gameObject.name == "elephant(Clone)")
                                status.cri = 2/2;
                        }
                        else
                        {
                            if (gameObject.name == "deer(Clone)")
                                status.cri = 25;
                            if (gameObject.name == "wolf(Clone)")
                                status.cri = 10;
                            if (gameObject.name == "bear(Clone)")
                                status.cri = 5;
                            if (gameObject.name == "elephant(Clone)")
                                status.cri = 2;
                        }
                        S_s.crinum = 0;
                    }
                }
            }
            else
            {
                if (lg_ok)
                    lg_finish = true;
                if (lg_finish)
                {
                    if (S_p.h_buff1)
                        {
                            if (gameObject.name == "deer(Clone)")
                                status.cri = 25/2;
                            if (gameObject.name == "wolf(Clone)")
                                status.cri = 10/2;
                            if (gameObject.name == "bear(Clone)")
                                status.cri = 5/2;
                            if (gameObject.name == "elephant(Clone)")
                                status.cri = 2/2;
                        }
                        else
                        {
                            if (gameObject.name == "deer(Clone)")
                                status.cri = 25;
                            if (gameObject.name == "wolf(Clone)")
                                status.cri = 10;
                            if (gameObject.name == "bear(Clone)")
                                status.cri = 5;
                            if (gameObject.name == "elephant(Clone)")
                                status.cri = 2;
                        }
                    S_s.crinum = 0;
                    lg_finish = false;
                }
            }
        }
        // -생존 ---------------------------------------------------------------------
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (S_s.y_buff)
            {
                if (!y_finish)
                {
                    if (!p_bs.b_state && !y_on)
                    {
                        if (gameObject.name == "deer(Clone)")
                            status.maxHp += 4f;
                        if (gameObject.name == "wolf(Clone)")
                            status.maxHp += 3f;
                        if (gameObject.name == "bear(Clone)")
                            status.maxHp += 2f;
                        if (gameObject.name == "elephant(Clone)")
                            status.maxHp += 1f;
                        y_on = true;
                        y_ok = true;
                    }
                    else if (p_bs.b_state && y_on)
                    {
                        y_on = false;
                    }
                }
            }
            else
            {
                if (y_ok)
                {
                    y_finish = true;
                    y_ok = false;
                }
                if (y_finish)
                {
                    if (gameObject.name == "deer(Clone)")
                        status.maxHp = 50f;
                    if (gameObject.name == "wolf(Clone)")
                        status.maxHp = 60f;
                    if (gameObject.name == "bear(Clone)")
                        status.maxHp = 100f;
                    if (gameObject.name == "elephant(Clone)")
                        status.maxHp = 150f;
                    y_finish = false;
                }
            }
        }
        // 직감 ---------------------------------------------------------------------
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (S_s.s_buff)
            {
                if (!s_finish)
                {
                    if ((enemyRadius.wolf.Count > 0 || enemyRadius.bear.Count > 0) && !s_on)
                    {
                        status.Dex += 2f;
                        s_on = true;
                    }
                    else if ((enemyRadius.wolf.Count == 0 && enemyRadius.bear.Count == 0) && s_on)
                    {
                        status.Dex -= 2f;
                        s_on = false;
                    }
                }
            }
            else
            {
                if (s_on)
                    s_finish = true;
                if (s_finish)
                {
                    if (s_on)
                    {
                        status.Dex -= 2f;
                        s_on = false;
                        s_finish = false;
                    }
                }
            }
        }
        // 사냥-------------------------------------------------------------------------
        if (S_s.o_buff)
        {
            if (!o_finish)
            {
                if (gameObject.tag == "faint")
                {
                    if (gameObject.name == "deer(Clone)" || gameObject.name == "elephant(Clone")
                    {
                        for (int i = 0; i < animal_change.asd.Count; i++)
                        {
                            Hpbar healhp = animal_change.asd[i].GetComponent<Hpbar>();
                            if (healhp.nowHp + 6 <= healhp.maxHp)
                                healhp.nowHp += 6f;
                            else
                                healhp.nowHp = healhp.maxHp;
                        }
                    }
                    else if (gameObject.name == "bear(Clone)" || gameObject.name == "wolf(Clone)")
                    {
                        for (int i = 0; i < animal_change.asd.Count; i++)
                        {
                            Hpbar healhp = animal_change.asd[i].GetComponent<Hpbar>();
                            if (healhp.nowHp + 3 <= healhp.maxHp)
                                healhp.nowHp += 3f;
                            else
                                healhp.nowHp = healhp.maxHp;
                        }
                    }
                    o_finish = true;
                }
            }
        }
        // 위협 ------------------------------------------------------------------------
        if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
        {
            if (S_s.g_buff)
            {
                if (!g_finish)
                {
                    if (status.inRadius && !g_on)
                    {
                        debuffDex = status.Dex - 2f;
                        if (debuffDex >= 1)
                        {
                            status.Dex -= 2f;
                        }
                        else
                        {
                            status.Dex = 1f;
                        }
                        g_on = true;
                    }
                    else if (!status.inRadius && g_on)
                    {
                        if (debuffDex >= 1)
                        {
                            status.Dex += 2f;
                        }
                        else
                        {
                            status.Dex += debuffDex + 2f;
                        }
                        g_on = false;
                    }
                }
            }
            else
            {
                if (g_on)
                    g_finish = true;
                if (g_finish)
                {
                    if (g_on)
                    {
                        status.Dex += 2f;
                        g_on = false;
                    }
                }
            }
        }
        // 압도 ----------------------------------------------------------------------------
        if (S_s.p_buff)
        {

            if (!p_finish)
            {
                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
                {
                    if (status.inRadius && !p_on)
                    {
                        status.atkDmg -= 2f;
                        p_on = true;
                    }
                    else if (!status.inRadius && p_on)
                    {
                        status.atkDmg += 2f;
                        p_on = false;
                    }
                }
            }
        }
        else
        {
            if (p_on)
                p_finish = true;
            if (p_finish)
            {
                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
                {
                    if (p_on == true)
                    {
                        status.atkDmg += 2f;
                        p_on = false;
                    }
                    p_finish = false;
                }
            }
        }
    }
}
