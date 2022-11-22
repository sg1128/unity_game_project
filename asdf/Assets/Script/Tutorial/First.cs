using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class First : MonoBehaviour
{
    public int start=1;
    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;
    public GameObject t5;
    public GameObject t6;
    public GameObject t7;
    public GameObject t8;
    public GameObject t9;
    public GameObject t10;
    public GameObject t11;
    public GameObject t12;
    public GameObject t13;
    public GameObject t14;
    public GameObject t15;
    public GameObject t16;
    public GameObject t17;
    public GameObject t18;
    void Start()
    {
        start = PlayerPrefs.GetInt("start");
    }

    // Update is called once per frame
    void Update()
    {
        if (start == 1)
        {
            t1.SetActive(true); t2.SetActive(false);
        }
        if (start == 2)
        {
            t2.SetActive(true); t3.SetActive(false);
        }
        if (start == 3)
        {
            t3.SetActive(true); t4.SetActive(false);
        }
        if (start == 4)
        {
            t4.SetActive(true); t5.SetActive(false);
        }
        if (start == 5)
        {
            t5.SetActive(true); t6.SetActive(false);
        }
        if (start == 6)
        {
            t6.SetActive(true); t7.SetActive(false);
        }
        if (start == 7)
        {
            t7.SetActive(true); t8.SetActive(false);
        }
        if (start == 8)
        {
            t8.SetActive(true); t9.SetActive(false);
        }
        if (start == 9)
        {
            t9.SetActive(true); t10.SetActive(false);
        }
        if (start == 10)
        {
            t10.SetActive(true); t11.SetActive(false);
        }
        if (start == 11)
        {
            t11.SetActive(true); t12.SetActive(false);
        }
        if (start == 12)
        {
            t12.SetActive(true); t13.SetActive(false);
        }
        if (start == 13)
        {
            t13.SetActive(true); t14.SetActive(false);
        }
        if (start == 14)
        {
            t14.SetActive(true); t15.SetActive(false);
        }
        if (start == 15)
        {
            t15.SetActive(true); t16.SetActive(false);
        }
        if (start == 16)
        {
            t16.SetActive(true); t17.SetActive(false);
        }
        if (start == 17)
        {
            t17.SetActive(true); t18.SetActive(false);
        }
        if (start == 18)
        {
            t18.SetActive(true);
        }
    }

        public void rightClick()
    {
        start++;
        PlayerPrefs.SetInt("start", start);
        if (start >= 19)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
    public void leftClick()
    {
        start--;
        if (start <= 1)
        {
            start = 1;
        }

        PlayerPrefs.SetInt("start", start);
    }
}
