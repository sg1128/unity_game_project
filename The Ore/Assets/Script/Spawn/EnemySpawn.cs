using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    SpawnGroup sg;
    public List<float> pos_x = new List<float>();
    public List<float> pos_y = new List<float>();
    int goodsp = -1;
    public float spawnTime;
    public int maxAnimal;
    float aniRange;
    bool reStart = false;
    public float startSpawn;
    public float rds;
    bool start = false;
    float startTime;
    float currentTime;
    public float endTime;
    public bool isEnd;
    public Vector2 size;
    public float respawnTime;
    public float spawnStart;
    public bool finish = false;
    Vector2 spawnPos;
    float posX;
    float posY;
    public bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        size.x = rds * 2;
        size.y = rds * 2;
        GetComponent<BoxCollider2D>().size = size;
        reStart = false;

        sg = gameObject.GetComponent<SpawnGroup>();
        if (gameObject.name == "Enemy1Spawner")
        {
            aniRange = 2.5f;
        }
        else if (gameObject.name == "Enemy2Spawner")
        {
            aniRange = 2.7f;
        }
        else if (gameObject.name == "Enemy3Spawner")
        {
            aniRange = 3.2f;
        }
        else if (gameObject.name == "Enemy4Spawner")
        {
            aniRange = 4.18f;
        }

        startTime = Time.time;
        //area = GetComponent<BoxCollider2D>();
        InvokeRepeating("SpawnNoEnemy", startSpawn, spawnTime);
        // 여기서 조절하면 됩니둥 (겜 시작 후 몇초 후에, 몇초 간격으로)
    }

    public void SpawnNoEnemy()
    {
        Vector3 spawnPos = GetRandomPosition();
        GameObject instance = Instantiate(Enemy, spawnPos, Quaternion.identity);
        sg.group.Add(instance);
    }

    Vector2 GetRandomPosition()
    {
        if (gameObject.name == "TreeSpawner")
        {
            if (sg.group.Count > 0)
            {
                for (int j = 0; j < 1; j++)
                {
                    Vector2 basePosition = new Vector2(0, 0);
                    if (goodsp == -1)
                    {
                        posX = basePosition.x + Random.Range(size.x / 2, -size.x / 2);
                        posY = basePosition.y + Random.Range(size.y / 2, -size.y / 2);
                        if ((posX <= 14 && posX >= -14) && (posY <= 19 && posY >= 0))
                        {
                            j--;
                        }
                        else if (posX > 14 || posX < -14 || posY > 19 || posY < 0)
                        {
                            spawnPos = new Vector2(posX, posY);
                            goodsp = 2;
                            return spawnPos;
                        }
                    }
                    else
                    {
                        basePosition = transform.position;
                        posX = basePosition.x + Random.Range(size.x / 2, -size.x / 2);
                        posY = basePosition.y + Random.Range(size.y / 2, -size.y / 2);
                        if ((posX <= 14 && posX >= -14) && (posY <= 19 && posY >= 0))
                        {
                            j--;
                        }
                        else if (posX > 14 || posX < -14 || posY > 19 || posY <0)
                        {
                            for (int i = 0; i < sg.group.Count; i++)
                            {
                                if (posX > sg.group[i].GetComponent<E_Fight>().spawnPos.x + aniRange || posX < sg.group[i].GetComponent<E_Fight>().spawnPos.x - aniRange || posY > sg.group[i].GetComponent<E_Fight>().spawnPos.y + aniRange || posY < sg.group[i].GetComponent<E_Fight>().spawnPos.y - aniRange)
                                {
                                    goodsp = 1;
                                }
                                else if (posX <= sg.group[i].GetComponent<E_Fight>().spawnPos.x + aniRange || posX >= sg.group[i].GetComponent<E_Fight>().spawnPos.x - aniRange || posY <= sg.group[i].GetComponent<E_Fight>().spawnPos.y + aniRange || posY >= sg.group[i].GetComponent<E_Fight>().spawnPos.y - aniRange)
                                {
                                    goodsp = 0;
                                    j--;
                                    break;
                                }
                            }
                            if (goodsp == 1)
                            {
                                goodsp = 2;
                                spawnPos = new Vector2(posX, posY);
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (goodsp == -1)
            {
                Vector2 basePosition = transform.position;
                posX = basePosition.x + Random.Range(size.x / 2, -size.x / 2);
                posY = basePosition.y + Random.Range(size.y / 2, -size.y / 2);
                spawnPos = new Vector2(posX, posY);
                goodsp = 2;
                return spawnPos;
            }
            if (sg.group.Count > 0)
            {
                for (int j = 0; j < 1; j++)
                {
                    Vector2 basePosition = transform.position;
                    posX = basePosition.x + Random.Range(size.x / 2, -size.x / 2);
                    posY = basePosition.y + Random.Range(size.y / 2, -size.y / 2);


                    for (int i = 0; i < sg.group.Count; i++)
                    {
                        if (posX > sg.group[i].GetComponent<E_Fight>().spawnPos.x + aniRange || posX < sg.group[i].GetComponent<E_Fight>().spawnPos.x - aniRange || posY > sg.group[i].GetComponent<E_Fight>().spawnPos.y + aniRange || posY < sg.group[i].GetComponent<E_Fight>().spawnPos.y - aniRange)
                        {
                            goodsp = 1;
                        }
                        else if (posX <= sg.group[i].GetComponent<E_Fight>().spawnPos.x + aniRange || posX >= sg.group[i].GetComponent<E_Fight>().spawnPos.x - aniRange || posY <= sg.group[i].GetComponent<E_Fight>().spawnPos.y + aniRange || posY >= sg.group[i].GetComponent<E_Fight>().spawnPos.y - aniRange)
                        {
                            goodsp = 0;
                            j--;
                            break;
                        }
                    }
                    if (goodsp == 1)
                    {
                        goodsp = 2;
                        spawnPos = new Vector2(posX, posY);
                        break;
                    }
                }
            }
        }
        return spawnPos;

    }

    // Update is called once per frame
    void Update()
    {
        // 몇초뒤에 꺼지는 코드
        if (isEnd)
        {
            currentTime = Time.time - startTime;

            if (currentTime >= startSpawn && !start)
            {
                start = true;
                startTime = Time.time;
            }
            if (start == true)
            {
                currentTime = Time.time - startTime;
                if (currentTime >= endTime)
                {
                    CancelInvoke("SpawnNoEnemy");
                    GetComponent<EnemySpawn>().enabled = false;
                }
            }
        }

        if (!stop)
        {
            // 만약 maxAnimal이 되면
            if (sg.group.Count >= maxAnimal && !reStart)
            {
                CancelInvoke("SpawnNoEnemy");
                finish = true;
                reStart = true;
            }
            if (sg.group.Count < maxAnimal)  // 근데 중간에 maxAnimal보다 적어지면
            {
                if (reStart)
                {
                    spawnStart = Time.time;
                    reStart = false;
                }
                else // respawnTime 이 spawnTime보다 크고 finish가 true이면 다시 시작
                {
                    respawnTime = Time.time - spawnStart;
                    if (respawnTime >= spawnTime)
                    {
                        spawnStart = Time.time;
                        if (finish)
                        {
                            finish = false;
                            Start();
                        }
                    }
                }
            }
        }
        else
        {
            CancelInvoke("SpawnNoEnemy");
        }
    }

    public void Rstart()
    {
        Start();
    }
}