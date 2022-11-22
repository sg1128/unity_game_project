using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{
    public List<GameObject> group = new List<GameObject>();
    SpawnFight sf;
    EnemySpawn ES;
    void Start()
    {
        sf = transform.GetChild(0).GetComponent<SpawnFight>();
        ES = GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = group.Count-1; i >=0; i--)
        {
            if (group[i].GetComponent<Hpbar>().nowHp <= 0)
            {
                group.Remove(group[i]);
            }
        }
        for (var i = group.Count - 1; i > -1; i--)
        {
            if (group[i] == null)
                group.RemoveAt(i);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.x < (ES.size.x) / 2 && other.transform.position.x > -(ES.size.x) / 2 && other.transform.position.y < (ES.size.y) / 2 && other.transform.position.y > -(ES.size.y) / 2)
        {
            if (group.Contains(other.gameObject))
            {
                return;
            }
            if (!sf.fight)
            {
                if (gameObject.name == "Enemy1Spawner")
                {
                    if (other.tag == "enemy1")
                        group.Add(other.gameObject);
                }
                else if (gameObject.name == "Enemy2Spawner")
                {
                    if (other.tag == "enemy2")
                        group.Add(other.gameObject);
                }
                else if (gameObject.name == "Enemy3Spawner")
                {
                    if (other.tag == "enemy3")
                        group.Add(other.gameObject);
                }
                else if (gameObject.name == "Enemy4Spawner")
                {
                    if (other.tag == "enemy4")
                        group.Add(other.gameObject);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.tag == "faint" || gameObject.tag == "Bossunder")
        {
            group.Remove(other.gameObject);
        }
    }
}
