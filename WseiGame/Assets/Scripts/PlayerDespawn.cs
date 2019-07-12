using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDespawn : MonoBehaviour
{
    float Speed = 0.35f;
    public bool playAnimation = false;

    void Update()
    {
        if (playAnimation == true)
        {
            if (transform.localPosition.y > 0.0f)
            {
                transform.localPosition -= new Vector3(0f, Speed * Time.deltaTime, 0f);
            }
            else
            {
                transform.parent.GetComponent<PlayerEnd>().LoadNextLevel();
            }
        }
    }
}
