using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    float upSpeed = 0.5f;
    public GameObject player;

    void Update()
    {
        if(transform.localPosition.y < 0.6f)
        {
            transform.localPosition += new Vector3(0f, upSpeed * Time.deltaTime, 0f);
        }
        else
        {
            Instantiate(player, transform.parent.position + transform.localPosition, transform.rotation);
            Destroy(gameObject);
        }
    }
}
