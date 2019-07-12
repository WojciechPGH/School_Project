using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 2f;
    float bulletLifeSpan = 30f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0f);
    }

    private void Update()
    {
        bulletLifeSpan -= Time.deltaTime;
        if(bulletLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.CompareTag("Player"))
        {
            coll.GetComponent<PlayerController>().GotHit(1);
            Destroy(gameObject);
        }
    }
}
