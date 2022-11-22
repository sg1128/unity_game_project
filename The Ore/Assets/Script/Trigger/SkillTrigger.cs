using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger : MonoBehaviour
{
	public bool bu_battlestart = false;
	public List<GameObject> enemylist = new List<GameObject>();
	public bool pl = false;
	int startcoolTime = 0;
	public int coolTime;
	int clear = -10;
	public int clearTime;
	public Hpbar bossHpbar;
    private void Start()
    {
	}

    private void Update()
    {
        if(enemylist.Count == 0)
        {
			bu_battlestart = false;
        }
		if(pl == false)
		{
			coolTime = (int)Time.time - startcoolTime;
			if (coolTime > 10)
			{
				if(transform.parent.name == "deerBoss")
                {
					transform.parent.GetComponent<DeerBoss>().enemyFightList.Clear();
                }
				if (transform.parent.name == "wolfBoss")
				{
					transform.parent.GetComponent<WolfBoss>().enemyFightList.Clear();
				}
				if (transform.parent.name == "bearBoss")
				{
					transform.parent.GetComponent<BearBoss>().enemyFightList.Clear();
				}
				if (transform.parent.name == "elephantBoss")
				{
					transform.parent.GetComponent<ElephantBoss>().enemyFightList.Clear();
				}
				startcoolTime = (int)Time.time;
				if ((bossHpbar.nowHp + 50) < bossHpbar.maxHp)
				{
					bossHpbar.nowHp += 50f;
				}
				else if((bossHpbar.nowHp +50) > bossHpbar.maxHp)
				{
					bossHpbar.nowHp = bossHpbar.maxHp;
				}
			}
        }
        else
        {
			clearTime = (int)Time.time - clear;
			if(clearTime > 10)
            {
				clear = (int)Time.time;
				if (transform.parent.name == "deerBoss")
				{
					transform.parent.GetComponent<DeerBoss>().enemyFightList.Clear();
				}
				if (transform.parent.name == "wolfBoss")
				{
					transform.parent.GetComponent<WolfBoss>().enemyFightList.Clear();
				}
				if (transform.parent.name == "bearBoss")
				{
					transform.parent.GetComponent<BearBoss>().enemyFightList.Clear();
				}
				if (transform.parent.name == "elephantBoss")
				{
					transform.parent.GetComponent<ElephantBoss>().enemyFightList.Clear();
				}
			}

		}
	}
    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Player" || other.gameObject.tag =="team")
		{
			bu_battlestart = true;
			enemylist.Add(other.gameObject);
		}
		if(other.tag == "Player")
        {
			pl = true;
			startcoolTime = (int)Time.time;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4" || other.gameObject.tag == "Player" || other.gameObject.tag == "team")
		{
			enemylist.Remove(other.gameObject);
		}
		if (gameObject.transform.parent.tag == "Boss")
		{
			if (other.tag == "Bossunder")
			{
				other.transform.GetChild(0).GetComponent<Bu_Trigger>().follow = true;
			}
		}
		if(other.tag == "Player")
        {
			pl = false;
		}
	}
}
