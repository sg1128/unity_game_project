using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_Info : MonoBehaviour
{
    public GameObject info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            info.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            info.SetActive(false);
        }
    }
}
