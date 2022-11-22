using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Ui : MonoBehaviour
{
    public RectTransform anm1;
    public RectTransform anm2;
    public RectTransform anm3;
    public RectTransform anm4;
    public GameObject lock_anm1;
    public GameObject lock_anm2;
    public GameObject lock_anm3;
    public GameObject lock_anm4;
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
    public GameObject info4;
    //public GameObject skill_cool1;
    //public GameObject skill_cool2;
    //public GameObject skill_cool3;
    //public GameObject skill_cool4;
    public GameObject deer;
    public GameObject wolf;
    public GameObject bear;
    public GameObject elephant;
    GameObject player;
    Animal_Change animalChange;
    public CameraFollow CF;
    int startTime = 0;
    int goTime = 0;
    void Start()
    {
        animalChange = GameObject.FindWithTag("MainCamera").GetComponent<Animal_Change>();
        startTime = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            startTime = (int)Time.time;
        }
        goTime = (int)Time.time - startTime;
        if (goTime < 6)
        {
            if (CF.p_ani == 1)
            {
                anm1.offsetMax = new Vector2(0, 0);
                anm1.offsetMin = new Vector2(0, 0);
                anm2.offsetMax = new Vector2(0, 29f);
                anm2.offsetMin = new Vector2(0, 29);
                anm3.offsetMax = new Vector2(0, 29f);
                anm3.offsetMin = new Vector2(0, 29);
                anm4.offsetMax = new Vector2(0, 29f);
                anm4.offsetMin = new Vector2(0, 29);
                deer.transform.localPosition = new Vector2(-914, 422);
                wolf.transform.localPosition = new Vector2(-846, 452);
                bear.transform.localPosition = new Vector2(-777.11f, 452);
                elephant.transform.localPosition = new Vector2(-708f, 452);
                //skill_cool1.transform.localPosition = new Vector2(-913.0001f, 355.5f);
                //skill_cool2.transform.localPosition = new Vector2(-844.8f, 357f);
                //skill_cool3.transform.localPosition = new Vector2(-777f, 357f);
                //skill_cool4.transform.localPosition = new Vector2(-709f, 357f);
            }
            else if (CF.p_ani == 2)
            {
                anm1.offsetMax = new Vector2(0, 29f);
                anm1.offsetMin = new Vector2(0, 29f);
                anm2.offsetMax = new Vector2(0, 0);
                anm2.offsetMin = new Vector2(0, 0);
                anm3.offsetMax = new Vector2(0, 29f);
               anm3.offsetMin = new Vector2(0, 29);
                anm4.offsetMax = new Vector2(0, 29f);
                anm4.offsetMin = new Vector2(0, 29);
                deer.transform.localPosition = new Vector2(-914, 452);
                wolf.transform.localPosition = new Vector2(-846, 422);
                bear.transform.localPosition = new Vector2(-777.11f, 452);
                elephant.transform.localPosition = new Vector2(-708f, 452);
                //skill_cool1.transform.localPosition = new Vector2(-913.0001f, 357f);
                //skill_cool2.transform.localPosition = new Vector2(-844.8f, 355.5f);
                //skill_cool3.transform.localPosition = new Vector2(-777f, 357f);
                //skill_cool4.transform.localPosition = new Vector2(-709f, 357f);
            }
            else if (CF.p_ani == 3)
            {
                anm1.offsetMax = new Vector2(0, 29f);
               anm1.offsetMin = new Vector2(0, 29f);
                anm2.offsetMax = new Vector2(0, 29f);
                anm2.offsetMin = new Vector2(0, 29f);
                anm3.offsetMax = new Vector2(0, 0);
                anm3.offsetMin = new Vector2(0, 0);
                anm4.offsetMax = new Vector2(0, 29f);
                anm4.offsetMin = new Vector2(0, 29);
                deer.transform.localPosition = new Vector2(-914, 452);
                wolf.transform.localPosition = new Vector2(-846, 452);
                bear.transform.localPosition = new Vector2(-777.11f, 422);
                elephant.transform.localPosition = new Vector2(-708f, 452);
                //skill_cool1.transform.localPosition = new Vector2(-913.0001f, 357f);
                //skill_cool2.transform.localPosition = new Vector2(-844.8f, 357f);
                //skill_cool3.transform.localPosition = new Vector2(-777f, 355.5f);
                //skill_cool4.transform.localPosition = new Vector2(-709f, 357f);
            }
            else if (CF.p_ani == 4)
            {
                anm1.offsetMax = new Vector2(0, 29f);
                anm1.offsetMin = new Vector2(0, 29f);
                anm2.offsetMax = new Vector2(0, 29f);
                anm2.offsetMin = new Vector2(0, 29f);
                anm3.offsetMax = new Vector2(0, 29f);
                anm3.offsetMin = new Vector2(0, 29);
                anm4.offsetMax = new Vector2(0, 0);
                anm4.offsetMin = new Vector2(0, 0);
                deer.transform.localPosition = new Vector2(-914, 452);
                wolf.transform.localPosition = new Vector2(-846, 452);
                bear.transform.localPosition = new Vector2(-777.11f, 452);
                elephant.transform.localPosition = new Vector2(-708f, 422);
                //skill_cool1.transform.localPosition = new Vector2(-913.0001f, 357f);
                //skill_cool2.transform.localPosition = new Vector2(-844.8f, 357f);
                //skill_cool3.transform.localPosition = new Vector2(-777f, 357f);
                //skill_cool4.transform.localPosition = new Vector2(-709f, 355.5f);
            }
        }
        else
        {
            if (CF.p_ani == 1)
            {
                anm1.offsetMax = new Vector2(0, 29f);
                anm1.offsetMin = new Vector2(0, 29f);
                deer.transform.localPosition = new Vector2(-914, 452);
                //skill_cool1.transform.localPosition = new Vector2(-913.0001f, 357f);
            }
            else if (CF.p_ani == 2)
            {
                anm2.offsetMax = new Vector2(0, 29f);
                anm2.offsetMin = new Vector2(0, 29f);
                wolf.transform.localPosition = new Vector2(-846, 452);
                //skill_cool2.transform.localPosition = new Vector2(-844.8f, 357f);
            }
            else if (CF.p_ani == 3)
            {
                anm3.offsetMax = new Vector2(0, 29f);
                anm3.offsetMin = new Vector2(0, 29f);
                bear.transform.localPosition = new Vector2(-777.11f, 452);
                //skill_cool3.transform.localPosition = new Vector2(-777f, 357f);
            }
            else if (CF.p_ani == 4)
            {
                anm4.offsetMax = new Vector2(0, 29f);
                anm4.offsetMin = new Vector2(0, 29f);
                elephant.transform.localPosition = new Vector2(-708f, 452);
                //skill_cool4.transform.localPosition = new Vector2(-709f, 357f);
            }
            CF.p_ani = CF.conAni;
        }
        player = GameObject.FindWithTag("Player");
        if (player.name == "deer(Clone)")
        {
            anm1.offsetMax = new Vector2(0, 0);
            anm1.offsetMin = new Vector2(0, 0);
            info1.transform.localPosition = new Vector2(-913.0001f, 355.5f);
            info2.transform.localPosition = new Vector2(-844.8f, 380);
            info3.transform.localPosition = new Vector2(-777f, 380);
            info4.transform.localPosition = new Vector2(-709f, 380);
            //skill_cool1.transform.localPosition = new Vector2(-913.0001f, 355.5f);
            deer.transform.localPosition = new Vector2(-914, 422);
        }
        else if (player.name == "wolf(Clone)")
        {
            anm2.offsetMax = new Vector2(0, 0);
            anm2.offsetMin = new Vector2(0, 0);
            info1.transform.localPosition = new Vector2(-913.0001f, 380);
            info2.transform.localPosition = new Vector2(-844.8f, 355.5f);
            info3.transform.localPosition = new Vector2(-777f, 380);
            info4.transform.localPosition = new Vector2(-709f, 380);
            //skill_cool2.transform.localPosition = new Vector2(-844.8f, 355.5f);
            wolf.transform.localPosition = new Vector2(-846, 422);
        }
        else if (player.name == "bear(Clone)")
        {
            anm3.offsetMax = new Vector2(0, 0);
            anm3.offsetMin = new Vector2(0, 0);
            info1.transform.localPosition = new Vector2(-913.0001f, 380);
            info2.transform.localPosition = new Vector2(-844.8f, 380);
            info3.transform.localPosition = new Vector2(-777f, 355.5f);
            info4.transform.localPosition = new Vector2(-709f, 380);
            //skill_cool3.transform.localPosition = new Vector2(-777f, 355.5f);
            bear.transform.localPosition = new Vector2(-777.11f, 422);
        }
        else if (player.name == "elephant(Clone)")
        {
            anm4.offsetMax = new Vector2(0, 0);
            anm4.offsetMin = new Vector2(0, 0);
            info1.transform.localPosition = new Vector2(-913.0001f, 380);
            info2.transform.localPosition = new Vector2(-844.8f, 380);
            info3.transform.localPosition = new Vector2(-777f, 380);
            info4.transform.localPosition = new Vector2(-709f, 355.5f);
            //skill_cool4.transform.localPosition = new Vector2(-709f, 355.5f);
            elephant.transform.localPosition = new Vector2(-706.3f, 422);
        }


        if (animalChange.deer.Count == 0)
        {
            lock_anm1.SetActive(true);
            anm1.offsetMax = new Vector2(0, 29f);
            anm1.offsetMin = new Vector2(0, 29);
            deer.transform.localPosition = new Vector2(-914, 452);
        }
        else
        {
            lock_anm1.SetActive(false);
        }
        if (animalChange.wolf.Count == 0)
        {
            lock_anm2.SetActive(true);
            anm2.offsetMax = new Vector2(0, 29f);
            anm2.offsetMin = new Vector2(0, 29);
            wolf.transform.localPosition = new Vector2(-846, 452);
        }
        else
        {
            lock_anm2.SetActive(false);
        }
        if (animalChange.bear.Count == 0)
        {
            lock_anm3.SetActive(true);
            anm3.offsetMax = new Vector2(0, 29f);
            anm3.offsetMin = new Vector2(0, 29);
            bear.transform.localPosition = new Vector2(-777.11f, 452);
        }
        else
        {
            lock_anm3.SetActive(false);
        }
        if (animalChange.elephant.Count == 0)
        {
            lock_anm4.SetActive(true);
            anm4.offsetMax = new Vector2(0, 29f);
            anm4.offsetMin = new Vector2(0, 29);
            elephant.transform.localPosition = new Vector2(-708f, 452);
        }
        else
        {
            lock_anm4.SetActive(false);
        }
    }
}
