using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosMove : MonoBehaviour
{
    Vector2 MousePosition;
    Camera Camera;
    UnitSelections unitSelections;
    public bool aclick = false;
    //public bool toWall = false;

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //        toWall = true;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    toWall = false;
    //}

    private void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        unitSelections = GameObject.FindGameObjectWithTag("UnitSelection").transform.GetChild(0).GetComponent<UnitSelections>();
    }

    void Update()
    {
        //if (aclick == true)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        MousePosition = Input.mousePosition;
        //        MousePosition = Camera.ScreenToWorldPoint(MousePosition);
        //        transform.position = MousePosition;
        //    }
        //}
        if (Input.GetMouseButton(1))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(MousePosition);
            transform.position = MousePosition;
        }
    }
}
