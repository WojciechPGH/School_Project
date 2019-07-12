using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJumperMovement : EnemyAI
{
    float moveTimer = 2f;
    bool right = true;
    Rigidbody2D rgbd;
    int status; // 0 - stay, 1 - jump
    public float jumpHeight = 0.65f;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        status = 0;
        gameObject.AddComponent<MonsterLife>().life = life;
    }

    protected override void Movement()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            moveTimer = 1f + Random.Range(1, 3);
            status = (status + 1) % 2;
            if (status == 1)
            {
                float dir = Random.value;
                if (dir >= 0.5f)
                    right = true;
                else
                    right = false;
            }
        }
        if (status == 1)
        {
            if (right == true)
            {
                rgbd.AddForce(new Vector2(0.5f, 2f) * jumpHeight, ForceMode2D.Impulse);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rgbd.AddForce(new Vector2(-0.5f, 2f) * jumpHeight, ForceMode2D.Impulse);
                transform.localScale = new Vector3(1, 1, 1);
            }
            status = 0;
        }
    }
}
