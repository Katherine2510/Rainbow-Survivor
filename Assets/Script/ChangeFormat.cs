using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeFormat : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _componentToDisable;
    [SerializeField] private MeshRenderer Box;
    [SerializeField] private float _scaleFactor = 0.25f;
    [SerializeField] private GameObject _objectScaler;

    public PlayerMovement _player;

    public float _boxSpeed = 3;
    public float _moveSpeed = 7;

    public Button yourButton;

    public bool isBox = false;
    Button btn;


   void Start () {
		btn = yourButton.GetComponent<Button>();
        
        
		btn.onClick.AddListener(delegate () { this.ChangeFormatPlayer(); });
	}

	
    public void DisableComponent()
    {
        if (_componentToDisable != null && Box != null)
        {
            
            _componentToDisable.enabled = false;
            Box.enabled = true;
            _player.MoveSpeed = _boxSpeed;
            
        }
    }

    public void EnableComponent()
    {
        if (_componentToDisable != null && Box != null)
        {
            
            _componentToDisable.enabled = true;
            Box.enabled = false;
            _player.MoveSpeed = _moveSpeed;
            
        }
    }
    public void ChangeFormatPlayer()
    {
        if (!isBox)
        {
            _objectScaler.transform.localScale *= _scaleFactor;
            DisableComponent();

        }
        else
        {
            _objectScaler.transform.localScale /= _scaleFactor;
            EnableComponent();
        }

        isBox = !isBox;
    }
     private void Update()
    {
       
    }
}
