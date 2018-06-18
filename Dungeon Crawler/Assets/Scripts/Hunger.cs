using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour 
{

	public static float hunger;

	// Use this for initialization
	void Start () 
	{
		hunger = 100.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		hunger -= ((1 * Time.deltaTime));
	}
}
