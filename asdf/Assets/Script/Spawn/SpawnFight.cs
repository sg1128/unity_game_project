using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFight : MonoBehaviour
{
    public List<GameObject> enemy = new List<GameObject>();
    public bool fight;
    SpawnGroup sg;
    public bool player = false;
    public bool boss = false;
    public bool bossenemy = false;
    public bool playerenemy = false;
    //bool esstop = false;
    EnemySpawn es;
    void Start()
    {
        sg = transform.parent.GetComponent<SpawnGroup>();
        es = transform.parent.GetComponent<EnemySpawn>();
        GetComponent<CircleCollider2D>().radius = es.rds * 2;
    }

    void Update()
    {

        if (fight)
        {
            if (enemy.Count == 0)
            {
                fight = false;
            }
            if (sg.group.Count > 0)
            {
                for (int i = sg.group.Count-1; i >=0; i--)
                {
                    es.pos_x.Remove(sg.group[i].transform.position.x);
                    es.pos_y.Remove(sg.group[i].transform.position.y);
                    if (player)
                    {
                        sg.group[i].GetComponent<E_Fight>().target = GameObject.FindWithTag("Player");
                    }
                    else if (boss)
                    {
                        sg.group[i].GetComponent<E_Fight>().target = GameObject.FindWithTag("Boss");
                    }
                    else if (bossenemy && !boss)
                    {
                        sg.group[i].GetComponent<E_Fight>().target = GameObject.FindWithTag("Boss");
                    }
                    else if (playerenemy && !player)
                        sg.group[i].GetComponent<E_Fight>().target = GameObject.FindWithTag("Player");
                    sg.group[i].GetComponent<E_Fight>().E_fight = true;
                }
            }
        }
        else
        {
            if (sg.group.Count > 0)
            {
                for (int i = 0; i < sg.group.Count; i++)
                {
                    sg.group[i].GetComponent<E_Fight>().E_fight = false;
                    if (sg.group[i].GetComponent<E_Fight>().arrive && !sg.group[i].GetComponent<E_Fight>().finish)
                    {
                        es.pos_x.Add(sg.group[i].transform.position.x);
                        es.pos_y.Add(sg.group[i].transform.position.y);
                        sg.group[i].GetComponent<E_Fight>().finish = true;
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "team" || other.tag == "Boss" || other.tag == "Bossunder")
        {
            fight = true;
            enemy.Add(other.gameObject);
            if (other.tag == "Player")
            {
                player = true;
            }
            else if (other.tag == "Boss")
            {
                boss = true;
            }
            else if (other.tag == "Bossunder")
            {
                bossenemy = true;
            }
            else if (other.tag == "team")
            {
                playerenemy = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "team" || other.tag == "Boss" || other.tag == "Bossunder")
        {
            enemy.Remove(other.gameObject);
            if (other.tag == "Player")
            {
                player = false;
            }
            else if (other.tag == "Boss")
            {
                boss = false;
            }
            else if (other.tag == "Bossunder")
            {
                bossenemy = false;
            }
            else if (other.tag == "team")
            {
                playerenemy = false;
            }
        }
        //if(other.tag == "enemy1")
        //{
        //    other.GetComponent<E_Fight>().E_fight = false;
        //}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "faint" || other.tag == "team" || other.tag == "Bossunder")
        {
            sg.group.Remove(other.gameObject);
        }
    }
}