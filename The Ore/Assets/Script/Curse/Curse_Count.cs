using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Curse_Count : MonoBehaviour
{
    public Text Curse_count1;
    public Text Curse_count2;
    public Text Curse_count3;
    public Text Curse_count4;
    public Text Curse_count5;
    public Curse curse;
    public BossSpawn BS;
    public GameObject curse_act1;
    public GameObject curse_act2;
    public GameObject curse_act3;
    public GameObject curse_act4;
    public GameObject curse_act5;
    public GameObject curse_not1;
    public GameObject curse_not2;
    public GameObject curse_not3;
    public GameObject curse_not4;
    public GameObject curse_not5;
    public GameObject curse_sol;
    public int cur_c;
    public int cur_c1;
    public int cur_c2;
    public int cur_c3;
    public bool cur_1 = false;
    public bool cur_2 = false;
    public bool cur_3 = false;
    public bool cur_start1 = false;
    public bool cur_start2 = false;
    public bool cur_start3 = false;
    public int cur_suc = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BS.min >= 10 && BS.min <11 && cur_1== false)
        {
            cur_1 = true;
            Curse_count1.text = 10.ToString();
            Curse_count2.text = 10.ToString();
            Curse_count3.text = 10.ToString();
            Curse_count4.text = 10.ToString();
            Curse_count5.text = 10.ToString();
            cur_c = Random.Range(1, 6);
            cur_c1 = cur_c;
            Cur_Not();
        }
        if (BS.min >= 17 && BS.min < 18 && cur_2 == false)
        {
            cur_2 = true;
            Curse_count1.text = 20.ToString();
            Curse_count2.text = 20.ToString();
            Curse_count3.text = 20.ToString();
            Curse_count4.text = 20.ToString();
            Curse_count5.text = 20.ToString();
            while (true)
            {
                cur_c = Random.Range(1, 6);
                cur_c2 = cur_c;
                if(cur_c2 != cur_c1)
                {
                    break;
                }
            }
            Cur_Not();
        }
        if (BS.min >= 24 && BS.min < 25 && cur_3==false)
        {
            cur_3 = true;
            Curse_count1.text = 30.ToString();
            Curse_count2.text = 30.ToString();
            Curse_count3.text = 30.ToString();
            Curse_count4.text = 30.ToString();
            Curse_count5.text = 30.ToString();
            while (true)
            {
                cur_c = Random.Range(1, 6);
                cur_c3 = cur_c;
                if(cur_c3 != cur_c1 && cur_c3 != cur_c2)
                {
                    break;
                }
            }
            Cur_Not();
        }

        if (BS.min >= 11 && curse.curse1 == false  && BS.min <12)
        {
            if(cur_start1 == false)
            {
                cur_start1 = true;
                Cur_Act();
            }
        }
        if (BS.min >= 18 && curse.curse2 == false  && BS.min < 19)
        {
            if (cur_start2 == false)
            {
                cur_start2 = true;
                Cur_Act();
            }

        }
        if (BS.min >= 25 && curse.curse3 == false  && BS.min < 26)
        {
            if (cur_start3 == false)
            {
                cur_start3 = true;
                Cur_Act();
            }
        }

    }

    void Cur_Not()
    {
        if (cur_c == 1)
        {
            curse_not1.SetActive(true);
        }else if(cur_c == 2)
            {
            curse_not2.SetActive(true);
        }else if (cur_c == 3)
        {
            curse_not3.SetActive(true);
        }else if (cur_c == 4)
        {
            curse_not4.SetActive(true);
        }else if (cur_c == 5)
        {
            curse_not5.SetActive(true);
        }
        StartCoroutine("Cur_NNot");
    }

    IEnumerator Cur_NNot()
    {
        yield return new WaitForSeconds(5f);
        if (cur_c == 1)
        {
            curse_not1.SetActive(false);
        }
        else if (cur_c == 2)
        {
            curse_not2.SetActive(false);
        }
        else if (cur_c == 3)
        {
            curse_not3.SetActive(false);
        }
        else if (cur_c == 4)
        {
            curse_not4.SetActive(false);
        }
        else if (cur_c == 5)
        {
            curse_not5.SetActive(false);
        }
    }

    void Cur_Act()
    {
        if (cur_c == 1)
        {
            curse_act1.SetActive(true);
        }
        else if (cur_c == 2)
        {
            curse_act2.SetActive(true);
        }
        else if (cur_c == 3)
        {
            curse_act3.SetActive(true);
        }
        else if (cur_c == 4)
        {
            curse_act4.SetActive(true);
        }
        else if (cur_c == 5)
        {
            curse_act5.SetActive(true);
        }
        StartCoroutine("Cur_NAct");
    }

    IEnumerator Cur_NAct()
    {
        yield return new WaitForSeconds(5f);
        if (cur_c == 1)
        {
            curse_act1.SetActive(false);
        }
        else if (cur_c == 2)
        {
            curse_act2.SetActive(false);
        }
        else if (cur_c == 3)
        {
            curse_act3.SetActive(false);
        }
        else if (cur_c == 4)
        {
            curse_act4.SetActive(false);
        }
        else if (cur_c == 5)
        {
            curse_act5.SetActive(false);
        }
    }

    public void Cur_Sol()
    {
        curse_not1.SetActive(false);
        curse_not2.SetActive(false);
        curse_not3.SetActive(false);
        curse_not4.SetActive(false);
        curse_not5.SetActive(false);
        curse_sol.SetActive(true);
        StartCoroutine("Cur_NSol");
        cur_suc++;
    }
    IEnumerator Cur_NSol()
    {
        yield return new WaitForSeconds(5f);
        curse_sol.SetActive(false);
    }
}
