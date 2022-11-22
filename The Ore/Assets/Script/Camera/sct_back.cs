using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sct_back : MonoBehaviour
{
    public GameObject back1;
    public GameObject back2;
    public GameObject back3;
    public GameObject back4;
    public CameraFollow CF;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CF.conAni == 1)
        {
            back1.SetActive(false);
            back2.SetActive(true);
            back3.SetActive(true);
            back4.SetActive(true);
        }
        else if (CF.conAni == 2)
        {
            back1.SetActive(true);
            back2.SetActive(false);
            back3.SetActive(true);
            back4.SetActive(true);
        }
        else if (CF.conAni == 3)
        {
            back1.SetActive(true);
            back2.SetActive(true);
            back3.SetActive(false);
            back4.SetActive(true);
        }
        else if (CF.conAni == 4)
        {
            back1.SetActive(true);
            back2.SetActive(true);
            back3.SetActive(true);
            back4.SetActive(false);
        }
    }
}
