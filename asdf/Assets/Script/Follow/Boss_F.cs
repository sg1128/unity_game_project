using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_F : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;
    public float moveSpeed;
    public Bu_Trigger trigger;
    GameObject BossName;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Boss").GetComponent<Transform>();
        BossName = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        trigger = transform.GetChild(0).gameObject.GetComponent<Bu_Trigger>();
        moveSpeed = BossName.GetComponent<Hpbar>().Dex;
        if (trigger.follow == true)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        if (trigger.follow == true)
        {
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = target.position;

            if (transform.GetComponent<Hpbar>().close == false)
            {
                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else if (trigger.follow == false)
        {
            rb.velocity = Vector2.zero;
        }
    }
}