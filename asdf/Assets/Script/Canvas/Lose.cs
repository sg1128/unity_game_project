using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    BossSpawn boss;
    public GameObject deer_kill;
    public GameObject wolf_kill;
    public GameObject bear_kill;
    public GameObject time;
    public GameObject curTime;
    public bool escStop = false;
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        escStop = true;
        time.SetActive(true);
        curTime.SetActive(false);
        if (boss.deerdie == true)
        {
            deer_kill.SetActive(true);
        }
        else
        {
            deer_kill.SetActive(false);
        }
        if (boss.wolfdie == true)
        {
            wolf_kill.SetActive(true);
        }
        else
        {
            wolf_kill.SetActive(false);
        }
        if (boss.beardie == true)
        {
            bear_kill.SetActive(true);
        }
        else
        {
            bear_kill.SetActive(false);
        }
    }
}