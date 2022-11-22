using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Spine.Unity;
public class BearBoss : MonoBehaviour
{
    public Vector2 target;
    Hpbar bossHpbar;
    float startTime;
    float chasecoolTime;
    public float moveSpeed;
    public bool skill1 = false, skill2 = false, skill3 = false;
    GameObject player;
    public bool fight = false;
    float distance = 0f;
    int b_idx = -1;
    public Transform gogo;
    BossTrigger bTrigger;
    SkillTrigger sTrigger;
    public int num = 0;
    public List<GameObject> enemyFightList = new List<GameObject>();
    public SkeletonAnimation skeletonAnimation;
    public enum MonsterStates
    {
        Move,
        Move1,
        Move2,
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
        fsm.ChangeState(MonsterStates.Move);
    }

    void Update()
    {
        fsm.Driver.Update.Invoke();
    }
    void Move_Enter()
    {
        moveSpeed = bossHpbar.Dex;
        target = GameObject.FindWithTag("Player").transform.position;
        startTime = Time.time;
        num = 1;
    }
    void Move_Update()
    {
        BearRun();

        if (!skill1)
        {
            if (sTrigger.enemylist.Count > 0)
            {
                distance = 0;
                for (int i = 0; i < sTrigger.enemylist.Count; i++)
                {
                    if (distance < (sTrigger.enemylist[i].transform.position - transform.position).magnitude)
                    {
                        distance = (sTrigger.enemylist[i].transform.position - transform.position).magnitude;
                        b_idx = i;
                    }
                }
                if (!fight)
                {
                    bearGogo(b_idx);
                    gogo = sTrigger.enemylist[b_idx].transform;
                    transform.position = Vector2.MoveTowards(transform.position, gogo.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    fsm.ChangeState(MonsterStates.Attack);
                }
            }
            else
            {
                player = GameObject.FindWithTag("Player"); // 플레이어를 게속 찾아야한다.
                bearTarget();
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                if (transform.position.x == target.x && transform.position.y == target.y)
                {
                    startTime = Time.time;
                    target = GameObject.FindWithTag("Player").transform.position;
                }
                chasecoolTime = Time.time - startTime;
                if (chasecoolTime >= 60)
                {
                    startTime = Time.time;
                    target = GameObject.FindWithTag("Player").transform.position;
                }
                if (bTrigger.player.Count > 0) // 첫번째 콜라이더에 플레이어가 들어왔을때
                {
                    fsm.ChangeState(MonsterStates.Move1);
                }
            }
        }
        else
        {
            fsm.ChangeState(MonsterStates.Move1);
        }
    }


    void Move1_Enter() //즉시추적
    {
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = 90;
        skill1 = true;
        num = 2;
    }
    void Move1_Update()
    {
        BearRun();

        if (sTrigger.enemylist.Count > 0)
        {
            distance = 0;
            for (int i = 0; i < sTrigger.enemylist.Count; i++)
            {
                if (distance < (sTrigger.enemylist[i].transform.position - transform.position).magnitude)
                {
                    distance = (sTrigger.enemylist[i].transform.position - transform.position).magnitude;
                    b_idx = i;
                }
            }
            if (!fight)
            {
                bearGogo(b_idx);
                gogo = sTrigger.enemylist[b_idx].transform;
                transform.position = Vector2.MoveTowards(transform.position, gogo.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                fsm.ChangeState(MonsterStates.Attack);
            }
        }
        else  //sT 콜라이더에 적이 없으면 플레이어를 따라가기
        {
            player = GameObject.FindWithTag("Player"); // 플레이어를 게속 찾아야한다.
            bearPlayer();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            if (bTrigger.player.Count > 0) // 2번째 콜라이더에 플레이어가 있으면 속도 증가
            {
                if (!skill2)
                {
                    bossHpbar.Dex += 2f;
                    skill2 = true;
                }
            }
            else if (bTrigger.player.Count == 0)
            {
                if (skill2)
                {
                    bossHpbar.Dex -= 2f;
                    skill2 = false;
                }
            }

        }
    }
    void Attack_Enter()
    {
    }

    void Attack_Update()
    {
        if (!fight)
            fsm.ChangeState(MonsterStates.Move);
        if (bossHpbar.nowHp <= bossHpbar.maxHp / 3 && !skill3)
        {
            bossHpbar.atkDmg += 5f;
            skill3 = true;
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
    void bearGogo(int idx)
    {
        moveSpeed = bossHpbar.Dex;
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = sTrigger.enemylist[idx].transform.position;

        if (fight == false)
        {
            if (startPos.x - finalPos.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
            }
            else if (startPos.x - finalPos.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1.75f, 1.75f, 1);
            }
        }
    }
    void bearTarget()
    {
        moveSpeed = bossHpbar.Dex;
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = target;

        if (fight == false)
        {
            if (startPos.x - finalPos.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1);
            }
            else if (startPos.x - finalPos.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1.75f, 1.75f, 1);
            }
        }
    }
    void bearPlayer()
    {
        moveSpeed = bossHpbar.Dex;
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
                gameObject.transform.localScale = new Vector3(-1.75f, 1.75f, 1);
            }
        }
    }

    void BearRun()
    {
        if (!fight)
        {
            skeletonAnimation.AnimationName = "Bear_Run";
        }
    }
}