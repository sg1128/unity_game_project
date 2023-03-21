using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Sword : MonoBehaviour
{
    public GameObject target;
    Tank_fsm tank_fsm;
    Unit unit;
    void Start()
    {
        tank_fsm = gameObject.transform.parent.GetComponent<Tank_fsm>();
        unit = gameObject.transform.parent.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        target = tank_fsm.target;
        if(target == null)
        {
            StopCoroutine(Attack_Dmg());
        }
    }

    public void Swing()
    {
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x , transform.position.y-1), Time.deltaTime * 1);
        StartCoroutine(Attack_Dmg());

    }

    IEnumerator Attack_Dmg()
    {
        yield return new WaitForSeconds(1.0f);
        if(target != null)
            target.GetComponent<Enemy>().TakeDamage(unit.dmg);
        StopCoroutine(Attack_Dmg());
    }
}
