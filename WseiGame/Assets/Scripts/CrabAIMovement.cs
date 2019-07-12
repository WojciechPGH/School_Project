using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAIMovement : MonoBehaviour
{
    float moveTimer = 5f;
    bool right = true;
    Rigidbody2D rgbd;
    int status; // 0 - stay, 1 - walk
    float moveSpeed = 0.5f;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody2D>();
        status = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GotHit(1);
        }
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            moveTimer = 6f + Random.Range(3, 7);
            status = (status + 1) % 2;
            if (status == 1)
            {
                right = !right;
            }
        }
        if (status == 1)
        {
            anim.SetBool("Walking", true);
            if (right == true)
            {
                rgbd.velocity = Vector2.right * moveSpeed;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rgbd.velocity = Vector2.left * moveSpeed;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
