using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class Enemy_FSM : MonoBehaviour
{
    Enemy enemy;
    TargetList targetlist;
    public GameObject target;
    float DelayTime = 4f;
    float startTime = 0;
    public float currentTime = 0;
    public bool fight = false;
    bool on = false;
    public bool movement = false;
    public bool targetMove = false;
    public float shrtDis;
    public enum CharacterStates
    {
        Idle,
        Move,
        Attack,  //����
        Stand_by, //���� �� ��� �ð�
        Death
    }
    private StateMachine<CharacterStates, StateDriverUnity> fsm;
    void Start()
    {
        targetlist = GameObject.Find("TargetList").GetComponent<TargetList>();
        enemy = gameObject.GetComponent<Enemy>();
        fsm = new StateMachine<CharacterStates, StateDriverUnity>(this);
        fsm.ChangeState(CharacterStates.Idle);
    }

    void Update()
    {
        fsm.Driver.Update.Invoke();
        currentTime = Time.time - startTime;
        Target_Setting();
    }

    void Idle_Enter()
    {
        Debug.Log("IDLE");
    }
    void Idle_Update()
    {
        if (fight)
        {
            if (currentTime >= DelayTime)
            {
                if (on == false)
                {
                    on = true;
                    fsm.ChangeState(CharacterStates.Attack);
                }
            }
        }
        else
        {
            if (target != null)
                fsm.ChangeState(CharacterStates.Move);
            else
                fsm.ChangeState(CharacterStates.Idle);
        }

        //if (gameObject.GetComponent<Heal_Unitmovement>().arrived == false)
        //{
        //    fsm.ChangeState(CharacterStates.Move);
        //}

        //if (target == null)
        //{
        //    if (targetList.Count > 0)
        //    {
        //        target = targetList[0];
        //        fight = true;
        //    }
        //}
        //else
        //{
        //    FoundTarget();
        //}
    }
    void Move_Enter()
    {
        // ���߸� IDEL�� �ٲٴ� �ڵ�
        // Ÿ���� ������� ���ο� Ÿ���� ã�� �ڵ�
    }
    void Move_Update()
    {
        // ���߸� IDEL�� �ٲٴ� �ڵ�
        // Ÿ���� ������� ���ο� Ÿ���� ã�� �ڵ�
        if (target == null)
        {
            fsm.ChangeState(CharacterStates.Idle);
        }
        else
        {
            StartCoroutine(Enemy_move());
        }
    }
    void Attack_Enter()
    {
        Debug.Log("att");
        StopCoroutine(Enemy_move());
        StartCoroutine(Enemy_Attack());
    }
    void Attack_Update()
    {
        // �����̸� MOVE�� �ٲٴ� �ڵ�
        //if (gameObject.GetComponent<Heal_Unitmovement>().arrived == false)
        //{
        //    fsm.ChangeState(CharacterStates.Move);
        //}
    }

    IEnumerator Enemy_move()
    {
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, target.transform.position, 3 * Time.deltaTime);
        yield return 0;
    }

    IEnumerator Enemy_Attack()
    {
        startTime = Time.time;
        Debug.Log("attak");
        on = false;
        // �����ϴ� �ð�
        yield return new WaitForSeconds(1.0f);        // Ÿ���� �ִٸ� Ÿ���� �������� ����
       if(movement == false)
        {
            fsm.ChangeState(CharacterStates.Idle);
        }
        target.GetComponent<Unit>().TakeDamage(enemy.dmg);
        StopCoroutine(Enemy_Attack());
    }

    //IEnumerator Enemy_Delay()
    //{
    //    Debug.Log("delay");
    //    startTime = Time.time;
    //    // �������� ���� �ð�
    //    fsm.ChangeState(CharacterStates.Attack);
    //    yield return 0;
    //    StopCoroutine(Enemy_Delay());
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "dps" || col.gameObject.tag == "tank" || col.gameObject.tag == "heal")
        {
            targetMove = true;
            if (target == null)
            {
                target = col.gameObject;
            }
            fsm.ChangeState(CharacterStates.Move);
        }
    }
    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "dps" || col.gameObject.tag == "tank" || col.gameObject.tag == "heal")
    //    {
    //        fight = true;
    //        fsm.ChangeState(CharacterStates.Attack);
    //    }
    //}


    //���� ����κ��� �۵��� ����
    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    Debug.Log("!123");
    //   if(col == target)
    //    {
    //        targetMove = true;
    //        fsm.ChangeState(CharacterStates.Move);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "dps" || col.gameObject.tag == "tank" || col.gameObject.tag == "heal")
        {
            fight = true;
            fsm.ChangeState(CharacterStates.Idle);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "dps" || col.gameObject.tag == "tank" || col.gameObject.tag == "heal")
            fight = false;
    }

    void Target_Setting()
    {
        if (targetlist.targetAttack.Count > 0)
        {
            if (target == null)
            {
                shrtDis = Vector3.Distance(gameObject.transform.position, targetlist.targetAttack[0].transform.position);
                target = targetlist.targetAttack[0];
                foreach (GameObject found in targetlist.targetAttack)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                    if (Distance < shrtDis)
                    {
                        target = found;
                        shrtDis = Distance;
                    }
                }
                if(shrtDis >= 15)
                {
                    target = null;
                }
            }
        }
        else
        {
            target = null;
        }
    }
}
