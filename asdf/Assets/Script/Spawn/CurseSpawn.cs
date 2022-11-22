using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseSpawn : MonoBehaviour
{
    public Curse curse;
    public GameObject Curse_enemy;
    float startSpawn = 0.1f;
    float spawnTime = 0.1f;
    Vector2 size;
    bool stop = true;
    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        size.x = 20;
        size.y = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (curse.cur_count == 2 && stop == true)
        {
            stop = false;
            InvokeRepeating("SpawnNoEnemy", startSpawn, spawnTime);
        }
        if (i >= 11 && stop == false)
        {
            stop = true;
            CancelInvoke("SpawnNoEnemy");
        }
    }
    public void SpawnNoEnemy()
    {
        Vector3 spawnPos = GetRandomPosition();
        GameObject instance = Instantiate(Curse_enemy, spawnPos, Quaternion.identity);
        instance.tag = "CurseEnemy";
        instance.name = "curseEnemy";
        instance.GetComponent<Hpbar>().equalName();
        Destroy(instance.GetComponent<E_Fight>());
        Destroy(instance.GetComponent<Player>());
        Destroy(instance.GetComponent<FollowUp>());
        Destroy(instance.GetComponent<SNG_P>());
        Destroy(instance.GetComponent<SNG_S>());
        Destroy(instance.GetComponent<Bool>());
        Destroy(instance.GetComponent<UseSkill>());
        Destroy(instance.transform.GetChild(0).GetComponent<E_Trigger>());
        instance.transform.GetChild(0).gameObject.AddComponent<Cu_Trigger>();
        instance.AddComponent<Cu_Fight>();
        i++;
    }
    Vector2 GetRandomPosition()
    {

        Vector2 basePosition = transform.position;
        float posX = basePosition.x + Random.Range(size.x / 2, -size.x / 2);
        float posY = basePosition.y + Random.Range(size.y / 2, -size.y / 2);
        Vector2 spawnPos = new Vector2(posX, posY);
        return spawnPos;
    }
}
