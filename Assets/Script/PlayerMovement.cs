using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float TurnSpeed = 2f;
    [SerializeField] private FloatingJoystick _joystick;
    Vector2 InputAxes;
    float Angle;
    //float GroundDistance;
    Quaternion TargetRotation;
    Transform MainCamera;
    CharacterController Player;
     [SerializeField] private Transform _pivot;
    private float gravity = 9.81f;
    private Vector3 velocity;

    public Transform target; // Vị trí đích
 
    void Start() {
        Player = GetComponent<CharacterController> ();
        MainCamera = Camera.main.transform;
    }
 
    private void FixedUpdate()
    {
        GetInput();

       if (Mathf.Abs(InputAxes.x) > 0.1f || Mathf.Abs(InputAxes.y) > 0.1f)
        {
            CalculateDirection();
            Rotate();
            float smoothness = 10f;
            Vector3 movement = transform.forward * MoveSpeed * Time.deltaTime;
            Player.Move(movement);
            Vector3 _pivotPosition =  this.transform.position;
            _pivot.position = Vector3.Lerp(_pivot.position, _pivotPosition, smoothness * Time.deltaTime);
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
        velocity = Vector3.zero;
        }
        Player.Move(velocity * Time.deltaTime);
        
        /*GetInput();

        // Tính toán vector di chuyển dựa trên input từ joystick
        Vector3 moveDirection = new Vector3(InputAxes.x, 0f, InputAxes.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed;
        // Áp dụng trọng lực
        velocity.y -= gravity * Time.deltaTime;
        // Áp dụng di chuyển vào Character Controller
        Player.Move((moveDirection + velocity) * Time.deltaTime);
       */
    }
 
    void GetInput() {
       
         InputAxes.x = _joystick.Horizontal;
         InputAxes.y = _joystick.Vertical;
        
    }
 
    void CalculateDirection() {
        Angle = Mathf.Atan2 (InputAxes.x, InputAxes.y);
        Angle = Mathf.Rad2Deg * Angle;
        Angle += MainCamera.eulerAngles.y;
    }
 
    void Rotate() {
        TargetRotation = Quaternion.Euler (0, Angle, 0);
        transform.rotation = Quaternion.Slerp (transform.rotation, TargetRotation, TurnSpeed * Time.deltaTime);
    }
     
}