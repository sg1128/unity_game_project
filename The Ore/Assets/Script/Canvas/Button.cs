using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public int start = 1;
    public GameObject bgmButton;
    public GameObject bgmSlider;
    public Image img;
    bool buttonClick = false;
    public ButtonCtrl startCtrl;
    AudioSource bgm;
    public float fadeCount = 0f;
    public GameObject start_1;
    public GameObject quit;
    void Start()
    {
        bgm = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        start = PlayerPrefs.GetInt("start");
    }
    void Update()
    {
        if (startCtrl.startOn)
        {
                Go_stage1();
        }
    }

    public void Go_stage1()
    {
        start_1.SetActive(false);
        quit.SetActive(false);
        bgmButton.SetActive(false);
        bgmSlider.SetActive(false);
        startCtrl.startOn = false;
        if (!buttonClick)
        {
            buttonClick = true;
            StartCoroutine("FadePanel");
        }
    }
    IEnumerator FadePanel()
    {
        buttonClick = false;
        fadeCount = 0f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            bgm.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0, 0, 0, fadeCount);
        }
        if (start >= 16)
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
        StopCoroutine("FadePanel");
    }
}