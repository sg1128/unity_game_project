using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class BossSpawn : MonoBehaviour
{
    public GameObject deerBoss;
    public GameObject wolfBoss;
    public GameObject bearBoss;
    public GameObject elephantBoss;
    float deerSpawn = 7;
    float wolfSpawn = 14;
    float bearSpawn = 21;
    float elephantSpawn = 28;
    public int bossappear;
    float startTime;
    public GameObject warning;

    public int min = 0;
    public bool deerdie = false;
    public bool wolfdie = false;
    public bool beardie = false;
    public bool elephantdie = false;
    public bool bossDie = false;
    bool dS = false, wS = false, bS = false, eS = false;
    public GameObject deerboss;
    public GameObject wolfboss;
    public GameObject bearboss;
    public GameObject elephantboss;
    public Text time;
    public Text curtime;
    public GameObject win;
    public AudioCtrl AC;
    public BossSound BS;
    SkeletonAnimation d_sk;
    SkeletonAnimation w_sk;
    SkeletonAnimation b_sk;
    SkeletonAnimation e_sk;
    private void Awake()
    {
    }
    void Start()
    {
        startTime = Time.time;
        d_sk = deerboss.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        w_sk = wolfboss.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        b_sk = bearboss.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        e_sk = elephantboss.transform.GetChild(0).GetComponent<SkeletonAnimation>();
    }
    void Update()
    {
        if (deerdie == false && deerBoss.GetComponent<Hpbar>().nowHp <= 0)
        {
            BS.deerDie_Sound();
            deerboss.GetComponent<CircleCollider2D>().enabled = false;
            deerboss.GetComponent<DeerBoss>().enabled = false;
            d_sk.AnimationName = "Deer_Groggy";
            deerdie = true;
            bossDie = true;
            Destroy(deerBoss.GetComponent<Hpbar>().delete_hp.hpBar.gameObject, 2f);
            Destroy(deerBoss, 2f);
            AC.firstAudio = true;
            AC.boss = false;

        }
        if (wolfdie == false &&wolfBoss.GetComponent<Hpbar>().nowHp <= 0 )
        {
            BS.wolfDie_Sound();
            wolfboss.GetComponent<CircleCollider2D>().enabled = false;
            wolfboss.GetComponent<WolfBoss>().enabled = false;
            w_sk.AnimationName = "Wolf_Groggy";
            wolfdie = true;
            bossDie = true;
            AC.firstAudio = true;
            AC.boss = false;
            Destroy(wolfBoss.GetComponent<Hpbar>().delete_hp.hpBar.gameObject, 2f);
            Destroy(wolfboss, 2f);
        }
        if (beardie == false && bearBoss.GetComponent<Hpbar>().nowHp <= 0 )
        {
            BS.bearDie_Sound();
            bearboss.GetComponent<CircleCollider2D>().enabled = false;
            bearboss.GetComponent<BearBoss>().enabled = false;
            b_sk.AnimationName = "Bear_Groggy";
            beardie = true;
            bossDie = true;
            AC.firstAudio = true;
            AC.boss = false;
            Destroy(bearBoss.GetComponent<Hpbar>().delete_hp.hpBar.gameObject, 2f);
            Destroy(bearboss, 2f);
        }
        if (elephantdie == false && elephantBoss.GetComponent<Hpbar>().nowHp <= 0)
        {
            BS.elephantDie_Sound();
            AC.boss = false;
            elephantdie = true;
            bossDie = true;
            win.SetActive(true);
            Time.timeScale = 0;
        }



        bossappear = (int)Time.time - (int)startTime;
        if (bossappear >= 60)
        {
            min = bossappear / 60;
            bossappear -= 60 * min;
        }
        if(min < 10)
        {
            if (bossappear < 10)
            {
                time.text = "0" +min.ToString() + " : " + "0" + bossappear.ToString();
                curtime.text = "0" +min.ToString() + " : " + "0" + bossappear.ToString();
            }
            else
            {
                time.text = "0" + min.ToString() + " : " + bossappear.ToString();
                curtime.text = "0" + min.ToString() + " : " + bossappear.ToString();
            }
        }
        else
        {
            if (bossappear < 10)
            {
                time.text =min.ToString() + " : " + "0" + bossappear.ToString();
                curtime.text =min.ToString() + " : " + "0" + bossappear.ToString();            }
            else
            {
                time.text = min.ToString() + " : " + bossappear.ToString();
                curtime.text = min.ToString() + " : " + bossappear.ToString();
            }
        }
        if (deerdie == false && min >= deerSpawn && !dS)
        {
            deerBoss.SetActive(true);
            warning.SetActive(true);
            AC.Bossbgm();
            if (bossappear >3)
            {
                warning.SetActive(false);
                dS = true;
            }
        }
        else if (wolfdie == false && min >= wolfSpawn && !wS)
        {
            bossDie = false;
            wolfBoss.SetActive(true);
            warning.SetActive(true);
            AC.Bossbgm();
            if (bossappear > 3)
            {

                warning.SetActive(false);
                wS = true;
            }
        }
        else if (beardie == false && min >= bearSpawn && !bS)
        {
            bossDie = false;
            warning.SetActive(true);
            bearBoss.SetActive(true);
            AC.Bossbgm();
            if (bossappear > 3)
            {

                warning.SetActive(false);
                bS = true;
            }
        }
        else if (elephantdie == false && min >= elephantSpawn && !eS)
        {
            bossDie = false;
            warning.SetActive(true);
            elephantBoss.SetActive(true);
            AC.Bossbgm();
            if (bossappear > 3)
            {
                warning.SetActive(false);
                eS = true;
            }
        }
    }
}