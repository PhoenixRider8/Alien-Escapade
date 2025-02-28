using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    //Sensitivity
    [Header("Sensitivity")]
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    
    public void ProcessLook(Vector2 input)
    {
        //Mouse input
        float mouseX = input.x;
        float mouseY = input.y;

        //Camera rotation calculation for up/down look
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //Apply transform to camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotate Player itself to look left/right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
