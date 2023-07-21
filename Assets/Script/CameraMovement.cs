using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraMovement : MonoBehaviour
{
    public float rotSpeed = 10;
    Vector3 currentEulerAngles;
    float rotX;
    float rotY;
     public LayerMask targetLayer;

     public int _screenWidth = 602;
     public int _screenHeight = 400;

     [SerializeField] private GameObject follow;
    bool canRotate = true;

   private bool isRotating = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > _screenWidth / 2 && Input.mousePosition.x < _screenWidth - 100 && Input.mousePosition.y > 100 && canRotate)
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            rotX += Input.GetAxis("Mouse X") * rotSpeed;
            rotY += Input.GetAxis("Mouse Y") * rotSpeed;
            if (rotY < -120f) rotY = -120f;
            if (rotY > 50f) rotY = 50f;

            transform.localRotation = Quaternion.Euler(rotY, rotX, 0);
        }
    }
}
