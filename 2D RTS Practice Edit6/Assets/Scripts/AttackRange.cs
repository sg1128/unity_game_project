using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    UnitMovement unitmovement;
    DPS_fsm dps_fsm;
    private void Start()
    {
    }
    //void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("enemy"))
    //    {
    //        // target�� ������ Ÿ���� ����
    //        if (this.transform.parent.GetComponent<Attack_FSM>().target == null)
    //            this.transform.parent.GetComponent<Attack_FSM>().target = col.gameObject;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject == this.transform.parent.GetComponent<Attack_FSM>().target)
    //        this.transform.parent.GetComponent<Attack_FSM>().target = null;
    //}
}
