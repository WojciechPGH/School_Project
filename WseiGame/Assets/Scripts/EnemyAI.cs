using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected int life = 1;

    private void Update()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GotHit(1);
        }
    }

    //public virtual void GotHit(int val)
    //{
    //    life-= val;
    //    if (life < 0)
    //        Destroy(this);
    //}

    protected abstract void Movement();
}
