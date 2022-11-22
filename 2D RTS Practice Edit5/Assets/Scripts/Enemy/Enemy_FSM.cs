using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class Enemy_FSM : MonoBehaviour
{
    Enemy enemy;
    public GameObject target;
    float DelayTime=1.0f;

    public bool fight = false;
    bool on = false;
    public enum CharacterStates
    {
        Idle,
        Move,
        Attack,
        Attacked,
        Death
    }
    private StateMachine<CharacterStates, StateDriverUnity> fsm;
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        fsm = new StateMachine<CharacterStates, StateDriverUnity>(this);
        fsm.ChangeState(CharacterStates.Idle);
    }

    void Update()
    {
        fsm.Driver.Update.Invoke();
    }

    void Idle_Enter()
    {
    }
    void Idle_Update()
    {
        if (fight)
        {
            if (on == false)
            {
                on = true;
                StartCoroutine(Enemy_Delay());
            }
        }
    }
    void Move_Enter()
    {
    }

    void Move_Update()
    {

    }
    void Attack_Enter()
    {
        StartCoroutine(Enemy_Attack());
    }
    void Attack_Update()
    {
    }


    void Death_Enter()
    {
        //Unit Destroy()
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Clickable"))
        {
            fight = true;
            fsm.ChangeState(CharacterStates.Attack);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Clickable"))
            fight = false;
    }

    IEnumerator Enemy_Attack()
    {
        // 공격하는 시간
        yield return new WaitForSeconds(1.0f);
        // 타겟이 있다면 타겟은 데미지를 받음
        if (target != null)
            target.GetComponent<Unit>().TakeDamage(enemy.dmg);
        fsm.ChangeState(CharacterStates.Idle);
        StopCoroutine(Enemy_Attack());
    }

    IEnumerator Enemy_Delay()
    {
        // 다음공격 까지 시간
        yield return new WaitForSeconds(DelayTime);
        on = false;
        fsm.ChangeState(CharacterStates.Attack);
        StopCoroutine(Enemy_Delay());
    }
}
