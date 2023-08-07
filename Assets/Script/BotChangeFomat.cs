using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotChangeFomat : MonoBehaviour
{
   [SerializeField] private SkinnedMeshRenderer _componentToDisable;
    [SerializeField] private MeshRenderer Box;
    public float _boxSpeed = 3;
    public float _moveSpeed = 7;
    public bool isBox = false;
    GameObject[] Enemy;
    private Vector3 previousPosition;
   void Start () {
    Enemy = GameObject.FindGameObjectsWithTag("Enemy");
    previousPosition = this.transform.position;
	}
    public void DisableComponent()
    {
        if (_componentToDisable != null && Box != null)
        {
            
            _componentToDisable.enabled = false;
            Box.enabled = true;
            
        }
    }

    public void EnableComponent()
    {
        if (_componentToDisable != null && Box != null)
        {
            
            _componentToDisable.enabled = true;
            Box.enabled = false;

        }
    }
 
     private void Update()
    {
        foreach (GameObject enemy in Enemy) {
        float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
        if (distance < 5f) {
            this.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
            Vector3 changePosition = previousPosition;
            changePosition.y += 0.5f;
            this.transform.localPosition = changePosition;
            DisableComponent();
            
        } else {
            this.transform.localScale = new Vector3(1,1,1);
             this.transform.localPosition = previousPosition;
             EnableComponent();
        }
    }
       
    }
}
