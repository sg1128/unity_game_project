using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faint_die : MonoBehaviour
{
    Hpbar hpbar;
    void Start()
    {
        hpbar = transform.parent.GetComponent<Hpbar>();
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.delete_faint = transform.gameObject;
    }
}
