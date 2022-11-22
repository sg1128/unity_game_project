using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Spawn : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject inst = Instantiate(player, transform);
        inst.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
