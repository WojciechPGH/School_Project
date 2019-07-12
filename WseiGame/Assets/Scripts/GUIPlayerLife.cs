using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPlayerLife : MonoBehaviour
{
    int lifes = 5;
    private GameObject[] hearths;

    private void Start()
    {
        hearths = new GameObject[lifes];
        for(int i = 0; i < lifes; i++)
        {
            hearths[i] = transform.GetChild(i).gameObject;
        }
    }

    public void UpdateLife(int life)
    {
        for(int i = 0; i < lifes; i++)
        {
            if(i < life)
            {
                hearths[i].SetActive(true);
            }
            else
            {
                hearths[i].SetActive(false);
            }
        }
    }
}
