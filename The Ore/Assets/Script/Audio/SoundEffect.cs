using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip deerAtk;
    public AudioClip wolfAtk;
    public AudioClip bearAtk;
    public AudioClip elephantAtk1;
    public AudioClip elephantAtk2;

    public AudioClip deerCri;
    public AudioClip wolfCri;
    public AudioClip bearCri;
    public AudioClip elephantCri;

    public AudioClip deerDie;
    public AudioClip wolfDie;
    public AudioClip bearDie;
    public AudioClip elephantDie;

    public AudioClip deerCmd;
    public AudioClip wolfCmd;
    public AudioClip bearCmd;
    public AudioClip elephantCmd;

    public AudioClip hearth;
    public AudioClip cleans;
    public AudioClip suceed;

    public AudioSource audioSource;
    CameraFollow cameraF;
    // Start is called before the first frame update
    void Start()
    {
        cameraF = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraF.aniChange)
        {
            cameraF.aniChange = false;
            Suc_Sound(suceed);
        }
    }

    public void Animal_Die_Sound(AudioClip aniDie)
    {
        audioSource.clip = aniDie;
        audioSource.Play();

    }

    void Suc_Sound(AudioClip suc)
    {
        audioSource.clip = suc;
        audioSource.Play();
    }

}
