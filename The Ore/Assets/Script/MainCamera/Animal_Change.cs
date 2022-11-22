using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Animal_Change : MonoBehaviour
{
    public List<GameObject> asd = new List<GameObject>();
  
    public List<GameObject> deer = new List<GameObject>();
    public List<GameObject> wolf = new List<GameObject>();
    public List<GameObject> bear = new List<GameObject>();
    public List<GameObject> elephant = new List<GameObject>();
    public masug msg;
    public int radius;
    public int p_radius;

    public Text deerNum;
    public Text wolfNum;
    public Text bearNum;
    public Text elephantNum;
    public float minHp=1000;
    public int idx;
    //Camera zoom;
    void Awake(){
    }
    void Start()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) && msg.masugNum>0)
        {
            for (int i = asd.Count - 1; i >= 0; i--)
            {
                if (asd[i].tag == "team")
                {
                    if (asd[i].GetComponent<Hpbar>().nowHp < minHp)
                    {
                        minHp = asd[i].GetComponent<Hpbar>().nowHp;
                        idx = i;
                    }
                }
            }
            if (asd[idx].tag == "team")
            {
                Destroy(asd[idx].GetComponent<Hpbar>().delete_hp.hpBar.gameObject);
                Destroy(asd[idx].gameObject);
                msg.masugNum--;
                minHp = 1000;
            }
        }
        if (deer.Count > 9)
        {
            deerNum.text = deer.Count.ToString();
        }
        else
        {
            deerNum.text = "0"+ deer.Count.ToString();
        }

        if (wolf.Count > 9)
        {
            wolfNum.text = wolf.Count.ToString();
        }
        else
        {
            wolfNum.text = "0" + wolf.Count.ToString();
        }

        if (bear.Count > 9)
        {
            bearNum.text = bear.Count.ToString();
        }
        else
        {
            bearNum.text = "0"+ bear.Count.ToString();
        }

        if (elephant.Count > 9)
        {
            elephantNum.text = elephant.Count.ToString();
        }
        else
        {
            elephantNum.text = "0"+elephant.Count.ToString();
        }
        if (asd.Count >= 0)
        {
            radius = 20;
            p_radius = 40;
        }
        if (asd.Count >= 10)
        {
            radius = 27;
            p_radius = 45;
        }
        if(asd.Count >= 20)
        {
            radius = 36;
            p_radius = 55;
        }
        if(asd.Count >= 30)
        {
            radius = 45;
            p_radius = 60;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if(asd.Contains(other.gameObject)){
        //    return;
        //}

        if(other.tag == "Player" || other.tag == "team")
        {
            asd.Add(other.gameObject);
           
            if(other.name =="deer(Clone)")
            {
                deer.Add(other.gameObject);
            }else if(other.name =="wolf(Clone)")
            {
                wolf.Add(other.gameObject);
            }else if(other.name =="bear(Clone)")
            {
                bear.Add(other.gameObject);
            }else if(other.name =="elephant(Clone)")
            {
                elephant.Add(other.gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"|| other.tag == "team")
        {
            asd.Remove(other.gameObject);
            if(other.name =="deer(Clone)")
            {
                deer.Remove(other.gameObject);
            }else if(other.name =="wolf(Clone)")
            {
                wolf.Remove(other.gameObject);
            }else if(other.name =="bear(Clone)")
            {
                bear.Remove(other.gameObject);
            }else if(other.name =="elephant(Clone)")
            {
                elephant.Remove(other.gameObject);
            }
        }
    }
}
