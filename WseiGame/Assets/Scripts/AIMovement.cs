using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : EnemyAI
{
    float moveTimer = 5f;
    bool right = true;
    int status; // 0 - stay, 1 - walk
    float moveSpeed = 0.5f;
    Rigidbody2D rgbd;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        status = 0;
    }

    protected override void Movement()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            moveTimer = 4f + Random.Range(1, 3);
            status = (status + 1) % 2;
            if(status == 1)
            {
                right = !right;
            }
        }
        if(status == 1)
        {
            if(right == true)
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
    }
}
