using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject cursor;

    Vector3 prevFramePos; // camera position last frame

	void Start () {
		
	}
	
	void Update () {
        // Update cursor position
        Vector3 curFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curFramePos.z = 0;
        cursor.transform.position = curFramePos;

        // Pan camera with right and middle mouse buttons
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 diff = prevFramePos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.Translate(diff);
        }

        // update position
        prevFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curFramePos.z = 0;
    }

    Tile GetHoveredTile()
    {
        return null;
    }
}
