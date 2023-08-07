using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    private float rotSpeed = 2;
    Vector3 currentEulerAngles;
    float rotX = 0f;
    float rotY = -90f;
    public LayerMask targetLayer;
    public int _screenWidth = 602;
    public int _screenHeight = 400;
    [SerializeField] private GameObject follow;
    private bool isRotating = false;
    public float damping = 10;
    public float minDistance = 3f;
    public float maxDistance = 10f;
    public float zoomSpeed = 2f;

    private void Start()
    {

    }

    private void Update()
    {
        //Ấn chuột nhận sự kiện quay với điệu kiện chuột di trong vùng cho phép Drag
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > _screenWidth / 2 && Input.mousePosition.x < _screenWidth - 100 && Input.mousePosition.y > 100)
        {
            isRotating = true;
        }
        //Nhả chuột, không cho phép quay
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
        //Được phép quay, nhận các giá trị của chuột
        if (isRotating)
        {
            rotX += Input.GetAxis("Mouse X") * rotSpeed;
            rotY += Input.GetAxis("Mouse Y") * rotSpeed;
            rotY = Mathf.Clamp(rotY, -120f, 50f);
            
                Quaternion targetRotation = Quaternion.Euler(rotY, rotX, 0);
                transform.DOLocalRotateQuaternion(targetRotation, Time.deltaTime * damping);
            
            
               // follow.transform.position -= new Vector3(0,Input.GetAxis("Mouse Y"), 0 );
            
        }

        // Zooming in and out
        /*float scroll = Input.GetAxis("Mouse ScrollWheel");
        float distance = Vector3.Distance(transform.position, follow.transform.position);
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 targetPosition = follow.transform.position - transform.forward * distance;

        // Smoothly move the camera closer to the player's position on ground touch
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxDistance, targetLayer))
        {
            if (!isRotating)
            {
                targetPosition = hit.point + Vector3.up * minDistance;
            }
        }

        transform.DOMove(targetPosition, Time.deltaTime * damping);
        */

     
    }
}
