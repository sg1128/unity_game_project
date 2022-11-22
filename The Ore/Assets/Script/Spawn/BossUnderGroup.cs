using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnderGroup : MonoBehaviour
{
    public List<GameObject> group = new List<GameObject>();
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (group.Contains(other.gameObject))
        {
            return;
        }
        if (gameObject.name == "Bossunder1Spawner")
        {
            if (other.tag == "Bossunder")
                group.Add(other.gameObject);
        }
        else if (gameObject.name == "Bossunder2Spawner")
        {
            if (other.tag == "Bossunder")
                group.Add(other.gameObject);
        }
        else if (gameObject.name == "Bossunder3Spawner")
        {
            if (other.tag == "Bossunder")
                group.Add(other.gameObject);
        }
        else if (gameObject.name == "Bossunder4Spawner")
        {
            if (other.tag == "Bossunder")
                group.Add(other.gameObject);
        }
    }
}
