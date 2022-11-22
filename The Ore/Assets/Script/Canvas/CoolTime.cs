using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoolTime : MonoBehaviour
{
   Image nowCoolTime;
    Command cooltime;
    public GameObject white_gem;
    bool FinishCool = true;
    void Start()
    {
        nowCoolTime = GetComponent<Image>();
        cooltime = GameObject.FindWithTag("MainCamera").GetComponent<Command>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nowCoolTime.fillAmount == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FinishCool = true;
            }
        }
        if (gameObject.name == "skillcool_1")
        {
            nowCoolTime.fillAmount = 1 - ((float)cooltime.deercmdTime / (float)30f);
        }
        if (gameObject.name == "skillcool_2")
        {
            nowCoolTime.fillAmount = 1 - ((float)cooltime.wolfcmdTime / (float)80f);
        }
        if (gameObject.name == "skillcool_3")
        {
            nowCoolTime.fillAmount = 1 - ((float)cooltime.bearcmdTime / (float)100);
        }
        if (gameObject.name == "skillcool_4")
        {
            nowCoolTime.fillAmount = 1 - ((float)cooltime.elephantcmdTime / (float)120);
        }

        if (FinishCool == false)
        {
            if (nowCoolTime.fillAmount == 0)
            {
                FinishCool = true;
                StartCoroutine("white");
            }
        }
        if(FinishCool == true && nowCoolTime.fillAmount == 1)
        {
            FinishCool = false;
        }
    }

    private IEnumerator white()
    {
        white_gem.SetActive(true);
        yield  return new WaitForSeconds(0.5f);
        white_gem.SetActive(false);
    }
}
