using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSmooth : MonoBehaviour
{
    // Start is called before the first frame update
    public float mvar = 2000;
	 void Start()

	{
		Debug.Log ("success ME");
		GetComponent<Rigidbody>().AddForce (transform.forward * mvar);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
