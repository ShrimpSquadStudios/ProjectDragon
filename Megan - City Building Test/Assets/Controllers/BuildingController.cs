using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    public GameObject prefab; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");

            // Ray cast to get mouse position
            // https://forum.unity.com/threads/placing-objects-with-a-mouse-click.66121/ 
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray,out hit))
            {
                Debug.Log("Hit location" + hit.transform);
                Transform objectHit = hit.transform;

                Instantiate(prefab, objectHit.position, transform.rotation);
            }       
        }
    }
}
