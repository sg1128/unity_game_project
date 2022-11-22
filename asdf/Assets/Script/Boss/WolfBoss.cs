using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Spine.Unity;
public class WolfBoss : MonoBehaviour
{

    public Vector2 target;
    Hpbar bossHpbar;
    float startTime;
    float chasecoolTime;
    public float moveSpeed;
    bool skill1 = false, skill2 = false, skill3 = false;
    GameObject player;
    public bool fight = false;
    float distance = 0f;
    int w_idx = -1;
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
        moveSpeed = bossHpbar.Dex;
    }

    void Move_Enter()
    {
        num = 1;
        startTime = Time.time;
    }

    void Move_Update()
    {
        WolfRun();
        player = GameObject.FindWithTag("Player");
        if (player.GetComponent<Hpbar>().nowHp <= player.GetComponent<Hpbar>().maxHp / 2 && !skill1)
        {
            bossHpbar.Dex += 2f;
            skill1 = true;
        }
        if ((player.GetComponent<Hpbar>().nowHp <= player.GetComponent<Hpbar>().maxHp / 3 && !skill2) || skill2)
        {
            fsm.ChangeState(MonsterStates.Move1);
        }

        if (sTrigger.enemylist.Count == 0) // 적이 없을때
        {
            wolfTarget();
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
        }
        else if (sTrigger.enemylist.Count > 0)//sT에 적이 있으면 적한테 가기
        {
            distance = 0;
            for (int i = 0; i < sTrigger.enemylist.Count; i++)
            {
                if (distance < (sTrigger.enemylist[i].transform.position - transform.position).magnitude)
                {
                    distance = (sTrigger.enemylist[i].transform.position - transform.position).magnitude;
                    w_idx = i;
                }
            }
            if (!fight)
            {
                wolfGogo(w_idx);
                gogo = sTrigger.enemylist[w_idx].transform;
                transform.position = Vector2.MoveTowards(transform.position, gogo.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                fsm.ChangeState(MonsterStates.Attack);
            }
        }
    }

    void Move1_Enter()
    {
        num = 2;
        skill2 = true;
    }

    void Move1_Update()
    {
        WolfRun();
        player = GameObject.FindWithTag("Player");
        if (player.GetComponent<Hpbar>().nowHp <= player.GetComponent<Hpbar>().maxHp / 2 && !skill1)
        {
            bossHpbar.Dex += 2f;
            skill1 = true;
        }
        if (sTrigger.enemylist.Count == 0) // 적이 없을때
        {
            wolfPlayer();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            distance = 0;
            for (int i = 0; i < sTrigger.enemylist.Count; i++)
            {
                if (distance < (sTrigger.enemylist[i].transform.position - transform.position).magnitude)
                {
                    distance = (sTrigger.enemylist[i].transform.position - transform.position).magnitude;
                    w_idx = i;
                }
            }
            if (!fight)
            {
                wolfGogo(w_idx);
                gogo = sTrigger.enemylist[w_idx].transform;
                transform.position = Vector2.MoveTowards(transform.position, gogo.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                fsm.ChangeState(MonsterStates.Attack);
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
            bossHpbar.atkSpeed += 0.3f;
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
    void wolfGogo(int idx)
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
    void wolfTarget()
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
    void wolfPlayer()
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

    void WolfRun()
    {
        if (!fight)
        {
            skeletonAnimation.AnimationName = "Wolf_Run";
        }
    }
}