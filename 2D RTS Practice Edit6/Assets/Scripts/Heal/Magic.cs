using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject target;
    public bool shoot = false;
    public int dmg;
    float speed = 5f;
    void Update()
    {
        if (shoot)
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);

        if(target == null)
        {
            Destroy(this.gameObject);
        }
    }
    public void Target_dmg(GameObject heal_target, int heal_dmg)
    {
        target = heal_target;
        dmg = heal_dmg;
        target.GetComponent<Enemy>().projectile.Add(this.gameObject);
        shoot = true;
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (target == col.gameObject)
            {
                target.GetComponent<Enemy>().projectile.Remove(this.gameObject);
                target.GetComponent<Enemy>().TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
