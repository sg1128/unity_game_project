using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Wand : MonoBehaviour
{
    public GameObject magic;
    Heal_fsm heal_fsm;
    Vector2 wand_vector;
    public GameObject target;
    //public GameObject dps_target;
    Unit unit;
    void Start()
    {
        heal_fsm = gameObject.transform.parent.GetComponent<Heal_fsm>();
        unit = gameObject.transform.parent.GetComponent<Unit>();
    }
    private void Update()
    {
        target = heal_fsm.target;
        wand_vector.x = gameObject.transform.position.x;
        wand_vector.y = gameObject.transform.position.y;
    }
    public void Shoot()
    {
        GameObject copyMagic = Instantiate(magic, new Vector2(wand_vector.x, wand_vector.y), Quaternion.identity); //obj.transform.rotation - ȸ����
        copyMagic.GetComponent<Magic>().Target_dmg(target, unit.dmg);
        unit.nowMp += 5;
    }
}