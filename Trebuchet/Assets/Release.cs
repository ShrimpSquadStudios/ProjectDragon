using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Release : MonoBehaviour {

	HingeJoint releasePin;

	// Use this for initialization
	void Start () {
		releasePin = GetComponent<HingeJoint> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			releasePin.breakForce = 0;
		}
	}

}
