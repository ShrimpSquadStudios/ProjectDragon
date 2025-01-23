using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralMovement : MonoBehaviour {

	Transform playerPosition;
	NavMeshAgent nav;

	// Use this for initialization
	void Start () 
	{
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		nav.SetDestination (playerPosition.position);
	}
}
