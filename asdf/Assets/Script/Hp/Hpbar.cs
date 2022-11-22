using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using UnityEngine.SceneManagement;
public class Hpbar : MonoBehaviour
{
    public string Name;
    public float maxHp, nowHp, atkDmg, atkSpeed, Def, Dex;
    public int cri;
    Player player;
    bool delete = false;
    public float deleteTime = 0f;
    bool death = false;
    public GameObject faint;
    public GameObject bossunder;
    public GameObject myteam;
    public Transform pos;
    public GameObject delete_faint;
    public bool re_button = false;
    public bool ma_button = false;
    public bool Inside = false;
    public bool repeat = false;
    public bool close = false;
    public bool inRadius = false;
    public bool stop = false;
    // 공격속도 조절용 매개변수
    public float atkCoolTime;
    public float atkCoolTime_Max;
    public float waitForAtk;
    public bool bossDie = false;
    public GameObject thsSend;
    public GameObject colSend;
    public bool corou_Runing = false;
    Rigidbody2D rb;
    public SkeletonAnimation skeletonAnimation;
    Synergy_S dead;
    Synergy_P damaged;
    masug masug;
    public Hp delete_hp;
    public bool delete_child = false;
    public GameObject lose;

    public GameObject hearthstoning_Effect;
    public GameObject cleanse_Effect;

    public GameObject deerAtkAnim_1;
    public GameObject deerAtkAnim_2;
    public GameObject deerCriAnim;

    public GameObject wolfAtkAnim_1;
    public GameObject wolfAtkAnim_2;
    public GameObject wolfCriAnim;

    public GameObject bearAtkAnim_1;
    public GameObject bearAtkAnim_2;
    public GameObject bearCriAnim;

    public GameObject elephantAtkAnim_1;
    public GameObject elephantAtkAnim_2;
    public GameObject elephantCriAnim_1;
    public GameObject elephantCriAnim_2;

    public GameObject hp_BuffAnim;
    public GameObject dex_BuffAnim;
    public GameObject atk_BuffAnim;
    public GameObject maxhp_BuffAnim;

    public Bossunder_Count BC;
    Command orderAnim;
    public bool buff_Ck = false;
    public bool buff_Runing = false;

    public bool criStack = false;
    public SoundEffect soundManager;
    public AudioSource audioSource;
    AudioSource soundManager_audio;

    public Animal_Change AC;
    public Curse curse;

