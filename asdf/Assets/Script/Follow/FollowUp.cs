using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUp : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;
    float moveSpeed;
    public T_Trigger trigger;
    Hpbar playerHp;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        trigger = transform.GetChild(0).gameObject.GetComponent<T_Trigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "team" && trigger.follow == true)
        { FollowTarget(); }
    }

    void FollowTarget() {
        targetReset();

        playerHp = GameObject.FindWithTag("Player").GetComponent<Hpbar>();
        moveSpeed = playerHp.Dex;
        if (trigger.follow == true)
        {
            Vector3 startPos = gameObject.transform.position;
            Vector3 finalPos = target.position;

                if (startPos.x - finalPos.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (startPos.x - finalPos.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
            transform.position = Vector2.MoveTowards(transform.position, finalPos, moveSpeed * Time.deltaTime);
        }
        else if(trigger.follow ==false)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void targetReset(){
         target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}
