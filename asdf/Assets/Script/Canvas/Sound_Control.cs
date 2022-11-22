using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Sound_Control : MonoBehaviour
{
    public GameObject audio_con;
    public GameObject soundManager;
    public Slider bgslider;
    public Slider seslider;
    float backVol = 1f;
    float seVol = 1f;
    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backVol");
        seVol = PlayerPrefs.GetFloat("seVol");
        bgslider.value = backVol;
        seslider.value = seVol;

    }

    // Update is called once per frame
    void Update()
    {
        Bgm_sound();
        Se_sound();
    }

    public void Bgm_sound()
    {
        audio_con.GetComponent<AudioSource>().volume = bgslider.value;
        backVol = bgslider.value;
        PlayerPrefs.SetFloat("backVol", backVol);
    }

    public void Se_sound()
    {
        soundManager.GetComponent<AudioSource>().volume = seslider.value;
        seVol = seslider.value;
        PlayerPrefs.SetFloat("seVol", seVol);
    }
}
