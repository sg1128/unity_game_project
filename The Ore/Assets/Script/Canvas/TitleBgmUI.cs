using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBgmUI : MonoBehaviour
{
    public GameObject titlebgm;
    bool TorF = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clickbgm()
    {
        if (TorF == false)
        {
            titlebgm.SetActive(true);
            TorF = true;
        }
        else
        {
            titlebgm.SetActive(false);
            TorF = false;
        }
    }
}
