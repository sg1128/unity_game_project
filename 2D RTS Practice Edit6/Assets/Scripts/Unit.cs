using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject prfMpBar;
    public GameObject canvas;
    Image nowHpBar;
    Image nowMpBar;
    Control control;
    TargetList targetlist;
    RectTransform hpBar;
    RectTransform mpBar;

    public bool attacking = false;
    public bool switching = false;
    public float height = 1;

    public int maxHp;
    public int nowHp;
    public int maxMp = 100;
    public int nowMp = 0;
    public int dmg;
    public float atkSpeed;
    public int speed;
    private void SetUnitStatus(int _maxHp, int _dmg, float _atkSpeed, int _speed)
    {
        maxHp = _maxHp;
        nowHp = _maxHp;
        dmg = _dmg;
        atkSpeed = _atkSpeed;
        speed = _speed;
    }
    // Start is called before the first frame update

    private void Awake()
    {
        targetlist = GameObject.Find("TargetList").GetComponent<TargetList>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
    }
    void Start()
    {
        //targetlist.targetIdle.Add(this.gameObject);
        control = GameObject.Find("Object_control").GetComponent<Control>();
        SetUnitStatus(100, 10, 1,5);
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        mpBar = Instantiate(prfMpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        nowMpBar = mpBar.transform.GetChild(0).GetComponent<Image>();
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void Update()
    {
        nowHpBar.fillAmount = (float)nowHp / (float)maxHp;
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)1.3, 0));
        hpBar.position = _hpBarPos;

        nowMpBar.fillAmount = (float)nowMp / (float)maxMp;
        Vector3 _mpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1, 0));
        mpBar.position = _mpBarPos;

        if (attacking == true)
        {
            Duplication_att();
            if (switching)
            {
                switching = false;
                targetlist.targetIdle.Remove(this.gameObject);
                targetlist.targetAttack.Add(this.gameObject);
            }
        }
        else
        {
            Duplication_idle();

            if (switching)
            {
                switching = false;
                targetlist.targetAttack.Remove(this.gameObject);
                targetlist.targetIdle.Add(this.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        nowHp = nowHp - damage;
        nowMp +=10;
        Die();
    }

    void Die()
    {
        if(nowHp <= 0)
        {
            if (attacking == true)
            {
                targetlist.targetAttack.Remove(this.gameObject);
            }
            else
            {
                targetlist.targetIdle.Remove(this.gameObject);
            }
            control.playerlist.Remove(this.gameObject);
            Destroy(hpBar.gameObject);
            Destroy(mpBar.gameObject);
            Destroy(gameObject);
        }
    }

    void Duplication_att()
    {
        if (targetlist.targetAttack.Contains(this.gameObject))
        {
            switching = false;
        }
        else
        {
            switching = true;
        }
    }

    void Duplication_idle()
    {
        if (targetlist.targetIdle.Contains(this.gameObject))
        {
            switching = false;
        }
        else
        {
            switching = true;
        }

    }
}
