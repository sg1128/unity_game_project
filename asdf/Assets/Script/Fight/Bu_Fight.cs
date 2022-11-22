using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Spine.Unity;
public class Bu_Fight : MonoBehaviour
{
    public float moveSpeed;
    public SkillTrigger sT;
    public bool buBump;
    public int bu_idx = 0;
    public bool bu_fight = false;
    public Transform gogo;
    Boss_F bu_follow;
    public GameObject BossSpeed;
    BossSpawn bospawn;
    float distance;
    bool findClose = false;
    public SkeletonAnimation skeletonAnimation;
    public List<GameObject> enemyFightList = new List<GameObject>();
    public enum MonsterStates
    {
        Move,
        FightC,
        FightNc,
        Attack,
        GoP
    }
    private StateMachine<MonsterStates, StateDriverUnity> fsm;
    private void Awake()
    {
        bu_follow = GetComponent<Boss_F>();
        sT = GameObject.FindWithTag("Boss").transform.GetChild(1).GetComponent<SkillTrigger>();
        fsm = new StateMachine<MonsterStates, StateDriverUnity>(this);
        fsm.ChangeState(MonsterStates.Move);
        bospawn = GameObject.Find("Boss").GetComponent<BossSpawn>();
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
    }
    void Update()
    {
        fsm.Driver.Update.Invoke();
    }
    void Move_Enter()
    {
        BossSpeed = GameObject.FindWithTag("Boss");
        moveSpeed = BossSpeed.GetComponent<Hpbar>().Dex + 1f;
        bu_follow.enabled = true;
        bu_fight = false;
    }
    void Move_Update()
    {
        Bu_Run();
        FindPlayer();
        BossDie();
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = BossSpeed.transform.position;
        if (buBump == false)
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
        if (sT.enemylist.Count > 0)
        {
            bu_idx = 0;
            for (int i = sT.enemylist.Count - 1; i >= 0; i--)
            {
                if (sT.enemylist.Count == 0)
                    break;
                if (sT.enemylist[i].GetComponent<Hpbar>().close == false)
                {
                    bu_idx = i;
                    findClose = false;
                    fsm.ChangeState(MonsterStates.FightNc);
                    break;
                }
                else
                {
                    bu_idx = i;
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
        BossSpeed = GameObject.FindWithTag("Boss");
        moveSpeed = BossSpeed.GetComponent<Hpbar>().Dex + 1f;
        bu_follow.enabled = false;
        bu_fight = false;
    }
    void FightC_Update()
    {
        Bu_Run();
        FindPlayer();
        BossDie();
        if (!sT.bu_battlestart)
        {
            bu_fight = false;
            buBump = false;
            fsm.ChangeState(MonsterStates.Move);
        }
        if (bu_idx < sT.enemylist.Count)
        {
            teamFight(bu_idx);
            transform.position = Vector2.MoveTowards(transform.position, sT.enemylist[bu_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (buBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }

    void FightNc_Enter()
    {
        BossSpeed = GameObject.FindWithTag("Boss");
        moveSpeed = BossSpeed.GetComponent<Hpbar>().Dex + 1f;
        bu_follow.enabled = false;
        buBump = false;
    }
    void FightNc_Update()
    {
        Bu_Run();
        FindPlayer();
        BossDie();
        if (!sT.bu_battlestart)
        {
            bu_fight = false;
            buBump = false;
            fsm.ChangeState(MonsterStates.Move);
        }
        for (int i = sT.enemylist.Count - 1; i >= 0; i--)
        {
            if (sT.enemylist.Count == 0)
                break;
            bu_idx = 0;
            bu_fight = true;
            if (distance < (sT.enemylist[i].transform.position).magnitude)
            {
                distance = (sT.enemylist[i].transform.position).magnitude;
                bu_idx = i;
            }
        }
        if (bu_idx < sT.enemylist.Count)
        {
            teamFight(bu_idx);
            transform.position = Vector2.MoveTowards(transform.position, sT.enemylist[bu_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (buBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }
    void GoP_Enter()
    {
        BossSpeed = GameObject.FindWithTag("Boss");
        moveSpeed = BossSpeed.GetComponent<Hpbar>().Dex-0.2f;
        bu_follow.enabled = false;
    }
    void GoP_Update() {
        Bu_Run();
        BossDie();
        GameObject player;
        player = GameObject.FindWithTag("Player");
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = player.transform.position;
        if (buBump == false)
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
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        if (buBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }
    void Attack_Enter()
    {
        bu_follow.enabled = false;
    }
    void Attack_Update()
    {
        if (!buBump)
        {
            fsm.ChangeState(MonsterStates.Move);
            buBump = false; 
        }
        if (!sT.bu_battlestart)
        {
            fsm.ChangeState(MonsterStates.Move);
            buBump = false;
        }
    }

    void BossDie()
    {
        if (bospawn.bossDie == true)
        {
            Destroy(transform.GetChild(0).GetComponent<Hp>().hpBar.gameObject);
            Destroy(gameObject);
        }
    }

    void FindPlayer()
    {
            if (BossSpeed.transform.GetChild(1).GetComponent<SkillTrigger>().pl == true)
            {
                fsm.ChangeState(MonsterStates.GoP);
            }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Player" || other.gameObject.tag == "team")
        {
            buBump = true;
            enemyFightList.Add(other.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Player" || other.gameObject.tag == "team")
        {
            enemyFightList.Remove(other.gameObject);
        }
        if (enemyFightList.Count == 0)
        {
            buBump = false;
        }
    }
    void Bu_Run()
    {
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        if (!buBump)
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
    void teamFight(int idx)
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = sT.enemylist[idx].transform.position;
        if (buBump == false)
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
    //void AtkToStop_bu_Moving()
    //{
    //    if (buBump == false)
    //    {
    //        StartCoroutine("Cheak_bu_Moving");
    //    }
    //    else
    //    {
    //        StopCoroutine("Cheak_bu_Moving");
    //    }
    //}
    //private IEnumerator Cheak_bu_Moving()
    //{
    //    Vector3 startPos = gameObject.transform.position;

    //    yield return new WaitForSeconds(0.1f);

    //    Vector3 finalPos = gameObject.transform.position;

    //    if (startPos == finalPos || startPos == finalPos)
    //    {
    //        if (transform.GetChild(0).gameObject.name == "deerAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Deer_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "wolfAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Wolf_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "bearAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Bear_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "elephantAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Elephant_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "deerAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Deer_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "wolfAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Wolf_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "bearAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Bear_Idle";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "elephantAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Elephant_Idle";
    //        }
    //    }
    //    else if (startPos != finalPos || startPos != finalPos)
    //    {
    //        if (transform.GetChild(0).gameObject.name == "deerAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Deer_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "wolfAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Wolf_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "bearAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Bear_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "elephantAnima")
    //        {
    //            skeletonAnimation.AnimationName = "Elephant_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "deerAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Deer_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "wolfAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Wolf_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "bearAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Bear_Run";
    //        }
    //        else if (transform.GetChild(0).gameObject.name == "elephantAnima(Clone)")
    //        {
    //            skeletonAnimation.AnimationName = "Elephant_Run";
    //        }
    //    }
    //}
}