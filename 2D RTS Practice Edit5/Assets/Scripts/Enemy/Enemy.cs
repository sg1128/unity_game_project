using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;
    Image nowHpBar;

    public int height = 1;

    public string name;
    public int maxHp;
    public int nowHp;
    public int dmg;
    public float atkSpeed;

    private void SetEnemyStatus(string _name, int _maxHp, int _dmg, float _atkSpeed)
    {
        name = _name;
        maxHp = _maxHp;
        nowHp = _maxHp;
        dmg = _dmg;
        atkSpeed = _atkSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        SetEnemyStatus("enemy", 100, 10, 1);
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpBar.fillAmount = (float)nowHp / (float)maxHp;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Clickable"))
        {
            col.gameObject.GetComponent<Unit>().TakeDamage(dmg);
        }
    }

    public void TakeDamage(int damage)
    {
        nowHp = nowHp - damage;
        Die();
    }

    void Die()
    {
        if (nowHp <= 0)
        {
            Destroy(hpBar.gameObject);
            Destroy(nowHpBar);
            Destroy(gameObject);
        }
    }
}