    public bool qe = false;
    GameObject inst;
    public GameObject QEinfo;
    bool aa = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dead = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_S>();
        damaged = GameObject.FindWithTag("MainCamera").GetComponent<Synergy_P>();
        masug = GameObject.FindWithTag("MainCamera").GetComponent<masug>();
        equalName();
        BC = GameObject.Find("Bossunder_Count").GetComponent<Bossunder_Count>();
        curse = GameObject.Find("Tree").GetComponent<Curse>();
    }
    private void SetEnemyStatus(string _Name, float _atkDmg, float _maxHp, float _Def, float _Dex, float _atkSpeed, int _cri)
    {
        Name = _Name;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        Dex = _Dex;
        Def = _Def;
        atkSpeed = _atkSpeed;
        cri = _cri;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundEffect>();
        lose = GameObject.Find("Canvas").transform.GetChild(16).gameObject;
        SetAtkCoolTime();
        orderAnim = GameObject.FindWithTag("MainCamera").GetComponent<Command>();
        soundManager_audio = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        delete_hp = transform.GetChild(0).GetComponent<Hp>();
        AC = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
    }
    void Update()
    {

        if (curse.cur_count == 3)
        {
            if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
            {
                nowHp = -1;
                Destroy(delete_hp.hpBar.gameObject, 0.5f);
                Destroy(gameObject, 0.5f);
            }
        }
        if (gameObject.tag == "Player")
        {
            if (aa == true)
            {
                if (transform.localScale.x == 1)
                    inst.transform.localScale = new Vector3(1, 1, 1);
                if (transform.localScale.x == -1)
                    inst.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (qe == true)
            {
                if (aa == false)
                {
                    aa = true;
                    inst = Instantiate(QEinfo, transform);
                    if (gameObject.name == "deer(Clone)")
                        inst.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 5);
                    if (gameObject.name == "wolf(Clone)")
                        inst.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 4);
                    if (gameObject.name == "bear(Clone)")
                        inst.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 4);
                    if (gameObject.name == "elephant(Clone)")
                        inst.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 4.7f);
                }
            }
            else if (qe == false)
            {
                if (aa == true)
                {
                    aa = false;
                    Destroy(inst.gameObject);
                }
            }
        }
        else
        {
            if (aa == true)
            {
                aa = false;
                Destroy(inst.gameObject);
            }
        }
        delete_hp = transform.GetChild(0).GetComponent<Hp>();
        audioSource.volume = soundManager_audio.volume;
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (!buff_Runing)
            {
                if (orderAnim.command1On)
                {
                    StartCoroutine("BuffAnim_INST", hp_BuffAnim);
                    StartCoroutine("Command_INST", soundManager.deerCmd);
                }
                else if (orderAnim.command2On)
                {
                    StartCoroutine("BuffAnim_INST", dex_BuffAnim);
                    StartCoroutine("Command_INST", soundManager.wolfCmd);
                }
                else if (orderAnim.command3On)
                {
                    StartCoroutine("BuffAnim_INST", atk_BuffAnim);
                    StartCoroutine("Command_INST", soundManager.bearCmd);
                }
                else if (orderAnim.command4On)
                {
                    StartCoroutine("BuffAnim_INST", maxhp_BuffAnim);
                    StartCoroutine("Command_INST", soundManager.elephantCmd);
                }
            }
        }

        if (!inRadius)
        {
            audioSource.mute = true;
        }
        if (inRadius || gameObject.tag == "faint")
        {
            audioSource.mute = false;
        }
        if (gameObject.tag == "Boss")
        {
            if (nowHp <= 0)
                inRadius = true;
        }


        if (gameObject.tag != "Player" && gameObject.tag != "Boss")
        {
            if (close)
            {
                rb.mass = 150;
            }
            else
            {
                rb.mass = 1;
            }
        }
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        if (!repeat)
        {
            if (inRadius)
            {
                skeletonAnimation.enabled = true;
            }
            else if (!inRadius && gameObject.tag != "faint")
            {
                skeletonAnimation.enabled = false;
            }
        }
        Re_AtkCoolTime();
        //if (gameObject.tag == "team")
        //{
        //    if (nowHp <= 0)
        //    {
        //        Destroy(delete_hp.hpBar.gameObject);
        //        Destroy(gameObject);
        //    }
        //}
        // 기절하고 30초 뒤에 없어짐
        if (gameObject.tag == "faint")
        {
            if (delete)
            {
                deleteTime += Time.deltaTime;
            }
            if (deleteTime >= 30)
            {
                Destroy(gameObject);
            }
        }
        if (death == true)
        {
            if (Inside == true && repeat == false)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                if (ma_button) //마석화
                {
                    StartCoroutine("norevive");

                }
                if (re_button && masug.masugNum >= 2 && player.revive[0] == transform.gameObject && AC.asd.Count <= 29) //정화
                {
                    StartCoroutine("revive_team");
                }
            }
        }
    }
    private IEnumerator norevive()
    {
        yield return new WaitForSeconds(0.05f);
        repeat = true;
        player.revive.Remove(gameObject);
        StartCoroutine("Hearthstoning_Effect", soundManager.hearth);
        masug.masugNum += 3;
        StopCoroutine("norevive");
    }
    private IEnumerator revive_team()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(delete_faint);
        player.revive.Remove(gameObject);
        masug.masugNum -= 2;
        close = false;
        repeat = true;
        GameObject inst = Instantiate(myteam, pos);
        inst.transform.position = this.transform.position;
        delete_child = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<FollowUp>().trigger = inst.GetComponent<T_Trigger>();
        gameObject.tag = "team";
        StartCoroutine("Cleanse_Effect", soundManager.cleans);
        Destroy(gameObject.GetComponent<E_Fight>());
        gameObject.AddComponent<T_Fight>();
        equalName();
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        Cleanse_EF(cleanse_Effect);
        StopCoroutine("revive_team");
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (gameObject.tag == "CurseEnemy")
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "team")
            {
                close = true;
                thsSend = gameObject;
                colSend = col.gameObject;

                StartCoroutine("EnumAtk");
            }
        }
        if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "team" || col.gameObject.tag == "Boss" || col.gameObject.tag == "Bossunder")
            {
                close = true;
                thsSend = gameObject;
                colSend = col.gameObject;

                StartCoroutine("EnumAtk");
            }
        }
        if (gameObject.tag == "Player")
        {
            if (col.gameObject.tag == "enemy1" || col.gameObject.tag == "enemy2" || col.gameObject.tag == "enemy3" || col.gameObject.tag == "enemy4" || col.gameObject.tag == "Boss" || col.gameObject.tag == "Bossunder" || col.gameObject.tag == "CurseEnemy")
            {
                close = true;
                thsSend = gameObject;
                colSend = col.gameObject;

                StartCoroutine("EnumAtk");
            }
        }
        if (gameObject.tag == "team")
        {
            if (col.gameObject.tag == "enemy1" || col.gameObject.tag == "enemy2" || col.gameObject.tag == "enemy3" || col.gameObject.tag == "enemy4" || col.gameObject.tag == "Boss" || col.gameObject.tag == "Bossunder" || col.gameObject.tag == "CurseEnemy")
            {
                close = true;
                thsSend = gameObject;
                colSend = col.gameObject;

                StartCoroutine("EnumAtk");
            }
            if (col.gameObject.tag == "team")
            {
                if (col.transform.GetChild(0).GetComponent<T_Trigger>().follow == true)
                {
                    transform.GetChild(0).GetComponent<T_Trigger>().follow = true;
                }
            }

        }
        if (gameObject.tag == "Boss" || gameObject.tag == "Bossunder")
        {
            if (col.gameObject.tag == "enemy1" || col.gameObject.tag == "enemy2" || col.gameObject.tag == "enemy3" || col.gameObject.tag == "enemy4" || col.gameObject.tag == "Player" || col.gameObject.tag == "team")
            {
                close = true;
                thsSend = gameObject;
                colSend = col.gameObject;

                StartCoroutine("EnumAtk");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        corou_Runing = false;
        if (gameObject.tag == "Player" || gameObject.tag == "team")
        {
            if (col.gameObject.tag == "enemy1" || col.gameObject.tag == "enemy2" || col.gameObject.tag == "enemy3" || col.gameObject.tag == "enemy4" || col.gameObject.tag == "Boss" || gameObject.tag == "Bossunder" || col.gameObject.tag == "CurseEnemy")
            {
                close = false;
            }
        }
        if (gameObject.tag == "Boss" || gameObject.tag == "Bossunder")
        {
            if (col.gameObject.tag == "enemy1" || col.gameObject.tag == "enemy2" || col.gameObject.tag == "enemy3" || col.gameObject.tag == "enemy4" || col.gameObject.tag == "Player" || gameObject.tag == "team")
            {
                close = false;
            }
        }
        if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "team" || col.gameObject.tag == "Boss" || col.gameObject.tag == "Bossunder")
            {
                close = false;
            }
        }
        if (gameObject.tag == "CurseEnemy")
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "team")
            {
                close = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (gameObject.tag == "faint")
        {
            if (col.tag == "Player")
            {
                player.revive.Add(gameObject);
                Inside = true;
            }
            if (col.tag == "Boss" || col.tag == "Bossunder")
            {
                if (BC.bossunder.Count <= 14)
                {
                    StartCoroutine("revive_bossunder");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.tag == "faint")
        {
            if (col.tag == "Player")
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                player.revive.Remove(gameObject);
                Inside = false;
            }
        }
    }

    private IEnumerator revive_bossunder()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.tag = "Bossunder";
        Destroy(delete_faint);
        close = false;
        Destroy(gameObject.GetComponent<E_Fight>());
        Destroy(gameObject.GetComponent<Player>());
        Destroy(gameObject.GetComponent<FollowUp>());
        Destroy(gameObject.GetComponent<SNG_P>());
        Destroy(gameObject.GetComponent<SNG_S>());
        Destroy(gameObject.GetComponent<Bool>());
        Destroy(gameObject.GetComponent<UseSkill>());
        gameObject.AddComponent<Boss_F>();
        gameObject.AddComponent<Bu_Fight>();
        equalName();
        gameObject.GetComponent<Boss_F>().enabled = true;
        GameObject inst = Instantiate(bossunder, pos);
        inst.transform.position = this.transform.position;
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        //gameObject.GetComponent<Collider2D>().isTrigger = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        delete_hp = transform.GetChild(0).GetComponent<Hp>();
        StopCoroutine("revive_bossunder");
    }
    void StartCorou()
    {
        if (corou_Runing == false)
        {
            StartCoroutine("EnumAtk");
        }
    }

    public void SetAtkCoolTime()
    {
        waitForAtk = atkSpeed;
        atkCoolTime = 1f / waitForAtk;
        atkCoolTime_Max = atkCoolTime;
    }

    public void Re_AtkCoolTime()
    {
        atkCoolTime += Time.deltaTime;
    }

    private IEnumerator EnumAtk()
    {
        corou_Runing = true;

        while (corou_Runing)
        {
            if (nowHp <= 0)
            {
                IfDead();
            }
            else if (nowHp > 0)
            {
                if (atkCoolTime >= atkCoolTime_Max)
                {
                    Atk();
                    IfDead();
                }
            }
            yield return null;
        }
    }
    private void Atk()
    {
        atkCoolTime = 0;
        float atk = thsSend.GetComponent<Hpbar>().atkDmg;
        float def = colSend.GetComponent<Hpbar>().Def;
        int cri = thsSend.GetComponent<Hpbar>().cri;

        int criRange = 100 / cri;

        if (criStack)
        {
            criRange = 1;
            criStack = false;
        }
        if (def == 0f)
        {
            if (Random.Range(0, criRange) == 0)
            {
                float atkcri = atk + colSend.GetComponent<Hpbar>().atkDmg / 2;
                //if(colSend.GetComponent<Hpbar>().atkDmg/2 > 20)
                //{
                //    atkcri = atk + 20;
                //}

                colSend.GetComponent<Hpbar>().nowHp -= atkcri;

                if (gameObject.tag == "team" || gameObject.tag == "Player")
                {
                    TeamCriAnim();
                }

                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4" || gameObject.tag == "Bossunder" || gameObject.tag == "CurseEnemy")
                {
                    EnemyCriAnim();
                }
                if (gameObject.tag == "Boss")
                {
                    BossCriAnim();
                }
            }
            else
            {
                colSend.GetComponent<Hpbar>().nowHp -= thsSend.GetComponent<Hpbar>().atkDmg;

                if (gameObject.tag == "team" || gameObject.tag == "Player")
                {
                    TeamAtkAnim();
                }

                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4" || gameObject.tag == "Bossunder" || gameObject.tag == "CurseEnemy")
                {
                    EnemyAtkAnim();
                }
                if (gameObject.tag == "Boss")
                {
                    BossAtkAnim();
                }
            }
        }
        else
        {
            if (Random.Range(0, criRange) == 0)
            {
                float atkcri = atk + colSend.GetComponent<Hpbar>().atkDmg / 2;

                colSend.GetComponent<Hpbar>().nowHp -= atkcri - atkcri * (def / 100);

                if (gameObject.tag == "team" || gameObject.tag == "Player")
                {
                    TeamCriAnim();
                }
                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4" || gameObject.tag == "Bossunder" || gameObject.tag == "CurseEnemy")
                {
                    EnemyCriAnim();
                }
                if (gameObject.tag == "Boss")
                {
                    BossCriAnim();
                }
            }
            else
            {
                colSend.GetComponent<Hpbar>().nowHp -= thsSend.GetComponent<Hpbar>().atkDmg - thsSend.GetComponent<Hpbar>().atkDmg * (def / 100);

                if (gameObject.tag == "team" || gameObject.tag == "Player")
                {
                    TeamAtkAnim();
                }
                if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4" || gameObject.tag == "Bossunder" || gameObject.tag == "CurseEnemy")
                {
                    EnemyAtkAnim();
                }
                if (gameObject.tag == "Boss")
                {
                    BossAtkAnim();
                }
            }
        }
    }


    private void IfDead()
    {
        if (gameObject.tag == "CurseEnemy")
        {
            if (nowHp <= 0)
            {
                soundManager.Animal_Die_Sound(soundManager.elephantDie);
                Destroy(delete_hp.hpBar.gameObject);
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "Bossunder")
        {
            if (nowHp <= 0)
            {
                if (gameObject.name == "deer(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.deerDie);
                if (gameObject.name == "wolf(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.wolfDie);
                if (gameObject.name == "bear(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.bearDie);
                if (gameObject.name == "elephant(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.elephantDie);
                Destroy(delete_hp.hpBar.gameObject);
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "enemy1" || gameObject.tag == "enemy2" || gameObject.tag == "enemy3" || gameObject.tag == "enemy4")
        {
            if (nowHp <= 0)
            {
                delete_faint = transform.GetChild(0).gameObject;
                Destroy(delete_hp.hpBar.gameObject);
                repeat = true;
                if (inRadius)
                {
                    if (gameObject.tag == "enemy1")
                        soundManager.Animal_Die_Sound(soundManager.deerDie);
                    if (gameObject.tag == "enemy2")
                        soundManager.Animal_Die_Sound(soundManager.wolfDie);
                    if (gameObject.tag == "enemy3")
                        soundManager.Animal_Die_Sound(soundManager.bearDie);
                    if (gameObject.tag == "enemy4")
                        soundManager.Animal_Die_Sound(soundManager.elephantDie);
                }
                delete = true;
                gameObject.GetComponent<E_Fight>().enabled = false;
                Destroy(transform.GetChild(0).gameObject, 0.2f);
                GameObject inst = Instantiate(faint, pos);
                inst.transform.position = this.transform.position;
                gameObject.GetComponent<Collider2D>().enabled = false;          //기존 Collider 해제
                death = true;
                gameObject.tag = "faint";
                skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
            }
        }

        if (gameObject.tag == "team" || gameObject.tag == "Player")
        {
            if (damaged.l_buff1)
            {
                nowHp -= 3;
            }
            if (nowHp <= 0)
            {
                if (gameObject.name == "deer(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.deerDie);
                if (gameObject.name == "wolf(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.wolfDie);
                if (gameObject.name == "bear(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.bearDie);
                if (gameObject.name == "elephant(Clone)")
                    soundManager.Animal_Die_Sound(soundManager.elephantDie);
                Destroy(delete_hp.hpBar.gameObject);
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                if (player.b_state)
                {
                    if (dead.lg_buff)
                    {
                        dead.crinum++;
                    }
                }
                if (gameObject.tag == "Player")
                {
                    delete_hp.start = false;
                    Time.timeScale = 0;
                    lose.SetActive(true);
                }
                if (gameObject.tag == "team")
                {
                    Destroy(delete_hp.hpBar.gameObject);
                    Destroy(gameObject);
                }

            }
        }
        repeat = false;
    }


    public void equalName()
    {
        if (name.Equals("deer(Clone)"))
        {
            SetEnemyStatus("deer", 5f, 50f, 0f, 5f, 1f, 20);
        }
        else if (name.Equals("wolf(Clone)"))
        {
            SetEnemyStatus("wolf", 15f, 60f, 0f, 5f, 1f, 10);
        }
        else if (name.Equals("bear(Clone)"))
        {
            SetEnemyStatus("bear", 13f, 105f, 5f, 5f, 1f, 5);
        }
        else if (name.Equals("elephant(Clone)"))
        {
            SetEnemyStatus("elephant", 10f, 155f, 20f, 5f, 1f, 2);
        }
        else if (name.Equals("deerBoss")) // base -> 20,30,35,40
        {
            SetEnemyStatus("deerBoos", 20f, 400f, 50f, 5f, 1f, 10);
        }
        else if (name.Equals("wolfBoss"))
        {
            SetEnemyStatus("wolfBoos", 30f, 450f, 50f, 5f, 1f, 10);
        }
        else if (name.Equals("bearBoss"))
        {
            SetEnemyStatus("bearBoos", 26f, 600f, 50f, 5f, 1f, 10);
        }
        else if (name.Equals("elephantBoss"))
        {
            SetEnemyStatus("elephantBoos", 20f, 700f, 50f, 5f, 1f, 10);
        }
        else if (name.Equals("curseEnemy"))
        {
            SetEnemyStatus("curseEnemy", 20f, 200f, 0f, 10f, 1f, 1);
        }
    }

    void Hearthstopning_EF(GameObject hearth)
    {
        GameObject inst = Instantiate(hearth, transform);
        Destroy(transform.GetChild(0).gameObject);
        Destroy(inst, 1.33f);
        Destroy(gameObject, 1.33f);
    }

    void Cleanse_EF(GameObject cleanse)
    {
        GameObject inst = Instantiate(cleanse, transform);
        Destroy(inst, 2f);
    }

    private IEnumerator AtkAnim_INST(GameObject anim)
    {
        yield return new WaitForSeconds(0.1f);
        stop = true;
        GameObject inst = Instantiate(anim, transform);
        yield return new WaitForSeconds(0.3f);
        stop = false;
        Destroy(inst);
    }

    private IEnumerator BuffAnim_INST(GameObject anim)
    {
        buff_Runing = true;

        GameObject inst = Instantiate(anim, transform);
        Destroy(inst, 1.2f);
        yield return new WaitForSeconds(2);

        buff_Runing = false;
    }

    private IEnumerator Hearthstoning_Effect(AudioClip hearth)
    {
        Hearthstopning_EF(hearthstoning_Effect);
        audioSource.clip = hearth;
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        audioSource.Stop();
    }
    private IEnumerator Cleanse_Effect(AudioClip hearth)
    {
        audioSource.clip = hearth;
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        audioSource.Stop();
    }

    private IEnumerator AtkSound_INST(AudioClip snd)
    {
        audioSource.clip = snd;
        yield return new WaitForSeconds(0.2f);
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        audioSource.Stop();
    }

    private IEnumerator Command_INST(AudioClip snd)
    {
        audioSource.clip = snd;
        audioSource.Play();
        yield return 0;
    }
    void TeamAtkAnim()
    {
        if (Random.Range(0, 2) == 0)
        {
            // 팀 공격처리 + 임시 플레이어
            if (transform.GetChild(0).gameObject.name == "team_1(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_1";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "team_2(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_1";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "team_3(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_1";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "team_4(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_1";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk1);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_1";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
        }
        else
        {
            if (transform.GetChild(0).gameObject.name == "team_1(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_2";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "team_2(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_2";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "team_3(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_2";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "team_4(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_2";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk2);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_2";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);

            }
        }
    }

    void EnemyAtkAnim()
    {
        if (Random.Range(0, 2) == 0)
        {
            // 적 공격처리
            if (transform.GetChild(0).gameObject.name == "deerAnima")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_1";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_1";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_1";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_1";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk1);
            }
            else if (transform.GetChild(0).gameObject.name == "deerAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_1";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_1";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_1";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_1";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk1);
            }
        }
        else
        {
            // 적 공격처리
            if (transform.GetChild(0).gameObject.name == "deerAnima")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_2";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_2";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_2";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_2";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk2);
            }
            else if (transform.GetChild(0).gameObject.name == "deerAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_2";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_2";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_2";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_2";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk2);
            }
        }
    }

    void TeamCriAnim()
    {
        // 팀 크리처리 + 임시 플레이어
        if (transform.GetChild(0).gameObject.name == "team_1(Clone)")
        {
            skeletonAnimation.AnimationName = "Deer_Atk_crit";
            StartCoroutine("AtkAnim_INST", deerCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.deerCri);
        }
        else if (transform.GetChild(0).gameObject.name == "team_2(Clone)")
        {
            skeletonAnimation.AnimationName = "Wolf_Atk_Crit";
            StartCoroutine("AtkAnim_INST", wolfCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.wolfCri);
        }
        else if (transform.GetChild(0).gameObject.name == "team_3(Clone)")
        {
            skeletonAnimation.AnimationName = "Bear_Atk_crit";
            StartCoroutine("AtkAnim_INST", bearCriAnim);

            StartCoroutine("AtkSound_INST", soundManager.bearCri);
        }
        else if (transform.GetChild(0).gameObject.name == "team_4(Clone)")
        {
            skeletonAnimation.AnimationName = "Elephant_Crit";
            StartCoroutine("AtkAnim_INST", elephantCriAnim_1);
            StartCoroutine("AtkAnim_INST", elephantCriAnim_2);
            StartCoroutine("AtkSound_INST", soundManager.elephantCri);
        }
        else if (transform.GetChild(0).gameObject.name == "wolfAnima")
        {
            skeletonAnimation.AnimationName = "Wolf_Atk_Crit";
            StartCoroutine("AtkAnim_INST", wolfCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.wolfCri);
        }
    }

    void EnemyCriAnim()
    {
        // 적 크리처리
        if (transform.GetChild(0).gameObject.name == "deerAnima")
        {
            skeletonAnimation.AnimationName = "Deer_Atk_crit";
            StartCoroutine("AtkAnim_INST", deerCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.deerCri);
        }
        else if (transform.GetChild(0).gameObject.name == "wolfAnima")
        {
            skeletonAnimation.AnimationName = "Wolf_Atk_Crit";
            StartCoroutine("AtkAnim_INST", wolfCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.wolfCri);
        }
        else if (transform.GetChild(0).gameObject.name == "bearAnima")
        {
            skeletonAnimation.AnimationName = "Bear_Atk_crit";
            StartCoroutine("AtkAnim_INST", bearCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.bearCri);
        }
        else if (transform.GetChild(0).gameObject.name == "elephantAnima")
        {
            skeletonAnimation.AnimationName = "Elephant_Crit";
            StartCoroutine("AtkAnim_INST", elephantCriAnim_1);
            StartCoroutine("AtkAnim_INST", elephantCriAnim_2);
            StartCoroutine("AtkSound_INST", soundManager.elephantCri);
        }
        else if (transform.GetChild(0).gameObject.name == "deerAnima(Clone)")
        {
            skeletonAnimation.AnimationName = "Deer_Atk_crit";
            StartCoroutine("AtkAnim_INST", deerCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.deerCri);
        }
        else if (transform.GetChild(0).gameObject.name == "wolfAnima(Clone)")
        {
            skeletonAnimation.AnimationName = "Wolf_Atk_Crit";
            StartCoroutine("AtkAnim_INST", wolfCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.wolfCri);
        }
        else if (transform.GetChild(0).gameObject.name == "bearAnima(Clone)")
        {
            skeletonAnimation.AnimationName = "Bear_Atk_crit";
            StartCoroutine("AtkAnim_INST", bearCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.bearCri);
        }
        else if (transform.GetChild(0).gameObject.name == "elephantAnima(Clone)")
        {
            skeletonAnimation.AnimationName = "Elephant_Crit";
            StartCoroutine("AtkAnim_INST", elephantCriAnim_1);
            StartCoroutine("AtkAnim_INST", elephantCriAnim_2);
            StartCoroutine("AtkSound_INST", soundManager.elephantCri);
        }
    }

    void BossAtkAnim()
    {
        if (Random.Range(0, 2) == 0)
        {
            if (transform.GetChild(0).gameObject.name == "deerAnima")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_1";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_1";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_1";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_1";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_1);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk1);
            }
        }
        else
        {
            if (transform.GetChild(0).gameObject.name == "deerAnima")
            {
                skeletonAnimation.AnimationName = "Deer_Atk_2";
                StartCoroutine("AtkAnim_INST", deerAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.deerAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "wolfAnima")
            {
                skeletonAnimation.AnimationName = "Wolf_Atk_2";
                StartCoroutine("AtkAnim_INST", wolfAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.wolfAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "bearAnima")
            {
                skeletonAnimation.AnimationName = "Bear_Atk_2";
                StartCoroutine("AtkAnim_INST", bearAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.bearAtk);
            }
            else if (transform.GetChild(0).gameObject.name == "elephantAnima")
            {
                skeletonAnimation.AnimationName = "Elephant_Atk_2";
                StartCoroutine("AtkAnim_INST", elephantAtkAnim_2);
                StartCoroutine("AtkSound_INST", soundManager.elephantAtk2);
            }
        }
    }

    void BossCriAnim()
    {
        if (transform.GetChild(0).gameObject.name == "deerAnima")
        {
            skeletonAnimation.AnimationName = "Deer_Atk_crit";
            StartCoroutine("AtkAnim_INST", deerCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.deerCri);
        }
        else if (transform.GetChild(0).gameObject.name == "wolfAnima")
        {
            skeletonAnimation.AnimationName = "Wolf_Atk_Crit";
            StartCoroutine("AtkAnim_INST", wolfCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.wolfCri);
        }
        else if (transform.GetChild(0).gameObject.name == "bearAnima")
        {
            skeletonAnimation.AnimationName = "Bear_Atk_crit";
            StartCoroutine("AtkAnim_INST", bearCriAnim);
            StartCoroutine("AtkSound_INST", soundManager.bearCri);
        }
        else if (transform.GetChild(0).gameObject.name == "elephantAnima")
        {
            skeletonAnimation.AnimationName = "Elephant_Crit";
            StartCoroutine("AtkAnim_INST", elephantCriAnim_1);
            StartCoroutine("AtkAnim_INST", elephantCriAnim_2);
            StartCoroutine("AtkSound_INST", soundManager.elephantCri);
        }
    }


}