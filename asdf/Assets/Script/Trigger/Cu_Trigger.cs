using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cu_Trigger : MonoBehaviour
{
    public List<GameObject> enemylist = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Player" || other.gameObject.tag == "team")
        {
            enemylist.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "team")
        {
            enemylist.Remove(other.gameObject);
        }
    }
}
