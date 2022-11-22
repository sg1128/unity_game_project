using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountLimite : MonoBehaviour
{
    public List<GameObject> S1 = new List<GameObject>();
    public List<GameObject> S2 = new List<GameObject>();
    public List<GameObject> deer = new List<GameObject>();
    public List<GameObject> wolf = new List<GameObject>();
    bool max = false;
    EnemySpawn es;
    void Start()
    {
        es = GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deer.Count >= S1.Count * 10&& !max)
        {
            if (S1.Count > 0)
            {
                for (int i = 0; i < S1.Count; i++)
                {
                    S1[i].GetComponent<EnemySpawn>().stop = true;
                    S1[i].GetComponent<EnemySpawn>().finish = true;
                }
            }
            max = true;
        }
        else if(deer.Count < S1.Count *10 && max)
        {
            for(int i=0; i<S1.Count; i++)
            {
                S1[i].GetComponent<EnemySpawn>().stop = false;
                S1[i].GetComponent<EnemySpawn>().spawnStart = Time.time;
            }
            max = false;
        }
        if (wolf.Count >= S2.Count * 15)
        {
            if (S2.Count > 0)
            {
                for (int i = 0; i < S2.Count; i++)
                {
                    S2[i].GetComponent<EnemySpawn>().CancelInvoke("SpawnNoEnemy");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Enemy1Spawner" || col.name == "TreeSpawner")
        {
            S1.Add(col.gameObject);
        }
        else if (col.name == "Enemy2Spawner")
        {
            S2.Add(col.gameObject);
        }

        if (col.tag == "enemy1")
        {
            deer.Add(col.gameObject);
        }
        else if (col.tag == "enemy2")
        {
            wolf.Add(col.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "enemy1")
        {
            deer.Remove(col.gameObject);
        }
        else if (col.tag == "enemy2")
        {
            wolf.Remove(col.gameObject);
        }
    }
}