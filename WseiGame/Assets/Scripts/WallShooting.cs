using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShooting : MonoBehaviour
{
    public GameObject bullet;
    Animator anim;
    public float reloadTime = 5f;
    float reloadTimer = 0f;
    bool reload = false;
    public bool readyToFire = true;
    float xOffset = 0.11f;
    Vector3 shootPos;

    void Start()
    {
        shootPos = transform.position + new Vector3(xOffset, 0f, 0f);
        anim = GetComponent<Animator>();
        anim.SetFloat("reloadingSpeed", 1 / reloadTime);
    }

    void Update()
    {
        if (reload == true)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                reload = false;
                readyToFire = true;
                reloadTimer = 0f;
            }
        }
        anim.SetBool("Reloading", reload);
    }

    public void Shoot(float bSpeed)
    {
        if (readyToFire == true)
        {
            Instantiate(bullet, shootPos, transform.rotation).GetComponent<BulletController>().bulletSpeed = bSpeed;
            reload = true;
            readyToFire = false;
        }
    }
}
