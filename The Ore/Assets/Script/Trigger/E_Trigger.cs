using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Trigger : MonoBehaviour
{
    public List<GameObject> playerlist = new List<GameObject>();
    public List<GameObject> enemyTeam = new List<GameObject>();
    public bool E_battlestart = false;
    public bool teamfight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "team" || other.tag == "Player" || other.tag =="Boss" || other.tag == "Bossunder")
            {
                E_battlestart = true;
                playerlist.Add(other.gameObject);
            }
        if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4")
        {
            enemyTeam.Add(other.gameObject);
            other.transform.GetChild(0).GetComponent<E_Trigger>().teamfight = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
         if (other.tag == "team" || other.tag == "Player" || other.tag == "Boss" || other.tag == "Bossunder")
            {
                playerlist.Remove(other.gameObject);
            }
            else if (other.tag == "enemy1" || other.tag == "enemy2" || other.tag == "enemy3" || other.tag == "enemy4")
            {
                other.transform.GetChild(0).GetComponent<E_Trigger>().teamfight = false;
                enemyTeam.Remove(other.gameObject);
            }
            if (playerlist.Count == 0)
            {
                E_battlestart = false;
            }
    }
}
