using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileProperties : MonoBehaviour {

	//UIController uiController;
	public Rigidbody projectileBody;


	// Use this for initialization
	void Start () 
	{
		//uiController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController> ();
		projectileBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
