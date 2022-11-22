using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
	public List<GameObject> fightlist = new List<GameObject>();
	public List<GameObject> player = new List<GameObject>();
	public List<GameObject> bossunder = new List<GameObject>();
	public List<GameObject> enemylist = new List<GameObject>();
    void Start()
    {
		transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {		
    }
    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			player.Add(other.gameObject);
		}
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "team")
		{
			fightlist.Add(other.gameObject);
		}
		if (other.gameObject.tag == "Bossunder")
		{
			bossunder.Add(other.gameObject);
		}
		if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4")
		{
			enemylist.Add(other.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			player.Remove(other.gameObject);
		}
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "team")
		{
			fightlist.Remove(other.gameObject);
		}
		if (other.gameObject.tag == "Bossunder")
		{
			bossunder.Remove(other.gameObject);
		}
		if (other.gameObject.tag == "enemy1" || other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemy4")
		{
			enemylist.Remove(other.gameObject);
		}
	}
}
