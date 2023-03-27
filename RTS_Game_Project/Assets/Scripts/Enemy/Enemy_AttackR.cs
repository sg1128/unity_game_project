using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackR : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Clickable"))
        {
            if (this.transform.parent.GetComponent<Enemy_FSM>().target == null)
                this.transform.parent.GetComponent<Enemy_FSM>().target = col.gameObject; //��ũ��Ʈ ���� ����� ������Ʈ �� ����
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == this.transform.parent.GetComponent<Enemy_FSM>().target)
            this.transform.parent.GetComponent<Enemy_FSM>().target = null;
    }
}

