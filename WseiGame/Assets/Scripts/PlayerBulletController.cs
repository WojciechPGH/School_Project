using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public float bulletSpeed = 2f;
    float bulletLifeSpan = 10f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * transform.localScale.x, 0f);
    }

    private void Update()
    {
        bulletLifeSpan -= Time.deltaTime;
        if (bulletLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.CompareTag("Enemy"))
        {
            coll.GetComponent<MonsterLife>().GotHit(1);
        }
        Destroy(gameObject);
    }
}
