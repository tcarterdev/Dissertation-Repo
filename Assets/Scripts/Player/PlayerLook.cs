using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLook : MonoBehaviour
{
    public PlayerInput playerInput;
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void LateUpdate()
    {
        float mouseX = playerInput.actions["Look"].ReadValue<Vector2>().x;
        float mouseY = playerInput.actions["Look"].ReadValue<Vector2>().y;

        xRotation -=(mouseY * Time.deltaTime) * ySensitivity;
        //clap rotation
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
