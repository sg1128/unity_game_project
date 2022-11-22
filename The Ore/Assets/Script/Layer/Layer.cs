using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent.parent.tag== "Player")
        {
            gameObject.layer = 6;
        }else
        {
            gameObject.layer = 7;
        }
    }
}
