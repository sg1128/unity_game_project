using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag2 : MonoBehaviour
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
        if (col.name == "deerBoss" || col.name == "elephantBoss")
        {
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "deerBoss" || col.name == "elephantBoss")
        {
            inside = false;
        }
    }
}
