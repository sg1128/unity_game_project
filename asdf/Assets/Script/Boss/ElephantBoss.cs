using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Spine.Unity;
public class ElephantBoss : MonoBehaviour
{
    GameObject player;
    public Vector2 target;
    Hpbar bossHpbar;
    public float moveSpeed;
    public bool skill1 = false;
    GameObject flag1, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9;
    public bool fight = false;
    float distance = 0f;
    int e_idx = -1;
    public Transform gogo;
    BossTrigger bTrigger;
    SkillTrigger sTrigger;
    public int num = 0;
    public List<GameObject> enemyFightList = new List<GameObject>();

    public SkeletonAnimation skeletonAnimation;
    public enum MonsterStates
    {
        Move1,
        Move2,
        Move3,
        Move4,
        Move5,
        MoveGo,
        Attack
    }
    private StateMachine<MonsterStates, StateDriverUnity> fsm;
    private void Awake()
    {
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        sTrigger = transform.GetChild(1).GetComponent<SkillTrigger>();
        bTrigger = transform.GetChild(0).GetComponent<BossTrigger>();
        bossHpbar = GetComponent<Hpbar>();
        fsm = new StateMachine<MonsterStates, StateDriverUnity>(this);
        num = Random.Range(1, 10);
        randomGo(num);
    }
    void Update()
    {
        fsm.Driver.Update.Invoke();
        moveSpeed = bossHpbar.Dex;
    }
    void Move1_Enter()
    {
        flag1 = GameObject.FindWithTag("Flag").transform.GetChild(1).gameObject;
    }
    void Move1_Update()
    {
        ElephantRun();
        if (sTrigger.enemylist.Count > 0)
        {
            enemyfight();
        }
        else
        {
            if (bTrigger.player.Count > 0)
            {
                fsm.ChangeState(MonsterStates.MoveGo);
            }
            else
            {
                Vector3 startPos = gameObject.transform.position;
                Vector3 finalPos = flag1.transform.position;

                if (fight == false)
                {
                    if (startPos.x - finalPos.x > 0)
                    {
                        gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                    }
                    else if (startPos.x - finalPos.x < 0)
                    {
                        gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, flag1.transform.position, moveSpeed * Time.deltaTime);
                if (flag1.GetComponent<Flag1>().inside)
                {
                    num = Random.Range(1, 5);
                    randomGo(num);
                }
            }
        }
    }
    void Move2_Enter()
    {
        flag2 = GameObject.FindWithTag("Flag").transform.GetChild(3).gameObject;
    }
    void Move2_Update()
    {
        ElephantRun();
        if (sTrigger.enemylist.Count > 0)
        {
            enemyfight();
        }
        else
        {
            if (bTrigger.player.Count > 0)
            {
                fsm.ChangeState(MonsterStates.MoveGo);
            }
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = flag2.transform.position;

            if (fight == false)
            {
                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, flag2.transform.position, moveSpeed * Time.deltaTime);
            if (flag2.GetComponent<Flag2>().inside)
            {
                num = Random.Range(1, 5);
                randomGo(num);
            }
        }
    }
    void Move3_Enter()
    {
        flag3 = GameObject.FindWithTag("Flag").transform.GetChild(5).gameObject;
    }
    void Move3_Update()
    {
        ElephantRun();
        if (sTrigger.enemylist.Count > 0)
        {
            enemyfight();
        }
        else
        {
            if (bTrigger.player.Count > 0)
            {
                fsm.ChangeState(MonsterStates.MoveGo);
            }
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = flag3.transform.position;

            if (fight == false)
            {
                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, flag3.transform.position, moveSpeed * Time.deltaTime);
            if (flag3.GetComponent<Flag3>().inside)
            {
                num = Random.Range(1, 5);
                randomGo(num);
            }
        }
    }
    void Move4_Enter()
    {
        flag4 = GameObject.FindWithTag("Flag").transform.GetChild(7).gameObject;
    }
    void Move4_Update()
    {
        ElephantRun();
        if (sTrigger.enemylist.Count > 0)
        {
            enemyfight();
        }
        else
        {
            if (bTrigger.player.Count > 0)
            {
                fsm.ChangeState(MonsterStates.MoveGo);
            }
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = flag4.transform.position;

            if (fight == false)
            {
                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, flag4.transform.position, moveSpeed * Time.deltaTime);
            if (flag4.GetComponent<Flag4>().inside)
            {
                num = Random.Range(1, 5);
                randomGo(num);
            }
        }
    }
    //void Move5_Enter()
    //{
    //    flag5 = GameObject.FindWithTag("Flag").transform.GetChild(4).gameObject;
    //    num = 5;
    //}
    //void Move5_Update()
    //{
    //    ElephantRun();
    //    if (sTrigger.enemylist.Count > 0)
    //    {
    //        enemyfight();
    //    }
    //    else
    //    {
    //        if (bTrigger.player.Count > 0)
    //        {
    //            fsm.ChangeState(MonsterStates.MoveGo);
    //        }
    //        Vector3 startPos = gameObject.transform.position;
    //        Vector3 finalPos = flag5.transform.position;

    //        if (fight == false)
    //        {
    //            if (startPos.x - finalPos.x > 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //            else if (startPos.x - finalPos.x < 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //        }
    //        transform.position = Vector2.MoveTowards(transform.position, flag5.transform.position, moveSpeed * Time.deltaTime);
    //        if (flag5.GetComponent<Flag5>().inside)
    //        {
    //            num = Random.Range(1, 10);
    //            randomGo(num);
    //        }
    //    }
    //}
    //void Move6_Enter()
    //{
    //    flag6 = GameObject.FindWithTag("Flag").transform.GetChild(5).gameObject;
    //}
    //void Move6_Update()
    //{
    //    ElephantRun();
    //    if (sTrigger.enemylist.Count > 0)
    //    {
    //        enemyfight();
    //    }
    //    else
    //    {
    //        if (bTrigger.player.Count > 0)
    //        {
    //            fsm.ChangeState(MonsterStates.MoveGo);
    //        }
    //        Vector3 startPos = gameObject.transform.position;
    //        Vector3 finalPos = flag6.transform.position;

    //        if (fight == false)
    //        {
    //            if (startPos.x - finalPos.x > 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //            else if (startPos.x - finalPos.x < 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //        }
    //        transform.position = Vector2.MoveTowards(transform.position, flag6.transform.position, moveSpeed * Time.deltaTime);
    //        if (flag6.GetComponent<Flag6>().inside)
    //        {
    //            num = Random.Range(1, 10);
    //            randomGo(num);
    //        }
    //    }
    //}
    //void Move7_Enter()
    //{
    //    flag7 = GameObject.FindWithTag("Flag").transform.GetChild(6).gameObject;
    //}
    //void Move7_Update()
    //{
    //    ElephantRun();
    //    if (sTrigger.enemylist.Count > 0)
    //    {
    //        enemyfight();
    //    }
    //    else
    //    {
    //        if (bTrigger.player.Count > 0)
    //        {
    //            fsm.ChangeState(MonsterStates.MoveGo);
    //        }
    //        Vector3 startPos = gameObject.transform.position;
    //        Vector3 finalPos = flag7.transform.position;

    //        if (fight == false)
    //        {
    //            if (startPos.x - finalPos.x > 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //            else if (startPos.x - finalPos.x < 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //        }
    //        transform.position = Vector2.MoveTowards(transform.position, flag7.transform.position, moveSpeed * Time.deltaTime);
    //        if (flag7.GetComponent<Flag7>().inside)
    //        {
    //            num = Random.Range(1, 10);
    //            randomGo(num);
    //        }
    //    }
    //}
    //void Move8_Enter()
    //{
    //    flag8 = GameObject.FindWithTag("Flag").transform.GetChild(7).gameObject;
    //}
    //void Move8_Update()
    //{
    //    ElephantRun();
    //    if (sTrigger.enemylist.Count > 0)
    //    {
    //        enemyfight();
    //    }
    //    else
    //    {
    //        if (bTrigger.player.Count > 0)
    //        {
    //            fsm.ChangeState(MonsterStates.MoveGo);
    //        }
    //        Vector3 startPos = gameObject.transform.position;
    //        Vector3 finalPos = flag8.transform.position;

    //        if (fight == false)
    //        {
    //            if (startPos.x - finalPos.x > 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //            else if (startPos.x - finalPos.x < 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //        }
    //        transform.position = Vector2.MoveTowards(transform.position, flag8.transform.position, moveSpeed * Time.deltaTime);
    //        if (flag8.GetComponent<Flag8>().inside)
    //        {
    //            num = Random.Range(1, 10);
    //            randomGo(num);
    //        }
    //    }
    //}
    //void Move9_Enter()
    //{
    //    flag9 = GameObject.FindWithTag("Flag").transform.GetChild(8).gameObject;
    //}
    //void Move9_Update()
    //{

    //    ElephantRun();
    //    if (sTrigger.enemylist.Count > 0)
    //    {
    //        enemyfight();
    //    }
    //    else
    //    {
    //        if (bTrigger.player.Count > 0)
    //        {
    //            fsm.ChangeState(MonsterStates.MoveGo);
    //        }
    //        Vector3 startPos = gameObject.transform.position;
    //        Vector3 finalPos = flag9.transform.position;

    //        if (fight == false)
    //        {
    //            if (startPos.x - finalPos.x > 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //            else if (startPos.x - finalPos.x < 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
    //            }
    //        }
    //        transform.position = Vector2.MoveTowards(transform.position, flag9.transform.position, moveSpeed * Time.deltaTime);
    //        if (flag9.GetComponent<Flag9>().inside)
    //        {
    //            num = Random.Range(1, 10);
    //            randomGo(num);
    //        }
    //    }
    //}
    void MoveGo_Enter()
    {
        num = 10;
    }
    void MoveGo_Update()
    {
        ElephantRun();
        //if (sTrigger.enemylist.Count > 0)
        //{
        //    enemyfight();
        //}
            player = GameObject.FindWithTag("Player");
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = player.transform.position;

            if (fight == false)
            {
                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            if (fight)
            {
                fsm.ChangeState(MonsterStates.Attack);
            }
    }
    void Attack_Enter()
    {
    }

    void Attack_Update()
    {
        if (!fight)
        {
            randomGo(num);
        }
        if (bossHpbar.nowHp <= bossHpbar.maxHp / 3 && !skill1)
        {
            bossHpbar.Def += 20f;
            skill1 = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team" || other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4")
        {
            fight = true;
            enemyFightList.Add(other.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team" || other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4")
        {
            enemyFightList.Remove(other.gameObject);
        }
        if (enemyFightList.Count == 0)
        {
            fight = false;
        }
    }

    void enemyfight()
    {
        distance = 0;
        for (int i = 0; i < sTrigger.enemylist.Count; i++)
        {
            if (distance < (sTrigger.enemylist[i].transform.position - transform.position).magnitude)
            {
                distance = (sTrigger.enemylist[i].transform.position - transform.position).magnitude;
                e_idx = i;
            }
        }
        if (!fight)
        {

            gogo = sTrigger.enemylist[e_idx].transform;
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = gogo.position;

            if (fight == false)
            {
                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, gogo.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }
    void randomGo(int num)
    {
        if (num == 1)
            fsm.ChangeState(MonsterStates.Move1);
        if (num == 2)
            fsm.ChangeState(MonsterStates.Move2);
        if (num == 3)
            fsm.ChangeState(MonsterStates.Move3);
        if (num == 4)
            fsm.ChangeState(MonsterStates.Move4);
        //if (num == 5)
        //    fsm.ChangeState(MonsterStates.Move5);
        //if (num == 6)
        //    fsm.ChangeState(MonsterStates.Move6);
        //if (num == 7)
        //    fsm.ChangeState(MonsterStates.Move7);
        //if (num == 8)
        //    fsm.ChangeState(MonsterStates.Move8);
        //if (num == 9)
        //    fsm.ChangeState(MonsterStates.Move9);
        if (num == 10)
            fsm.ChangeState(MonsterStates.MoveGo);
    }

    void ElephantRun()
    {
        if (!fight)
        {
            skeletonAnimation.AnimationName = "Elephant_Run";
        }
    }
}