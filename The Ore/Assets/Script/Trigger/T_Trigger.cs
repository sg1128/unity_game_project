using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Trigger : MonoBehaviour
{
    public bool T_battlestart = false;
    public bool b_state = false;
    public bool follow = false;
    public bool Inside = false;
    public List<GameObject> teamlist = new List<GameObject>();
    public List<GameObject> enemylist = new List<GameObject>();
    Animal_Change ani_chg;
    CircleCollider2D trigger;
    void Start()
    {
        ani_chg = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        trigger = transform.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (gameObject.transform.parent.tag == "team")
        {
            T_battlestart = false;
            enemylist.Clear();
            teamlist.Clear();
            trigger.radius = ani_chg.radius;
            if (GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<T_Trigger>().T_battlestart == false)
            {
                transform.parent.GetComponent<Hpbar>().close = false;
            }
        }
        else if (gameObject.transform.parent.tag == "Player")
        {
            follow = false;
            trigger.radius = ani_chg.p_radius;
            if (!T_battlestart)
            {
                transform.parent.GetComponent<Hpbar>().close = false; 
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (enemylist.Contains(other.gameObject))
        {
            return;
        }
        if (teamlist.Contains(other.gameObject))
        {
            return;
        }
        if (gameObject.transform.parent.tag == "Player")
        {
            if (other.tag == "team")
            {
                teamlist.Add(other.gameObject);
            }
            if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4" || other.tag == "Boss" || other.tag == "Bossunder" || other.tag == "CurseEnemy")
            {
                T_battlestart = true;
                enemylist.Add(other.gameObject);
            }
            if (other.tag == "faint")
            {
                enemylist.Remove(other.gameObject);
            }
        }
            if (gameObject.transform.parent.tag == "team")
            {
                if (other.tag == "Player")
                {
                    follow = false;
                }

            }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.transform.parent.tag == "Player")
        {
            if (other.tag == "team")
            {
                other.transform.GetChild(0).GetComponent<T_Trigger>().follow = true;
                teamlist.Remove(other.gameObject);
            }
            if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4" || other.tag == "Boss" || other.tag == "Bossunder" || other.tag == "CurseEnemy")
            {
                enemylist.Remove(other.gameObject);
            }
            if (enemylist.Count == 0)
            {
                T_battlestart = false;
            }
        }
        if (gameObject.transform.parent.tag == "faint")
        {
            if (other.tag == "Player")
            {
                transform.parent.GetComponent<Hpbar>().Inside = false;
            }
        }
    }
}
