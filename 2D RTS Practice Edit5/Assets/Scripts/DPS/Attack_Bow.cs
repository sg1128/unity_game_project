using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bow : MonoBehaviour
{
    public GameObject arrow;
    DPS_fsm dps_fsm;
    Vector2 bow_vector;
    public GameObject target;
    Unit unit;
    void Start()
    {
        dps_fsm = gameObject.transform.parent.GetComponent<DPS_fsm>();
        unit = gameObject.transform.parent.GetComponent<Unit>();
    }
    private void Update()
    {
        target = dps_fsm.target;
        bow_vector.x = gameObject.transform.position.x;
        bow_vector.y = gameObject.transform.position.y;
    }   
    public void Shoot()
    {
        GameObject copyArrow = Instantiate(arrow, new Vector2(bow_vector.x, bow_vector.y), Quaternion.identity); //obj.transform.rotation - È¸Àü°ª
        copyArrow.GetComponent<Arrow>().Target_dmg(target, unit.dmg);
    }
}
