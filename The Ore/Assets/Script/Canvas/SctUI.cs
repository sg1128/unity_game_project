using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SctUI : MonoBehaviour
{
    public CameraFollow CF;
    public Image sct_bar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sct_bar.fillAmount = (float)CF.coolTime / (float)20;
    }
}
