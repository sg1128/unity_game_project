using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public AudioClip otherClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip bossClip;
    public win win;
    public Lose lose;
    public bool asdf = false;
    public bool boss = false;
    public float time = 0f;
    public AudioSource bg;
    public bool firstAudio = true;

    void Start()
    {
    }
    void Update()
    {
        if (firstAudio)
        {
            firstAudio = false;
            bg.clip = otherClip;
            bg.Play();
            bg.loop = true;
        }
        if (win.escStop && asdf == false)
        {
            asdf = true;
            bg.clip = winClip;
            bg.Play();
            bg.loop = false;
        }
        if (lose.escStop && asdf == false)
        {
            asdf = true;
            bg.clip = loseClip;
            bg.Play();
            bg.loop = false;
        }
    }
    public void Bossbgm()
    {
        if (boss == false)
        {
            boss = true;
            bg.clip = bossClip;
            bg.Play();
            bg.loop = true;
        }
    }
}
