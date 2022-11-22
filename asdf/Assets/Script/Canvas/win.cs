using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    public GameObject time;
    public GameObject curTime;
    public bool escStop = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        escStop = true;
        time.SetActive(true);
        curTime.SetActive(false);
    }
}