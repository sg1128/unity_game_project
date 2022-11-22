using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class masug : MonoBehaviour
{
    public int masugNum;
    public Text ScriptTxt;


    void Start()
    {
    }

    void Update()
    {
        ScriptTxt.text = masugNum.ToString();
    }
}
