using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
	Vector3 movement;
	int floorMask;
	float camRayLength = 100f;

    void Awake()
    {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log(this.gameObject + " is immortal");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start");
        rb = GetComponent<Rigidbody>();
		floorMask = LayerMask.GetMask ("Floor");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

		Move (moveHorizontal, moveVertical);

		Turning ();
	}

	void Move (float moveHorizontal, float moveVertical)
	{
		movement.Set (moveHorizontal, 0f, moveVertical);
		movement = movement.normalized * speed * Time.deltaTime;
		rb.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;

			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			rb.MoveRotation (newRotation);
		}
	}
}
