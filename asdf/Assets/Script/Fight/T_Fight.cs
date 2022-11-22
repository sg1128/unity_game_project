using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using MonsterLove.StateMachine;
public class T_Fight : MonoBehaviour
{
    public float moveSpeed;
    public T_Trigger p_trigger;
    Hpbar playerHp;
    public int t_idx = -1;
    public bool T_fight = false;
    public bool teamBump = false;
    FollowUp t_follow;
    bool findClose = false;
    float distance;
    T_Trigger myTrigger;
    public List<GameObject> enemyFightList = new List<GameObject>();
    public SkeletonAnimation skeletonAnimation;
    public enum MonsterStates
    {
        Move,
        FightC,
        FightNc,
        Attack
    }
    private StateMachine<MonsterStates, StateDriverUnity> fsm;
    private void Awake()
    {
        t_follow = GetComponent<FollowUp>();
        p_trigger = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<T_Trigger>();
        fsm = new StateMachine<MonsterStates, StateDriverUnity>(this);
        fsm.ChangeState(MonsterStates.Move);
    }
    void Update()
    {
        fsm.Driver.Update.Invoke();
        moveSpeed = playerHp.Dex;
    }
    void Move_Enter()
    { 
        t_follow.enabled = true;
        teamBump = false;
        myTrigger = transform.GetChild(0).GetComponent<T_Trigger>();
    }
    void Move_Update()
    {
        AtkToStop_T_Moving();
        playerHp = GameObject.FindWithTag("Player").GetComponent<Hpbar>();
        p_trigger = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<T_Trigger>();
        if (p_trigger.enemylist.Count > 0)
        {
            t_idx = 0;
            for (int i = p_trigger.enemylist.Count-1; i>=0; i--)
            {
                if (p_trigger.enemylist.Count == 0)
                    break;
                if (p_trigger.enemylist[i].GetComponent<Hpbar>().close == false)
                {
                    t_idx = i;
                    findClose = false;
                    fsm.ChangeState(MonsterStates.FightNc);
                    break;
                }
                else
                {
                    t_idx = i;
                    findClose = true;
                }
            }
            if (findClose)
            {
                fsm.ChangeState(MonsterStates.FightC);
            }
        }
    }

