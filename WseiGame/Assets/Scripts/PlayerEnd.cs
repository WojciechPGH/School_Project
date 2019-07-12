using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponentInChildren<PlayerDespawn>().playAnimation = true;
        }
    }

    public void LoadNextLevel()
    {
        GameObject.Find("MainObject").GetComponent<ReturnToMenu>().LoadNextLevel();
    }
}
