using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossunder_Count : MonoBehaviour
{
    public List<GameObject> bossunder = new List<GameObject>();
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bossunder")
		{
			bossunder.Add(other.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bossunder")
		{
			bossunder.Remove(other.gameObject);
		}
	}
}
