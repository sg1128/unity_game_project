using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public bool b_state = false;
    public Vector3 mousePos, transPos, targetPos;
    public Transform pos;
    Vector3 mouseX = Vector3.one;
    Animal_Change animal;
    Hpbar playerHpbar;
    T_Trigger p_t;
    public bool isPause = false;
    public SkeletonAnimation skeletonAnimation;
    public List<GameObject> revive = new List<GameObject>();
    bool stop=false;
    masug ms;
    void Start()
    {
        animal = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        p_t = transform.GetChild(0).GetComponent<T_Trigger>();
        playerHpbar = GetComponent<Hpbar>();
        ms = GameObject.FindWithTag("MainCamera").GetComponent<masug>();
    }


    void Update()
    {
        if (revive.Count > 0)
        {
            playerHpbar.qe = true;
            if (revive[0] == null || revive[0].activeSelf == false)
            {
                revive.Clear();
            }
            if (revive[0].name != "deer(Clone)" && revive[0].name != "wolf(Clone)" && revive[0].name != "bear(Clone)" && revive[0].name != "elephant(Clone)")
            {
                revive.Clear();
            }
            if (revive[0].tag != "faint")
            {
                revive.Clear();
            }
        }
        else if(revive.Count == 0)
        {
            playerHpbar.qe = false;
        }
        AtkToStop_P_Moving();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                isPause = true;
            }
            else
            {
                isPause = false;
            }
        }


        mouseX.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        if (!isPause)
        {
            if(playerHpbar.stop == false)
                PlayerMoving();
        }

        //MouveToTarget();

        //전투상태 비전투상태 표시
        if (p_t.T_battlestart == false)
        {
            for (int i = 0; i < animal.asd.Count; i++)
            {
                 b_state = false;
            }
        }
        else
        {
            b_state = true;
        }

        if (revive.Count > 0)
        {
            if (Input.GetKeyDown("e"))
            {
                if (ms.masugNum > 1 && animal.asd.Count <= 29) { 
                    if(revive[0].GetComponent<Hpbar>().ma_button == false)
                        revive[0].GetComponent<Hpbar>().re_button = true;
                }
            }
           else if (Input.GetKeyDown("q"))
            {
                if(revive[0].GetComponent<Hpbar>().re_button == false)
                    revive[0].GetComponent<Hpbar>().ma_button = true;
            }
        }

    }

    void CalTargetPos()
    {
        speed = playerHpbar.Dex;
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos =new Vector3(transPos.x, transPos.y, 0);
    }

    void MouveToTarget()
    {
        if (transform.GetComponent<UseSkill>().orderToStopAnim == false)
        {
           transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            //rb.MovePosition(transform.position + movePosition * Time.deltaTime*speed);
            
        }
    }

    public void Stop()
    {

        speed = 0f;

    }

    void AtkToStop_P_Moving()
    {
        if (transform.GetComponent<Hpbar>().close == false && transform.GetComponent<UseSkill>().orderToStopAnim == false)
        {
            StartCoroutine("Cheak_P_Moving");
        }
        else if (transform.GetComponent<Hpbar>().close == true || transform.GetComponent<UseSkill>().orderToStopAnim == true)
        {
            StopCoroutine("Cheak_P_Moving");
        }
    }

    private IEnumerator Cheak_P_Moving()
    {
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();

        Vector3 startPos = gameObject.transform.position;

        yield return new WaitForSeconds(0.1f);

        Vector3 finalPos = gameObject.transform.position;

        if (startPos == finalPos || stop || GetComponent<Hpbar>().close == true)
        {
            if (gameObject.name == "deer(Clone)")
            {
                skeletonAnimation.AnimationName = "Deer_Idle";
            }
            else if (gameObject.name == "wolf(Clone)")
            {
                skeletonAnimation.AnimationName = "Wolf_Idle";
            }
            else if (gameObject.name == "bear(Clone)")
            {
                skeletonAnimation.AnimationName = "Bear_Idle";
            }
            else if (gameObject.name == "elephant(Clone)")
            {
                skeletonAnimation.AnimationName = "Elephant_Idle";
            }
        }
        if(GetComponent<Hpbar>().close == false || !stop) {
            if (startPos != finalPos || startPos != finalPos)
            {
                if (gameObject.name == "deer(Clone)")
                {
                    skeletonAnimation.AnimationName = "Deer_Run";
                }
                else if (gameObject.name == "wolf(Clone)")
                {
                    skeletonAnimation.AnimationName = "Wolf_Run";
                }
                else if (gameObject.name == "bear(Clone)")
                {
                    skeletonAnimation.AnimationName = "Bear_Run";
                }
                else if (gameObject.name == "elephant(Clone)")
                {
                    skeletonAnimation.AnimationName = "Elephant_Run";
                }
            }
        }
    }

    void PlayerMoving()
    {
        if (transform.GetComponent<UseSkill>().orderToStopAnim == false)
        {
            MouveToTarget();

            if (Input.GetMouseButton(1))
            {
                CalTargetPos();

                if (gameObject.transform.position.x - mouseX.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

}

