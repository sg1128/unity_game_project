using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraFollow : MonoBehaviour
{
    GameObject player_p;
    GameObject pl;
    public GameObject ChangePlayer;
    Animal_Change animal;
    Transform AT;
    bool cameracheck = true;
    public int p_ani;
    int ctime = 20;
    public int coolTime;
    int p_r;
    int t_r;
    public bool aniChange = false;
    public int conAni = 0;
    public int didx = 0;
    public int widx = 0;
    public int bidx = 0;
    public int eidx = 0;
    public GameObject suceed;
    Transform playerSuceed;
    public bool shift = false;
    int startTime = -30;
    public bool first = false;
    public Command cmCool;

    void Start()
    {
        animal = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();

        p_ani = 0;
        player_p = GameObject.FindWithTag("Player");
        conAni = 2;
    }

    void Update()
    {
        p_r = animal.p_radius;
        t_r = animal.radius;
        if (animal.asd.Count > 1)
        {
            if (!first)
            {
                first_animal();
            }
            else
            {
                chang_animal();
            }
        }
        if (cameracheck)
        {
            transform.position = new Vector3(player_p.transform.position.x, player_p.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(AT.transform.position.x, AT.transform.position.y, transform.position.z);
        }
        coolTime = (int)Time.time - startTime;
        if (coolTime >= ctime && cmCool.cmdCurse == false)
        {
            if (p_ani != 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    player_p = GameObject.FindWithTag("Player");
                    if (player_p.GetComponent<UseSkill>().orderToStopAnim == false)
                    {
                        if ((player_p.gameObject.name == "deer(Clone)" && p_ani == 1) || (player_p.gameObject.name == "wolf(Clone)" && p_ani == 2) || (player_p.gameObject.name == "bear(Clone)" && p_ani == 3) || (player_p.gameObject.name == "elephant(Clone)" && p_ani == 4) || cmCool.cmdTime < 2)
                        {
                            Debug.Log("no");
                        }
                        else
                        {
                            shift = true;
                            conAni = p_ani;
                            startTime = (int)Time.time;
                            aniChange = true;
                            coolTime = 0;
                            pl = GameObject.FindWithTag("Player");
                            pl.tag = "team";
                            if (conAni == 1)
                            {
                                cameracheck = false;
                                animal.deer[didx].tag = "Player";
                                ChangePlayer = animal.deer[didx];
                                AT = ChangePlayer.transform;
                            }
                            if (conAni == 2)
                            {
                                cameracheck = false;
                                animal.wolf[widx].tag = "Player";
                                ChangePlayer = animal.wolf[widx];
                                AT = ChangePlayer.transform;
                            }
                            else if (conAni == 3)
                            {
                                cameracheck = false;
                                animal.bear[bidx].tag = "Player";
                                ChangePlayer = animal.bear[bidx];
                                AT = ChangePlayer.transform;
                            }
                            else if (conAni == 4)
                            {
                                cameracheck = false;
                                animal.elephant[eidx].tag = "Player";
                                ChangePlayer = animal.elephant[eidx];
                                AT = ChangePlayer.transform;
                            }
                            Suceed_EF();

                            foreach (var i in animal.asd)
                            {
                                i.GetComponent<FollowUp>().target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                                if (i.tag == "Player")
                                {
                                    player_p = i;
                                    i.GetComponent<T_Fight>().enabled = false;
                                    Player P = i.GetComponent<Player>();
                                    P.enabled = true;
                                    P.Stop();
                                    i.GetComponent<FollowUp>().enabled = false;
                                    i.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = p_r;
                                    i.transform.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                                    i.transform.GetComponent<UseSkill>().enabled = true;
                                    i.transform.GetComponent<Rigidbody2D>().mass = 300;
                                    i.transform.GetChild(0).GetComponent<Hp>().reHpbar();
                                    i.GetComponent<Player>().revive.Clear();
                                }
                                else
                                {
                                    i.GetComponent<T_Fight>().enabled = true;
                                    Player P = i.GetComponent<Player>();
                                    P.enabled = false;
                                    i.GetComponent<T_Fight>().p_trigger = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<T_Trigger>();
                                    i.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = t_r;
                                    i.GetComponent<FollowUp>().enabled = true;
                                    i.transform.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                                    i.transform.GetComponent<UseSkill>().enabled = false;
                                    i.transform.GetComponent<Rigidbody2D>().mass = 10;
                                    i.transform.GetChild(0).GetComponent<Hp>().reHpbar();
                                    i.GetComponent<Player>().revive.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    void Suceed_EF()
    {
        playerSuceed = GameObject.FindWithTag("Player").transform;
        GameObject inst = Instantiate(suceed, playerSuceed);
        Destroy(inst, 1.583f);
    }
    void chang_animal()
    {
        if (p_ani == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.elephant.Count > 0 && conAni != 4)
                {
                    p_ani = 4;
                }
                else
                {
                    if (animal.bear.Count > 0 && conAni != 3)
                    {
                        p_ani = 3;
                    }
                    else
                    {
                        if (animal.wolf.Count > 0 && conAni != 2)
                        {
                            p_ani = 2;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.wolf.Count > 0 && conAni != 2)
                {
                    p_ani = 2;
                }
                else
                {
                    if (animal.bear.Count > 0 && conAni != 3)
                    {
                        p_ani = 3;
                    }
                    else
                    {
                        if (animal.elephant.Count > 0 && conAni != 4)
                        {
                            p_ani = 4;
                        }
                    }
                }
            }
        }
        else if (p_ani == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.deer.Count > 0 && conAni != 1)
                {
                    p_ani = 1;
                }
                else
                {
                    if (animal.elephant.Count > 0 && conAni != 4)
                    {
                        p_ani = 4;
                    }
                    else
                    {
                        if (animal.bear.Count > 0 && conAni != 3)
                        {
                            p_ani = 3;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.bear.Count > 0 && conAni != 3)
                {
                    p_ani = 3;
                }
                else
                {
                    if (animal.elephant.Count > 0 && conAni != 4)
                    {
                        p_ani = 4;
                    }
                    else
                    {
                        if (animal.deer.Count > 0 && conAni != 1)
                        {
                            p_ani = 1;
                        }
                    }
                }
            }
        }
        else if (p_ani == 3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.wolf.Count > 0 && conAni != 2)
                {
                    p_ani = 2;
                }
                else
                {
                    if (animal.deer.Count > 0 && conAni != 1)
                    {
                        p_ani = 1;
                    }
                    else
                    {
                        if (animal.elephant.Count > 0 && conAni != 4)
                        {
                            p_ani = 4;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.elephant.Count > 0 && conAni != 4)
                {
                    p_ani = 4;
                }
                else
                {
                    if (animal.deer.Count > 0 && conAni != 1)
                    {
                        p_ani = 1;
                    }
                    else
                    {
                        if (animal.wolf.Count > 0 && conAni != 2)
                        {
                            p_ani = 2;
                        }
                    }
                }
            }
        }
        else if (p_ani == 4)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.bear.Count > 0 && conAni != 3)
                {
                    p_ani = 3;
                }
                else
                {
                    if (animal.wolf.Count > 0 && conAni != 2)
                    {
                        p_ani = 2;
                    }
                    else
                    {
                        if (animal.deer.Count > 0 && conAni != 1)
                        {
                            p_ani = 1;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.deer.Count > 0 && conAni != 1)
                {
                    p_ani = 1;
                }
                else
                {
                    if (animal.wolf.Count > 0 && conAni != 2)
                    {
                        p_ani = 2;
                    }
                    else
                    {
                        if (animal.bear.Count > 0 && conAni != 3)
                        {
                            p_ani = 3;
                        }
                    }
                }
            }
        }
    }
    void first_animal()
    {
        if (conAni == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.elephant.Count > 0 && conAni != 4)
                {
                    p_ani = 4;
                }
                else
                {
                    if (animal.bear.Count > 0 && conAni != 3)
                    {
                        p_ani = 3;
                    }
                    else
                    {
                        if (animal.wolf.Count > 0 && conAni != 2)
                        {
                            p_ani = 2;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.wolf.Count > 0 && conAni != 2)
                {
                    p_ani = 2;
                }
                else
                {
                    if (animal.bear.Count > 0 && conAni != 3)
                    {
                        p_ani = 3;
                    }
                    else
                    {
                        if (animal.elephant.Count > 0 && conAni != 4)
                        {
                            p_ani = 4;
                        }
                    }
                }
            }
        }
        else if (conAni == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.deer.Count > 0)
                {
                    p_ani = 1;
                }
                else
                {
                    if (animal.elephant.Count > 0)
                    {
                        p_ani = 4;
                    }
                    else
                    {
                        if (animal.bear.Count > 0)
                        {
                            p_ani = 3;
                        }
                    }
                }
                first = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.bear.Count > 0 && conAni != 3)
                {
                    p_ani = 3;
                }
                else
                {
                    if (animal.elephant.Count > 0 && conAni != 4)
                    {
                        p_ani = 4;
                    }
                    else
                    {
                        if (animal.deer.Count > 0 && conAni != 1)
                        {
                            p_ani = 1;
                        }
                    }
                }
                first = true;
            }
        }
        else if (conAni == 3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.wolf.Count > 0 && conAni != 2)
                {
                    p_ani = 2;
                }
                else
                {
                    if (animal.deer.Count > 0 && conAni != 1)
                    {
                        p_ani = 1;
                    }
                    else
                    {
                        if (animal.elephant.Count > 0 && conAni != 4)
                        {
                            p_ani = 4;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.elephant.Count > 0 && conAni != 4)
                {
                    p_ani = 4;
                }
                else
                {
                    if (animal.deer.Count > 0 && conAni != 1)
                    {
                        p_ani = 1;
                    }
                    else
                    {
                        if (animal.wolf.Count > 0 && conAni != 2)
                        {
                            p_ani = 2;
                        }
                    }
                }
            }
        }
        else if (conAni == 4)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (animal.bear.Count > 0 && conAni != 3)
                {
                    p_ani = 3;
                }
                else
                {
                    if (animal.wolf.Count > 0 && conAni != 2)
                    {
                        p_ani = 2;
                    }
                    else
                    {
                        if (animal.deer.Count > 0 && conAni != 1)
                        {
                            p_ani = 1;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (animal.deer.Count > 0 && conAni != 1)
                {
                    p_ani = 1;
                }
                else
                {
                    if (animal.wolf.Count > 0 && conAni != 2)
                    {
                        p_ani = 2;
                    }
                    else
                    {
                        if (animal.bear.Count > 0 && conAni != 3)
                        {
                            p_ani = 3;
                        }
                    }
                }
            }
        }
    }
}

