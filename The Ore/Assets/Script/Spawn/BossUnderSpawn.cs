using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnderSpawn : MonoBehaviour
{
    public GameObject Enemy;
    BossUnderGroup bug;
    public List<float> pos_x = new List<float>();
    public List<float> pos_y = new List<float>();
    CircleCollider2D spawnRange;
    int goodsp = -1;
    public float spawnTime;
    public int maxAnimal;
    float aniRange;
    public int startSpawn;
    public Vector2 size;

    void Start()
    {
        GetComponent<BoxCollider2D>().size = size;
        spawnRange = GetComponent<CircleCollider2D>();
        bug = GetComponent<BossUnderGroup>();
        if (gameObject.name == "Bossunder1Spawner")
        {
            aniRange = 1.6f;
        }
        else if (gameObject.name == "Bossunder2Spawner")
        {
            aniRange = 2f;
        }
        else if (gameObject.name == "Bossunder3Spawner")
        {
            aniRange = 2.6f;
        }
        else if (gameObject.name == "Bossunder4Spawner")
        {
            aniRange = 3f;
        }

        //area = GetComponent<BoxCollider2D>();
        InvokeRepeating("SpawnNoEnemy", startSpawn, spawnTime);
        // 여기서 조절하면 됩니둥 (겜 시작 후 몇초 후에, 몇초 간격으로)
    }

    void SpawnNoEnemy()
    {
        for (int i = 0; i < maxAnimal; i++)
        {
            Vector3 spawnPos = GetRandomPosition();
            GameObject instance = Instantiate(Enemy, spawnPos, Quaternion.identity);
        }
        CancelInvoke("SpawnNoEnemy");
    }

    Vector2 GetRandomPosition()
    {
        while (true)
        {
            Vector2 basePosition = transform.position;

            float posX = basePosition.x + Random.Range(size.x/2, -size.x/2);
            float posY = basePosition.y + Random.Range(size.y/2, -size.y/2);

            if (goodsp == -1)
            {
                pos_x.Add(posX);
                pos_y.Add(posY);
                Vector2 spawnPos = new Vector2(posX, posY);
                goodsp = 2;
                return spawnPos;
            }

            for (int i = 0; i < pos_x.Count; i++)
            {
                if (posX > pos_x[i] + aniRange || posX < pos_x[i] - aniRange || posY > pos_y[i] + aniRange || posY < pos_y[i] - aniRange)
                {
                    goodsp = 1;
                }
                else if (posX <= pos_x[i] + aniRange || posX >= pos_x[i] - aniRange || posY <= pos_y[i] + aniRange || posY >= pos_y[i] - aniRange)
                {
                    goodsp = 0;
                    break;
                }
            }

            if (goodsp == 1)
            {
                goodsp = 2;
                pos_x.Add(posX);
                pos_y.Add(posY);
                Vector2 spawnPos = new Vector2(posX, posY);
                return spawnPos;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
