using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    public GameObject pause;
    public GameObject setting;
    public GameObject option_s;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Gosetting()
    {
        pause.SetActive(false);
        setting.SetActive(true);
        option_s.SetActive(false);
    }

    public void Gopause()
    {
        pause.SetActive(true);
        setting.SetActive(false);
    }
}
