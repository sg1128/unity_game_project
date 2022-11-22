using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hp : MonoBehaviour
{
    public GameObject prfHpbar;
    public GameObject canvas;
    public RectTransform hpBar;
    float height = 0.3f;
    public Image nowHpbar;
    Hpbar script_Hpbar;
    public bool start = true;
    private void Awake()
    {
        canvas = GameObject.Find("HpCanvas");
        script_Hpbar = transform.parent.GetComponent<Hpbar>();
    }
    public void Start()
    {
        if (gameObject.transform.parent.tag == "Player")
        {
            prfHpbar = canvas.transform.GetChild(0).gameObject;
        }
        else if (gameObject.transform.parent.tag == "team")
        {
            prfHpbar = canvas.transform.GetChild(1).gameObject;
        }
        else if (gameObject.transform.parent.tag == "Bossunder")
        {
            prfHpbar = canvas.transform.GetChild(2).gameObject;
        }
        else if (gameObject.transform.parent.tag == "enemy1" || gameObject.transform.parent.tag == "enemy2" || gameObject.transform.parent.tag == "enemy3" || gameObject.transform.parent.tag == "enemy4")
        {
            prfHpbar = canvas.transform.GetChild(3).gameObject;
        }else if(gameObject.transform.parent.tag == "Boss")
        {
            prfHpbar = canvas.transform.GetChild(4).gameObject;
        }
        hpBar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - height, 0));
            hpBar.position = _hpBarPos;
            nowHpbar.fillAmount = (float)script_Hpbar.nowHp / (float)script_Hpbar.maxHp;
        }
    }

    public void reHpbar()
    {
        start = false;
        Destroy(hpBar.gameObject);
        if (gameObject.transform.parent.tag == "Player")
        {
            prfHpbar = canvas.transform.GetChild(0).gameObject;
        }
        else if (gameObject.transform.parent.tag == "team")
        {
            prfHpbar = canvas.transform.GetChild(1).gameObject;
        }
        else if (gameObject.transform.parent.tag == "Bossunder")
        {
            prfHpbar = canvas.transform.GetChild(2).gameObject;
        }
        else if (gameObject.transform.parent.tag == "enemy1" || gameObject.transform.parent.tag == "enemy2" || gameObject.transform.parent.tag == "enemy3" || gameObject.transform.parent.tag == "enemy4")
        {
            prfHpbar = canvas.transform.GetChild(3).gameObject;
        }
        else if (gameObject.transform.parent.tag == "Boss")
        {
            prfHpbar = canvas.transform.GetChild(4).gameObject;
        }
        hpBar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
        start = true;
    }
}
