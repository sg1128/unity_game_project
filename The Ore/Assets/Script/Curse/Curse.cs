using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Curse : MonoBehaviour
{
    public BossSpawn BS;
    public masug ms;
    public Animal_Change AC;
    public Command CM;
    public bool curse1 = false;
    public bool curse2 = false;
    public bool curse3 = false;
    public int cur_count;
    public bool finish1 = false;
    public bool finish2 = false;
    public bool finish3 = false;
    int nowMin;
    public GameObject minimap;
    public GameObject minib;
    public Image reduce_sight;
    public float fadeCount = 0f;
    bool fade = false;
    bool sadf = false;
    public Curse_Count CC;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CM.cmdCurse == true)
        {
            if ((BS.min*60) + BS.bossappear >= nowMin + 180)
            {
                CM.cmdCurse = false;
            }
        }
        if (BS.min >= 11 && finish1 == false)
        {
            cur_count = CC.cur_c1;
            finish1 = true;
            if(curse1 == false)
                CurseCount();
        }
        if (BS.min >= 18 && finish2 == false)
        {
            cur_count = CC.cur_c2;
            finish2 = true;
            if(curse2 == false)
                CurseCount();
        }
        if (BS.min >= 25 && finish3 == false)
        {

            cur_count = CC.cur_c3;
            finish3 = true;
            if(curse3 == false)
                CurseCount();
        }

        if (fade)
        {
            if(BS.bossappear >= 30 && sadf == false)
            {
                StartCoroutine("FadeoutImage");
            }
        }

    }

    void CurseCount()
    {
        if (cur_count == 1)
        {
            // 피 감소
            for (int i = AC.asd.Count - 1; i >= 0; i--)
            {
                if (AC.asd[i].tag != "Player")
                {
                    AC.asd[i].GetComponent<Hpbar>().nowHp = 1;
                }
            }
        }
        else if (cur_count == 2)
        {
            // 무리 생성
        }
        else if (cur_count == 3)
        {
            // 삭제
            StartCoroutine("enemydie");
        }
        else if (cur_count == 4)
        {
            // 스킬 사용 불가 
            CM.cmdCurse = true;
            nowMin = (BS.min*60) + BS.bossappear;

        }
        else if (cur_count == 5)
        {
            // 시야 감소
            Debug.Log("1");
            if(!fade)
                StartCoroutine("FadeinImage");
        }
    }
    IEnumerator FadeinImage()
    {
        fade = true;
        fadeCount = 0f;
        minimap.SetActive(false);
        minib.SetActive(false);
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            reduce_sight.color = new Color(0, 0, 0, fadeCount);
        }
        StopCoroutine("FadePanel");
    }
    IEnumerator FadeoutImage()
    {
        sadf = true;
        fadeCount = 1f;
        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            reduce_sight.color = new Color(0, 0, 0, fadeCount);
        }
        minib.SetActive(true);
        minimap.SetActive(true);
        StopCoroutine("FadePanel");
    }

    IEnumerator enemydie()
    {
        Debug.Log("1");
        yield return new WaitForSeconds(0.2f);
        cur_count = 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (BS.min >= 10 && BS.min < 11)
            {
                if (ms.masugNum >= 10 && curse1 == false)
                {
                    curse1 = true;
                    ms.masugNum = ms.masugNum - 10;
                    CC.Cur_Sol();
                }
            }
            if (BS.min >= 17 && BS.min < 18)
            {
                if (ms.masugNum >= 20 && curse2 == false)
                {
                    curse2 = true;
                    ms.masugNum = ms.masugNum - 20;
                    CC.Cur_Sol();
                }
            }
            if (BS.min >= 24 && BS.min < 25)
            {
                if (ms.masugNum >= 30 && curse3 == false)
                {
                    curse3 = true;
                    ms.masugNum = ms.masugNum - 30;
                    CC.Cur_Sol();
                }
            }
        }
    }
}
