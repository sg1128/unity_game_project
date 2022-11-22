using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag5 : MonoBehaviour
{
    public bool inside = false;
    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" || col.name == "elephantBoss")
        {
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" || col.name == "elephantBoss")
        {
            inside = false;
        }
    }
}
