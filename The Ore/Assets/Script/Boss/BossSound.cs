using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    public BossSpawn BS;
    public AudioSource AS;
    public AudioClip deerbo;
    public AudioClip wolfbo;
    public AudioClip bearbo;
    public AudioClip elephantbo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void deerDie_Sound()
    {
        AS.clip = deerbo;
        AS.Play();
    }
    public void wolfDie_Sound()
    {
        AS.clip = wolfbo;
        AS.Play();
    }
    public void bearDie_Sound()
    {
        AS.clip = bearbo;
        AS.Play();
    }
    public void elephantDie_Sound()
    {
        AS.clip = elephantbo;
        AS.Play();
    }
}
