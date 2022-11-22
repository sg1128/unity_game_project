using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public bool flag1, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9;
    void Start()
    {
        flag1 = false;
        flag2 = false;
        flag3 = false;
        flag4 = false;
        flag5 = false;
        flag6 = false;
        flag7 = false;
        flag8 = false;
        flag9 = false;
    }

    void Update()
    {
        
    }

    void flag_child(int i)
    {
        transform.GetChild(i).GetComponent<Flag>();
    }
}
