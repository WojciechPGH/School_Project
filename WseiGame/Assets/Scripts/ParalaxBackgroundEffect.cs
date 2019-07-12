using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackgroundEffect : MonoBehaviour
{
    public Material backgroundMaterial;
    public Material foregroundMaterial;
    public float bSpeed = 0.01f;
    public float fSpeed = 0.02f;

    public void UpdateBackground(Vector2 playerPos)
    {
        //transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, playerPos.x, 0.2f), transform.position.y, transform.position.z);
        //scrollSpeed = (playerPos.x - transform.position.x) * 5f;
        //if (Mathf.Abs(scrollSpeed) <= 0.01f)
        //    scrollSpeed = 0f;
        //transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
        Vector2 offset;
        offset.x = playerPos.x;
        offset.y = 0f;
        backgroundMaterial.SetTextureOffset("_MainTex", offset * bSpeed);
        foregroundMaterial.SetTextureOffset("_MainTex", offset * fSpeed);
    }
}
