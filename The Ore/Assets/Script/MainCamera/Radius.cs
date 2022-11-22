using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour
{
    public List<GameObject> enemy_group = new List<GameObject>();
    public List<GameObject> deer = new List<GameObject>();
    public List<GameObject> wolf = new List<GameObject>();
    public List<GameObject> bear = new List<GameObject>();
    public List<GameObject> elephant = new List<GameObject>();
    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4")
        {
            enemy_group.Add(other.gameObject);
            if (other.name == "deer(Clone)")
            {
                deer.Add(other.gameObject);
            }
            else if (other.name == "wolf(Clone)")
            {
                wolf.Add(other.gameObject);
            }
            else if (other.name == "bear(Clone)")
            {
                bear.Add(other.gameObject);
            }
            else if (other.name == "elephant(Clone)")
            {
                elephant.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Bossunder" || other.gameObject.tag =="CurseEnemy")
        {
            other.gameObject.GetComponent<Hpbar>().inRadius = true;
        }
        if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4")
        {
            other.gameObject.GetComponent<Hpbar>().inRadius = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4")
        {
            enemy_group.Remove(other.gameObject);
            other.gameObject.GetComponent<Hpbar>().inRadius = false;
            if (other.name == "deer(Clone)")
            {
                deer.Remove(other.gameObject);
            }
            else if (other.name == "wolf(Clone)")
            {
                wolf.Remove(other.gameObject);
            }
            else if (other.name == "bear(Clone)")
            {
                bear.Remove(other.gameObject);
            }
            else if (other.name == "elephant(Clone)")
            {
                elephant.Remove(other.gameObject);
            }
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Bossunder" || other.gameObject.tag == "faint" || other.gameObject.tag == "CurseEnemy")
        {
            other.gameObject.GetComponent<Hpbar>().inRadius = false;
        }
    }
}