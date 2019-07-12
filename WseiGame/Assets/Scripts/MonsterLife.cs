using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    public int life;
    public GameObject destroyPrefab;
    public void GotHit(int val)
    {
        life -= val;
        if (life < 0)
        {
            Destroy(gameObject);
            DestroyEffect();
        }
    }

    private void DestroyEffect()
    {
        Instantiate(destroyPrefab, transform.position, transform.rotation);
    }
}
