using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class drag : MonoBehaviour
{
    private Vector2 mPosCur;
    private Vector2 mPosBegin;
    private Vector2 mPosMin;
    private Vector2 mPosMax;
    private bool showSelection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showSelection = Input.GetMouseButton(0);
        if (!showSelection) return;

        mPosCur = Input.mousePosition;
        mPosCur.y = Screen.height - mPosCur.y;

        if (Input.GetMouseButton(0))
            mPosBegin = mPosCur;

        mPosMin = Vector2.Min(mPosCur, mPosBegin);
        mPosMax = Vector2.Max(mPosCur, mPosBegin);
    }

    private void OnGUI()
    {
        if (!showSelection) return;
        Rect rect = new Rect();
        rect.min = mPosMin;
        rect.max = mPosMax;

        GUI.Box(rect, "");
    }
}
