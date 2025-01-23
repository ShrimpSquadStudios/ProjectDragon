using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	Vector3 offset;
	public float smoothing = 7f;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	void Start () 
	{
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector3 CameraPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, CameraPos, smoothing * Time.deltaTime);
	}
}
