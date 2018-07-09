using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	
	Rigidbody projectileRigidbody;
	Slider projectileMassSlider;
	static float projectileMass;
	Text projectileMassValue;

	Rigidbody counterweightRigidbody;
	Slider counterweightMassSlider;
	static float counterweightMass;
	Text counterweightMassValue;


	void Start () 
	{
		projectileRigidbody = GameObject.FindGameObjectWithTag ("Projectile").GetComponent<Rigidbody> ();
		projectileMassSlider = GameObject.FindGameObjectWithTag ("ProjectileMassSlider").GetComponent<Slider> ();
		projectileMassValue = GameObject.FindGameObjectWithTag ("ProjectileMassValue").GetComponent<Text> ();
		projectileMassSlider.value = projectileMass;

		counterweightRigidbody = GameObject.FindGameObjectWithTag ("Counterweight").GetComponent<Rigidbody> ();
		counterweightMassSlider = GameObject.FindGameObjectWithTag ("CounterweightMassSlider").GetComponent<Slider> ();
		counterweightMassValue = GameObject.FindGameObjectWithTag ("CounterweightMassValue").GetComponent<Text> ();
		counterweightMassSlider.value = counterweightMass;
	}
	

	void Update () 
	{
		projectileRigidbody.mass = projectileMassSlider.value;
		projectileMass = projectileMassSlider.value;
		projectileMassValue.text = projectileMass.ToString ();

		counterweightRigidbody.mass = counterweightMassSlider.value;
		counterweightMass = counterweightMassSlider.value;
		counterweightMassValue.text = counterweightMass.ToString ();



	}
}
