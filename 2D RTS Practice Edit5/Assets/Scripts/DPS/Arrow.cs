using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject target;
    bool shoot = false;
    public int dmg;
    float speed = 5f;
    void Update()
    {   
        if(shoot)
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }

    public void Target_dmg(GameObject dps_target, int dps_dmg)
    {
        target = dps_target;
        dmg = dps_dmg;
        shoot = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (target == col.gameObject)
            {
                target.GetComponent<Enemy>().TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
