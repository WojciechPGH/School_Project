using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerPos;
    public float cameraOffset = 0.45f;
    public float cameraSpeed = 0.1f;
    ParalaxBackgroundEffect paralax;
    private void Start()
    {
        paralax = FindObjectOfType<ParalaxBackgroundEffect>();
    }

    private void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 camPos = transform.position;
        if (playerPos == null)
            return;
        Vector2 pPos = playerPos.position;

        //camPos = new Vector3(Mathf.SmoothStep(camPos.x, pPos.x, cameraSpeed), camPos.y, camPos.z);
        //paralax.UpdateBackground(camPos);
        cameraSpeed = (pPos.x - transform.position.x) * 5f;
        if (Mathf.Abs(cameraSpeed) <= 0.01f)
            cameraSpeed = 0f;
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        paralax.UpdateBackground(transform.position);
    }
}
