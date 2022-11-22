using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using MonsterLove.StateMachine;
public class E_Fight : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    E_Trigger etrigger;
    float distance = 0f;
    public bool E_fight = false;
    public bool enemyBump = false;
    Hpbar EnemyHpbar;
    int e_idx;
    public Vector3 spawnPos;
    public GameObject target;
    public bool arrive = false;
    public bool finish = false;
    bool findClose = false;
    public List<GameObject> enemyFightList = new List<GameObject>();
    public SkeletonAnimation skeletonAnimation;
    public enum MonsterStates
    {
        Move,
        FightC,
        FightNc,
        FightT,
        Attack
    }
    private StateMachine<MonsterStates, StateDriverUnity> fsm;
    private void Awake()
    {
        target = gameObject;
        rb = GetComponent<Rigidbody2D>();
        spawnPos = transform.position;
        etrigger = transform.GetChild(0).gameObject.GetComponent<E_Trigger>();
        EnemyHpbar = GetComponent<Hpbar>();
        fsm = new StateMachine<MonsterStates, StateDriverUnity>(this);
        fsm.ChangeState(MonsterStates.Move);
    }

    private void Update()
    {
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        moveSpeed = EnemyHpbar.Dex;
        fsm.Driver.Update.Invoke(); 
    }
    void Move_Enter()
    {
    }
    void Move_Update()
    {
        AtkToStop_E_Moving();
        if (!E_fight)
        {
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = spawnPos;
            if (enemyBump == false)
            {
                if (startPos.x - finalPos.x > 1)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (startPos.x - finalPos.x < 1)
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            transform.position = Vector2.MoveTowards(transform.position, spawnPos, moveSpeed * Time.deltaTime);
            if (transform.position == startPos && !finish)
            {
                arrive = true;
            }
        }
        else if (E_fight)
        {
            arrive = false;
            finish = false;
            if (etrigger.playerlist.Count > 0)
            {
                e_idx = 0;
                for (int i = etrigger.playerlist.Count-1; i >=0; i--)
                {
                    if (!etrigger.playerlist[i].GetComponent<Hpbar>().close)
                    {
                        e_idx = i;
                        findClose = false;
                        fsm.ChangeState(MonsterStates.FightNc);
                        break;
                    }
                    else if (etrigger.playerlist[i].GetComponent<Hpbar>().close)
                    {
                        e_idx = i;
                        findClose = true;
                    }
                }
                if (findClose)
                {
                    fsm.ChangeState(MonsterStates.FightC);
                }
            }
            else
            {
                fsm.ChangeState(MonsterStates.FightT);
            }
        }
    }
    void FightC_Enter()
    {

    }
    void FightC_Update()
    {
        AtkToStop_E_Moving();
        if (!E_fight)
        {
            fsm.ChangeState(MonsterStates.Move);
        }
        if (etrigger.playerlist.Count == 0)
        {
            fsm.ChangeState(MonsterStates.FightT);
        }
        if (e_idx < etrigger.playerlist.Count)
        {
            enemyFight(e_idx);
            transform.position = Vector2.MoveTowards(transform.position, etrigger.playerlist[e_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (enemyBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }
    void FightNc_Enter()
    {

    }
    void FightNc_Update()
    {
        AtkToStop_E_Moving();
        if (!E_fight)
        {
            fsm.ChangeState(MonsterStates.Move);
        }
        if (etrigger.playerlist.Count == 0)
        {
            fsm.ChangeState(MonsterStates.FightT);
        }
        for (int i = etrigger.playerlist.Count-1; i >=0; i--)
        {
            e_idx = 0;
            if (distance < (etrigger.playerlist[i].transform.position).magnitude)
            {
                distance = (etrigger.playerlist[i].transform.position).magnitude;
                e_idx = i;
            }
        }
        if (e_idx < etrigger.playerlist.Count)
        {
            enemyFight(e_idx);
            transform.position = Vector2.MoveTowards(transform.position, etrigger.playerlist[e_idx].transform.position, moveSpeed * Time.deltaTime);
        }
        if (enemyBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }

    void FightT_Enter()
    {
        target = gameObject;
    }
    void FightT_Update()
    {
        AtkToStop_E_Moving();
        if (!E_fight)
        {
            fsm.ChangeState(MonsterStates.Move);
        }
        if (etrigger.playerlist.Count > 0)
        {
            fsm.ChangeState(MonsterStates.Move);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = target.transform.position;
        if (enemyBump == false)
        {
            if (startPos.x - finalPos.x > 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (startPos.x - finalPos.x < 1)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (enemyBump)
        {
            fsm.ChangeState(MonsterStates.Attack);
        }
    }
    void Attack_Enter()
    {
    }
    void Attack_Update()
    {

        AtkToStop_E_Moving();
        if (!enemyBump)
        {
            fsm.ChangeState(MonsterStates.Move);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Bossunder")
        {
            enemyBump = true;
            enemyFightList.Add(other.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Bossunder")
        {
            enemyFightList.Remove(other.gameObject);
        }
        if (enemyFightList.Count == 0)
        {
            enemyBump = false;
        }
    }
    void AtkToStop_E_Moving()
    {
        if (enemyBump == false)
        {
            StartCoroutine("Cheak_E_Moving");
        }
        else
        {
            StopCoroutine("Cheak_E_Moving");
        }
    }
    private IEnumerator Cheak_E_Moving()
    {
        Vector3 startPos = gameObject.transform.position;

        yield return new WaitForSeconds(0.1f);

        Vector3 finalPos = gameObject.transform.position;

        if (startPos == finalPos || startPos == finalPos)
        {
            if (gameObject.name == "deer(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Idle";
            }
            else if (gameObject.name == "wolf(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Idle";
            }
            else if (gameObject.name == "bear(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Idle";
            }
            else if (gameObject.name == "elephant(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Idle";
            }
        }
        else if (startPos != finalPos || startPos != finalPos)
        {
            if (gameObject.name == "deer(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Run";
            }
            else if (gameObject.name == "wolf(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Run";
            }
            else if (gameObject.name == "bear(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Run";
            }
            else if (gameObject.name == "elephant(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Run";
            }
        }
    }

    void enemyFight(int idx)
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = etrigger.playerlist[idx].transform.position;
        if (enemyBump == false)
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