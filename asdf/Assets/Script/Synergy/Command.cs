using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{
    public int deercmdTime;
    public int wolfcmdTime;
    public int bearcmdTime;
    public int elephantcmdTime;
    public float cmdTime;
    int deerStartTime = -30;
    int wolfStartTime = -80;
    int bearStartTime = -100;
    int elephantStartTime = -120;
    public bool cmdCurse = false;
    GameObject Player;

    public bool command1On = false, command2On = false, command3On = false, command4On = false;
    public bool wolfbuffon = false;
    public bool bearbuffon = false;
    public bool elephantbuffon = false;
    Animal_Change cmd_animal;

    public GameObject deerOrderAnim;
    public GameObject wolfOrderAnim;
    public GameObject bearOrderAnim;
    public GameObject elephantOrderAnim;
    Transform orderPos;
    void Start()
    {
        cmd_animal = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        deercmdTime = (int)Time.time - deerStartTime;
        wolfcmdTime = (int)Time.time - wolfStartTime;
        bearcmdTime = (int)Time.time - bearStartTime;
        elephantcmdTime = (int)Time.time - elephantStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindWithTag("Player");
        orderPos = Player.transform;
        deercmdTime = (int)Time.time - deerStartTime;
        wolfcmdTime = (int)Time.time - wolfStartTime;
        bearcmdTime = (int)Time.time - bearStartTime;
        elephantcmdTime = (int)Time.time - elephantStartTime;
        // »ç½¿ ----------------------------------------------------------------------------»ç½¿
        if (Player.name == "deer(Clone)")
        {
            if (wolfbuffon)
            {
                if (wolfcmdTime >= 20f && wolfbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().wolf == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().Dex -= 2f;
                                cmd_animal.asd[i].GetComponent<Bool>().wolf = false;
                            }
                        }
                    }
                    wolfbuffon = false;
                }
            }
            else if (bearbuffon)
            {
                if (bearcmdTime >= 20f && bearbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().bear == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().atkDmg -= 5f;
                                cmd_animal.asd[i].GetComponent<Bool>().bear = false;
                            }
                        }
                    }
                    bearbuffon = false;
                }
            }
            else if (elephantbuffon)
            {
                if (elephantcmdTime >= 15f && elephantbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().elephant == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().maxHp = cmd_animal.asd[i].GetComponent<Hpbar>().maxHp * (100f / 200f);
                                cmd_animal.asd[i].GetComponent<Hpbar>().nowHp = cmd_animal.asd[i].GetComponent<Hpbar>().nowHp * (100f / 200f);
                                cmd_animal.asd[i].GetComponent<Bool>().elephant = false;
                            }
                        }
                        elephantbuffon = false;
                    }
                }
            }
            cmdTime = Time.time - deerStartTime;
            if (deercmdTime >= 30f && cmdCurse == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    command1On = true;

                    OrderEffect(deerOrderAnim);

                    deerStartTime = (int)Time.time;
                    for (int i = 0; i < cmd_animal.asd.Count; i++)
                    {

                        if (cmd_animal.asd[i].GetComponent<Hpbar>().nowHp + 15f <= cmd_animal.asd[i].GetComponent<Hpbar>().maxHp)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().nowHp += 15f;
                        }
                        else if (cmd_animal.asd[i].GetComponent<Hpbar>().nowHp + 15f > cmd_animal.asd[i].GetComponent<Hpbar>().maxHp)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().nowHp = cmd_animal.asd[i].GetComponent<Hpbar>().maxHp;
                        }
                    }
                }
            }
        }

        // ´Á´ë ----------------------------------------------------------------------------´Á´ë
        if (Player.name == "wolf(Clone)")
        {
            if (bearbuffon)
            {
                if (bearcmdTime >= 20f && bearbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().bear == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().atkDmg -= 5f;
                                cmd_animal.asd[i].GetComponent<Bool>().bear = false;
                            }
                        }
                    }
                    bearbuffon = false;
                }
            }
            else if (elephantbuffon)
            {
                if (elephantcmdTime >= 15f && elephantbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().elephant == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().maxHp = cmd_animal.asd[i].GetComponent<Hpbar>().maxHp * (100f / 200f);
                                cmd_animal.asd[i].GetComponent<Hpbar>().nowHp = cmd_animal.asd[i].GetComponent<Hpbar>().nowHp * (100f / 200f);
                                cmd_animal.asd[i].GetComponent<Bool>().elephant = false;
                            }
                        }
                        elephantbuffon = false;
                    }
                }
            }
            cmdTime = Time.time - wolfStartTime;
            if (wolfcmdTime >= 80f && !wolfbuffon && cmdCurse == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wolfbuffon = true;
                    command2On = true;

                    OrderEffect(wolfOrderAnim);

                    wolfStartTime = (int)Time.time;
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().Dex += 2f;
                            cmd_animal.asd[i].GetComponent<Bool>().wolf = true;
                            cmd_animal.asd[i].GetComponent<Hpbar>().criStack = true;
                        }
                    }
                }
            }
            else if (wolfcmdTime >= 20f && wolfbuffon)
            {
                if (cmd_animal.asd.Count > 0)
                {
                    for (int i = 0; i < cmd_animal.asd.Count; i++)
                    {
                        if (cmd_animal.asd[i].GetComponent<Bool>().wolf == true)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().Dex -= 2f;
                            cmd_animal.asd[i].GetComponent<Bool>().wolf = false;
                        }
                    }
                }
                wolfbuffon = false;
            }
        }

        // °õ ----------------------------------------------------------------------------°õ
        if (Player.name == "bear(Clone)")
        {
            if (wolfbuffon)
            {
                if (wolfcmdTime >= 20f && wolfbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().wolf == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().Dex -= 2f;
                                cmd_animal.asd[i].GetComponent<Bool>().wolf = false;
                            }
                        }
                    }
                    wolfbuffon = false;
                }
            }
            else if (elephantbuffon)
            {
                if (elephantcmdTime >= 15f && elephantbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().elephant == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().maxHp = cmd_animal.asd[i].GetComponent<Hpbar>().maxHp * (100f / 200f);
                                cmd_animal.asd[i].GetComponent<Hpbar>().nowHp = cmd_animal.asd[i].GetComponent<Hpbar>().nowHp * (100f / 200f);
                                cmd_animal.asd[i].GetComponent<Bool>().elephant = false;
                            }
                        }
                        elephantbuffon = false;
                    }
                }
            }
            cmdTime = Time.time - bearStartTime;
            if (bearcmdTime >= 100f && !bearbuffon && cmdCurse == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OrderEffect(bearOrderAnim);

                    bearbuffon = true;
                    command3On = true;
                    bearStartTime = (int)Time.time;
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().atkDmg += 5f;
                            cmd_animal.asd[i].GetComponent<Bool>().bear = true;
                        }
                    }
                }
            }
            else if (bearcmdTime >= 20f && bearbuffon)
            {
                if (cmd_animal.asd.Count > 0)
                {
                    for (int i = 0; i < cmd_animal.asd.Count; i++)
                    {
                        if (cmd_animal.asd[i].GetComponent<Bool>().bear == true)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().atkDmg -= 5f;
                            cmd_animal.asd[i].GetComponent<Bool>().bear = false;
                        }
                    }
                }
                bearbuffon = false;
            }
        }

        // ÄÚ³¢¸® ----------------------------------------------------------------------------ÄÚ³¢¸®
        if (Player.name == "elephant(Clone)")
        {
            if (wolfbuffon)
            {
                if (wolfcmdTime >= 20f && wolfbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().wolf == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().Dex -= 2f;
                                cmd_animal.asd[i].GetComponent<Bool>().wolf = false;
                            }
                        }
                    }
                    wolfbuffon = false;
                }
            }
            else if (bearbuffon)
            {
                if (bearcmdTime >= 20f && bearbuffon)
                {
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            if (cmd_animal.asd[i].GetComponent<Bool>().bear == true)
                            {
                                cmd_animal.asd[i].GetComponent<Hpbar>().atkDmg -= 5f;
                                cmd_animal.asd[i].GetComponent<Bool>().bear = false;
                            }
                        }
                    }
                    bearbuffon = false;
                }
            }
            cmdTime = Time.time - elephantStartTime;
            if (elephantcmdTime >= 120f && !elephantbuffon && cmdCurse == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OrderEffect(elephantOrderAnim);

                    elephantbuffon = true;
                    command4On = true;
                    elephantStartTime = (int)Time.time;
                    if (cmd_animal.asd.Count > 0)
                    {
                        for (int i = 0; i < cmd_animal.asd.Count; i++)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().maxHp = cmd_animal.asd[i].GetComponent<Hpbar>().maxHp * (200f / 100f);
                            cmd_animal.asd[i].GetComponent<Hpbar>().nowHp = cmd_animal.asd[i].GetComponent<Hpbar>().nowHp * (200f / 100f);
                            cmd_animal.asd[i].GetComponent<Bool>().elephant = true;
                        }
                    }
                }
            }
            else if (elephantcmdTime >= 15f && elephantbuffon)
            {
                if (cmd_animal.asd.Count > 0)
                {
                    for (int i = 0; i < cmd_animal.asd.Count; i++)
                    {
                        if (cmd_animal.asd[i].GetComponent<Bool>().elephant == true)
                        {
                            cmd_animal.asd[i].GetComponent<Hpbar>().maxHp = cmd_animal.asd[i].GetComponent<Hpbar>().maxHp * (100f / 200f);
                            cmd_animal.asd[i].GetComponent<Hpbar>().nowHp = cmd_animal.asd[i].GetComponent<Hpbar>().nowHp * (100f / 200f);
                            cmd_animal.asd[i].GetComponent<Bool>().elephant = false;
                        }
                    }
                    elephantbuffon = false;
                }
            }
        }
    }

    private void OrderEffect(GameObject order)
    {
        GameObject inst = Instantiate(order, orderPos);
        Destroy(inst, 1.583f);

    }
}
