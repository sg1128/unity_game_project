using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class Unit : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    public GameObject prfHpBar;
    public GameObject prfMpBar;
    public GameObject canvas;
    Image nowHpBar;
    Image nowMpBar;
    Control control;
    TargetList targetlist;
    public RectTransform hpBar;
    public RectTransform mpBar;
    UnitSelections unitSelection;
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
    public int upgradeCnt;      //강화 횟수
    public bool canEvolution;       //진화 가능한지
    public bool evolved;        //진화한 개체인지
    public bool newChar = false;
    public bool die;

    private void SetUnitStatus(int _maxHp, int _dmg, float _atkSpeed, int _speed)
    {
        maxHp = _maxHp;
        nowHp = _maxHp;
        dmg = _dmg;
        atkSpeed = _atkSpeed;
        speed = _speed;
    }

    private void Awake()
    {
        targetlist = GameObject.Find("TargetList").GetComponent<TargetList>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
    }
    // Start is called before the first frame update
    void Start()
    {
        unitSelection = GameObject.Find("UnitSelections").GetComponent<UnitSelections>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
        die = false;
        upgradeCnt = 0;
        canEvolution = false;
        evolved = false;
        SetUnitStatus(100, 10, 1, 5);
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        mpBar = Instantiate(prfMpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        nowMpBar = mpBar.transform.GetChild(0).GetComponent<Image>();
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void Update()
    {
        if (newChar == true)
        {
            Invoke("CreateAnimTime", 1.8f);
        }

        nowHpBar.fillAmount = (float)nowHp / (float)maxHp;
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)4, 0));
        hpBar.position = _hpBarPos;

        nowMpBar.fillAmount = (float)nowMp / (float)maxMp;
        Vector3 _mpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)3.7, 0));
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

        if (upgradeCnt >= 2)  //강화 2회 이상일 경우
        {
            canEvolution = true;
        }
        else
        {
            canEvolution = false;
        }
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    void CreateAnimTime()
    {
        newChar = false;
        transform.GetComponent<Tank_UnitMovement>().Des = GameObject.Find("Destination");
        transform.GetComponent<Tank_PlayerTarget>().Des = GameObject.Find("Destination");
    }

    public void TakeDamage(int damage)
    {
        nowHp = nowHp - damage;
        nowMp += 10;
        Die();
    }

    void Die()
    {
        if (nowHp <= 0)
        {
            die = true;
            Invoke("DeleteChar", 1.2f);
        }
    }

    void DeleteChar()
    {
        unitSelection.unitList.Remove(this.gameObject);
        unitSelection.unitsSelected.Remove(this.gameObject);
        if (gameObject.tag == "dps")
        {
            gameObject.GetComponent<DPS_fsm>().target.GetComponent<Enemy>().player.Remove(this.gameObject);
        }
        else if (gameObject.tag == "tank")
        {
            gameObject.GetComponent<Tank_fsm>().target.GetComponent<Enemy>().player.Remove(this.gameObject);
        }
        else
        {
            gameObject.GetComponent<Heal_fsm>().target.GetComponent<Enemy>().player.Remove(this.gameObject);
        }
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
        Debug.Log("유닛이 죽었습니다.");
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

    void TargetReset()
    {

    }
}