using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlignement : MonoBehaviour
{
    private Transform cam;
    private GameObject healthBar;
    public Vector3 offset;
    public float smoothSpeed = 5f;
    void Start()
    {
        cam = Camera.main.transform;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
    }

    void Update()
    {
        healthBar.transform.LookAt(healthBar.transform.position + cam.forward); // Aligns the UI forward
    }
    public void CameraSettings()
    {
        Vector3 desiredPosition = healthBar.transform.position + offset; // Desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smooth movement
        cam.transform.position = smoothedPosition;
        cam.transform.LookAt(healthBar.transform.position); // Camera looks at the player
    }
}
