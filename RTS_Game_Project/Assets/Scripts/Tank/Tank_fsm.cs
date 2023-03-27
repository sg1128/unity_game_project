using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
public class Tank_fsm : MonoBehaviour
{
    public Attack_Sword attack_sword;
    public Tank_UnitMovement tank_unitmovement;
    public GameObject target;
    public List<GameObject> targetList = new List<GameObject>();
    private Camera myCam;
    float MaxDistance = 15f;
    Vector3 MousePosition;
    public bool aclick = false;
    public bool fight = false;
    float DelayTime = 4f;
    bool on = false;
    bool player_move = false;
    float startTime = 0;
    public float currentTime = 0;
    Unit unit;

    public enum CharacterStates
    {
        Idle,
        Move,
        Attack,
        Death
    }
    private StateMachine<CharacterStates, StateDriverUnity> fsm;
    void Awake()
    {
        myCam = Camera.main;
        unit = GetComponent<Unit>();
        tank_unitmovement = gameObject.GetComponent<Tank_UnitMovement>();
        fsm = new StateMachine<CharacterStates, StateDriverUnity>(this);
        fsm.ChangeState(CharacterStates.Idle);
    }
    void Update()
    {
        fsm.Driver.Update.Invoke();
        currentTime = Time.time - startTime;
        SwitchingTime();
        if (target != null)
        {
            if (target.GetComponent<Enemy>().nowHp <= 0)
            {
                target = null;
            }
            if (!target.GetComponent<Enemy>().player.Contains(this.gameObject))
            {
                target.GetComponent<Enemy>().player.Add(this.gameObject);
            }
        }
        if (tank_unitmovement.selected)
        {
            if (aclick == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MousePosition = Input.mousePosition;
                    MousePosition = myCam.ScreenToWorldPoint(MousePosition);

                    RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

                    if (hit)
                    {
                        if (hit.collider.gameObject.tag == "enemy")    //��Ʈ�� ������Ʈ�� �±װ� enemy�� ���
                        {
                            target = hit.collider.gameObject;       //Ÿ������ ����
                        }
                        if (!fight)
                        {
                            if (target != null)
                                tank_unitmovement.GoEnemy();
                        }
                    }
                    else
                    {
                        // ���� ���� �ƴϰ� ���� Ŭ���ϸ� Ÿ���� �ʱ�ȭ
                        if (!fight)
                        {
                            target = null;
                        }
                    }

                    aclick = false;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    target = null;
                    fight = false;
                }
            }
        }
    }
    void Idle_Enter()
    {
        player_move = false;
        tank_unitmovement.PlayerStop();
    }
    void Idle_Update()
    {
        if (gameObject.GetComponent<Tank_UnitMovement>().arrived == false)
        {
            fsm.ChangeState(CharacterStates.Move);
        }

        // ��Ŭ������ �̵��ϴٰ� ������ �� �ֺ��� ���� ������ �ο�
        // ���� ����� ���� target���� �ٲ�� �ϴ°��� ��ǥ
        if (target == null)
        {
            if (targetList.Count > 0)
            {
                target = targetList[0];
                fight = true;
            }
        }
        // ����µ� Ÿ���� �������� �i�ư���
        else
        {
            FoundTarget();
        }
        //�ڽĿ�����Ʈ Ʈ���� �ȿ� target�� ������
        if (fight)
        {
            if (currentTime >= DelayTime)
            {
                if (on == false)
                {
                    on = true;
                    StartCoroutine(Delay());
                }
            }
        }

    }
    void Move_Enter()
    {
        player_move = true;
        //Debug.Log("�̵�");
        StopCoroutine(Attack());
    }

    void Move_Update()
    {
        if (gameObject.GetComponent<Tank_UnitMovement>().arrived == true)
        {
            fsm.ChangeState(CharacterStates.Idle);
        }
        //�̵��ϴ� ���߿��� �������� ��� �i�ư�
        if (target != null)
        {
            FoundTarget();
        }
        if (targetList.Count > 0)
        {
            if (tank_unitmovement.only_move == false)
            {
                player_move = false;
                tank_unitmovement.PlayerStop();
            }
        }
    }
    void Attack_Enter()
    {
        StartCoroutine(Attack());
    }
    void Attack_Update()
    {
        if (gameObject.GetComponent<Tank_UnitMovement>().arrived == false)
        {
            fsm.ChangeState(CharacterStates.Move);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            targetList.Add(col.gameObject);
            // aclick�̸� �ο�
            if (tank_unitmovement.only_move == false)
            {
                fight = true;
                fsm.ChangeState(CharacterStates.Idle);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            targetList.Remove(col.gameObject);
            //if (col.gameObject == target)
            //{
            //    target = null;
            //}
            fight = false;
        }
    }

    // target�� �ִµ� target�� �������� ����� �i�ư���
    void FoundTarget()
    {
        if (target != null)
        {
            bool foundenemy = false;
            for (int i = targetList.Count - 1; i >= 0; i--)
            {
                if (target == targetList[i])
                {
                    foundenemy = true;
                    break;
                }
            }

            if (!foundenemy)
            {
                tank_unitmovement.GoEnemy();
            }
        }
    }

    IEnumerator Attack()
    {
        on = false;
        // �����ϴ� �ð�
        yield return new WaitForSeconds(1.0f);
        if (player_move == false)
        {
            attack_sword.Swing();
            // Ÿ���� �ִٸ� Ÿ���� �������� ����
            //if(target != null)
            //target.GetComponent<Enemy>().TakeDamage(unit.dmg);
            fsm.ChangeState(CharacterStates.Idle);
        }
        StopCoroutine(Attack());
    }

    IEnumerator Delay()
    {
        startTime = Time.time;
        // �������� ���� �ð�
        fsm.ChangeState(CharacterStates.Attack);
        yield return 0;
        StopCoroutine(Delay());
    }



    void SwitchingTime() //unit��ũ��Ʈ���� attacking�� ��ȯ(������ Ȯ��)
    {
        if (currentTime >= DelayTime)
        {
            unit.attacking = false;
        }
        else
        {
            if (fight)
                unit.attacking = true;
        }
    }
}
