using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bu_Trigger : MonoBehaviour
{
    public bool follow = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (follow)
        {
            if (gameObject.transform.parent.tag == "Bossunder")
            {
                if (other.tag == "Boss")
                {
                    follow = false;
                }
            }
        }
    }
}
