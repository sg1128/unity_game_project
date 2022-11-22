using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleSound_Manager : MonoBehaviour
{
    public GameObject audio_con;
    public Slider bgslider;
    float backVol = 1f;
    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backVol");
        bgslider.value = backVol;
    }

    // Update is called once per frame
    void Update()
    {
        Bgm_sound();
    }
    public void Bgm_sound()
    {
        audio_con.GetComponent<AudioSource>().volume = bgslider.value;
        backVol = bgslider.value;
        PlayerPrefs.SetFloat("backVol", backVol);
    }
}
