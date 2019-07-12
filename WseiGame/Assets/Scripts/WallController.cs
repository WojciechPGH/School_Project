using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public enum ShootType { Salve = 0, AllAtOnce, Randomly };
    public float reloadTime = 5f;
    public ShootType shootType = 0; // 0 - One after another, 1 - Salve, 2 - randomly
    public float bulletSpeed = 2f;
    public float randomWait = 3f;
    public WallShooting[] wallFragments;

    void Start()
    {
        StartCoroutine(WaitToShoot(5));
    }

    IEnumerator WaitToShoot(float sec)
    {
        yield return new WaitForSeconds(sec);
        switch (shootType)
        {
            case ShootType.Salve: StartCoroutine(ShootSalve()); break;
            case ShootType.AllAtOnce: StartCoroutine(ShootAllAtOnce()); break;
            case ShootType.Randomly: StartCoroutine(ShootRandomly()); break;

            default: StartCoroutine(ShootSalve()); break;
        }
    }

    IEnumerator ShootSalve()
    {
        while (true)
        {
            yield return new WaitUntil(() => wallFragments[0].readyToFire);
            for(int i = 0; i < 3; i++)
            {
                wallFragments[i].Shoot(bulletSpeed);
                yield return new WaitForSeconds(1f);
            }
        }

    }

    IEnumerator ShootAllAtOnce()
    {
        while (true)
        {
            yield return new WaitUntil(() => wallFragments[0].readyToFire);
            for (int i = 0; i < 3; i++)
            {
                wallFragments[i].Shoot(bulletSpeed);
            }
        }
    }

    IEnumerator ShootRandomly()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitUntil(() => wallFragments[0].readyToFire);
                yield return new WaitForSeconds(Random.Range(0f, randomWait));
                wallFragments[i].Shoot(bulletSpeed);
            }
        }
    }
}
