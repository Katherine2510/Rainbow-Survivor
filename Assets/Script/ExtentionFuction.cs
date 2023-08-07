using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ExtentionFuction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _player;
    public GameObject _character;
    float previousSpeed;
    
    public GameObject _body;
    public Button _speed;
    public Button _magnet;
    public Button _changeCam;
    public CinemachineVirtualCamera Camera;
    public float maxRoom = 100;
    public float startRoom = 60;
 
  
    Vector3 postionBlock = Vector3.zero;
    private Transform holdPosition;

    bool isClickOnMagnet = false;


    void Start()
    {
        previousSpeed = _player.GetComponent<PlayerMovement>().MoveSpeed;
        _speed.onClick.AddListener(RiseSpeed);
        _changeCam.onClick.AddListener(ChangeCam);
        _magnet.onClick.AddListener(isMagnet);
        startRoom = Camera.m_Lens.FieldOfView;
    }
 

    // Update is called once per frame
    void Update()
    {
        if (isClickOnMagnet) {
            Magnet();
        }
        
    }

    void RiseSpeed() {
        if (_body.GetComponent<ChangeFormat>().isBox == false)
        _player.GetComponent<PlayerMovement>().MoveSpeed  = previousSpeed + 3;

    }

    void Magnet() {
        _character.GetComponent<CharacterMovement>().Magnet();
       
    }
    void isMagnet() {
        isClickOnMagnet = true;
    }
    void ChangeCam() {
        if(Camera.m_Lens.FieldOfView < maxRoom)
       Camera.m_Lens.FieldOfView += 20;
       else
       {
        Camera.m_Lens.FieldOfView = startRoom;
       }
    }

    


}
