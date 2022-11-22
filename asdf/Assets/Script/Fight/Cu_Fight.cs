using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using MonsterLove.StateMachine;
public class Cu_Fight : MonoBehaviour
{
    // Start is called before the first frame update
    public SkeletonAnimation skeletonAnimation;
    GameObject player;
    bool cuBump;
    public List<GameObject> enemylist = new List<GameObject>();
    Cu_Trigger CT;
    float moveSpeed;
    Hpbar myhpbar;
    int cu_idx;
    bool findClose = false;
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
        CT = transform.GetChild(0).GetComponent<Cu_Trigger>();
        myhpbar = GetComponent<Hpbar>();
        fsm = new StateMachine<MonsterStates, StateDriverUnity>(this);
        fsm.ChangeState(MonsterStates.Move);
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Driver.Update.Invoke();
    }

    void Move_Enter()
    {
        moveSpeed = myhpbar.Dex;
    }
    void Move_Update()
    {
        Cu_Run();
        player = GameObject.FindWithTag("Player");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        if (CT.enemylist.Count > 0)
        {
                fsm.ChangeState(MonsterStates.FightC);
        }
    }
    void FightC_Enter()
    {
    }
    void FightC_Update()
    {
        Cu_Run();
        if (CT.enemylist.Count ==0)
        {
            cuBump = false;
            fsm.ChangeState(MonsterStates.Move);
        }
        if (cu_idx < CT.enemylist.Count)
        {
            teamFight(cu_idx);
            transform.position = Vector2.MoveTowards(transform.position, CT.enemylist[cu_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (cuBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }

    //void FightNc_Enter()
    //{
    //    cuBump = false;
    //}
    //void FightNc_Update()
    //{
    //    Cu_Run();
    //    if (CT.enemylist.Count == 0)
    //    {
    //        cuBump = false;
    //        fsm.ChangeState(MonsterStates.Move);
    //    }
    //    for (int i = CT.enemylist.Count - 1; i >= 0; i--)
    //    {
    //        if (CT.enemylist.Count == 0)
    //            break;
    //        cu_idx = 0;
    //        bu_fight = true;
    //        if (distance < (CT.enemylist[i].transform.position).magnitude)
    //        {
    //            distance = (CT.enemylist[i].transform.position).magnitude;
    //            cu_idx = i;
    //        }
    //    }
    //    if (cu_idx < CT.enemylist.Count)
    //    {
    //        teamFight(cu_idx);
    //        transform.position = Vector2.MoveTowards(transform.position, CT.enemylist[cu_idx].transform.position, moveSpeed * Time.deltaTime);
    //    }
    //    if (cuBump)
    //    {
    //        fsm.ChangeState(MonsterStates.Attack);
    //    }
    //}
    void Attack_Enter()
    {
    }
    void Attack_Update()
    {
        if (!cuBump)
        {
            fsm.ChangeState(MonsterStates.Move);
            cuBump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Player" || other.gameObject.tag == "team")
        {
            cuBump = true;
            enemylist.Add(other.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Player" || other.gameObject.tag == "team")
        {
            enemylist.Remove(other.gameObject);
        }
        if (enemylist.Count == 0)
        {
            cuBump = false;
        }
    }

    void teamFight(int idx)
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = CT.enemylist[idx].transform.position;
        if (cuBump == false)
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
    void Cu_Run()
    {
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        if (!cuBump)
        {
            if (transform.GetChild(0).gameObject.name == "deerAnima")
            {
                skeletonAnimation.AnimationName = "Deer_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima")
            {
                skeletonAnimation.AnimationName = "Bear_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima")
            {
                skeletonAnimation.AnimationName = "Elephant_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "deerAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Run";
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Run";
            }
        }
    }
}