    void FightC_Enter()
    {
        t_follow.enabled = false;
        teamBump = false;
    }
    void FightC_Update()
    {
        Run();
        playerHp = GameObject.FindWithTag("Player").GetComponent<Hpbar>();
        p_trigger = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<T_Trigger>();
        if (!p_trigger.T_battlestart)
        {
            T_fight = false;
            teamBump = false;
            fsm.ChangeState(MonsterStates.Move);
        }
        if (t_idx < p_trigger.enemylist.Count)
        {
            teamFight(t_idx);
            transform.position = Vector2.MoveTowards(transform.position, p_trigger.enemylist[t_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (teamBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }

    void FightNc_Enter()
    {
        t_follow.enabled = false;
        teamBump = false;
    }
    void FightNc_Update()
    {
        Run();
        playerHp = GameObject.FindWithTag("Player").GetComponent<Hpbar>();
        p_trigger = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<T_Trigger>();
        if (!p_trigger.T_battlestart)
        {
            T_fight = false;
            teamBump = false;
            fsm.ChangeState(MonsterStates.Move);
        }
        for (int i = p_trigger.enemylist.Count-1; i >=0; i--)
        {
            if (p_trigger.enemylist.Count == 0)
                break;
            t_idx = 0;
            T_fight = true;
            if (distance < (p_trigger.enemylist[i].transform.position).magnitude)
            {
                distance = (p_trigger.enemylist[i].transform.position).magnitude;
                t_idx = i;
            }
        }
        if (t_idx < p_trigger.enemylist.Count)
        {
            teamFight(t_idx);
            transform.position = Vector2.MoveTowards(transform.position, p_trigger.enemylist[t_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (teamBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }

    void Attack_Enter()
    {
        t_follow.enabled = false;
    }
    void Attack_Update()
    {
        AtkToStop_T_Moving();
        if (!teamBump)
        {
            fsm.ChangeState(MonsterStates.Move);
            teamBump = false;
        }
        if (!p_trigger.T_battlestart)
        {
            fsm.ChangeState(MonsterStates.Move);
            teamBump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Bossunder")
        {
            teamBump = true;
            enemyFightList.Add(other.gameObject);
            if (transform.tag == "team" && teamBump == true)
            {
                if (other.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    //transform.GetChild(0).GetComponent<SkeletonAnimation>().gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 1f);
                }
                else if (other.transform.position.x < transform.position.x)
                {
                    //transform.GetChild(0).GetComponent<SkeletonAnimation>().gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Bossunder")
        {
            enemyFightList.Remove(other.gameObject);
        }
        if(enemyFightList.Count == 0)
        {
            teamBump = false;
        }
    }

    void AtkToStop_T_Moving()
    {
        if (teamBump == false)
        {
            StartCoroutine("Cheak_T_Moving");
        }
        else
        {
            StopCoroutine("Cheak_T_Moving");
        }
    }

    void Run()
    {
        if (transform.GetComponent<Hpbar>().close == false)
        {
            StartCoroutine("GoFight");
        }
        else
        {
            StopCoroutine("GoFight");
        }
    }
    private IEnumerator Cheak_T_Moving()
    {
            yield return 0;
            myTrigger = transform.GetChild(0).GetComponent<T_Trigger>();
            skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();

            if (myTrigger.follow == false)
            {
                if (transform.GetChild(0).gameObject.name == "team_1(Clone)")
                {
                    skeletonAnimation.AnimationName = "Deer_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "team_2(Clone)")
                {
                    skeletonAnimation.AnimationName = "Wolf_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "team_3(Clone)")
                {
                    skeletonAnimation.AnimationName = "Bear_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "team_4(Clone)")
                {
                    skeletonAnimation.AnimationName = "Elephant_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "elephantAnima") // 임시 플레이어 정의목록
                {
                    skeletonAnimation.AnimationName = "Elephant_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "wolfAnima")
                {
                    skeletonAnimation.AnimationName = "Wolf_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "bearAnima")
                {
                    skeletonAnimation.AnimationName = "Bear_Idle";
                }
                else if (transform.GetChild(0).gameObject.name == "deerAnima")
                {
                    skeletonAnimation.AnimationName = "Deer_Idle";
                }
            }
            if (myTrigger.follow == true)
            {
                if (transform.GetChild(0).gameObject.name == "team_1(Clone)")
                {
                    skeletonAnimation.AnimationName = "Deer_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "team_2(Clone)")
                {
                    skeletonAnimation.AnimationName = "Wolf_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "team_3(Clone)")
                {
                    skeletonAnimation.AnimationName = "Bear_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "team_4(Clone)")
                {
                    skeletonAnimation.AnimationName = "Elephant_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "elephantAnima") // 임시 플레이어 정의목록
                {
                    skeletonAnimation.AnimationName = "Elephant_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "wolfAnima")
                {
                    skeletonAnimation.AnimationName = "Wolf_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "bearAnima")
                {
                    skeletonAnimation.AnimationName = "Bear_Run";
                }
                else if (transform.GetChild(0).gameObject.name == "deerAnima")
                {
                    skeletonAnimation.AnimationName = "Deer_Run";
                }
            }
    }

    private IEnumerator GoFight()
    {
        if (transform.GetChild(0).gameObject.name == "team_1(Clone)")
        {
            skeletonAnimation.AnimationName = "Deer_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "team_2(Clone)")
        {
            skeletonAnimation.AnimationName = "Wolf_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "team_3(Clone)")
        {
            skeletonAnimation.AnimationName = "Bear_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "team_4(Clone)")
        {
            skeletonAnimation.AnimationName = "Elephant_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "elephantAnima") // 임시 플레이어 정의목록
        {
            skeletonAnimation.AnimationName = "Elephant_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "wolfAnima")
        {
            skeletonAnimation.AnimationName = "Wolf_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "bearAnima")
        {
            skeletonAnimation.AnimationName = "Bear_Run";
        }
        else if (transform.GetChild(0).gameObject.name == "deerAnima")
        {
            skeletonAnimation.AnimationName = "Deer_Run";
        }
        yield return 0;
    }
    void teamFight(int idx)
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = p_trigger.enemylist[idx].transform.position;
        if (teamBump == false)
        {
            if (startPos.x - finalPos.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (startPos.x - finalPos.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}