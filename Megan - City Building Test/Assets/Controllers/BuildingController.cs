﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    public GameObject prefab;
    World world;

    WorldController worldController;

	// Use this for initialization
	void Start ()
    {
        worldController = GameObject.FindGameObjectWithTag("world").GetComponent<WorldController>();
        world = worldController.world;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ray cast to get mouse position
            // https://forum.unity.com/threads/placing-objects-with-a-mouse-click.66121/ 
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray,out hit))
            {
                // Raycast found collider
                Debug.Log("Hit location" + hit.transform.position);
                Transform objectHit = hit.transform;

                int currentX = (int)objectHit.transform.position.x;
                int currentZ = (int)objectHit.transform.position.z;

                // If type of tile is not building, add the building and set the type to building
                for (int a = currentX - 1; a <= currentX + 1; a++)
                {
                    for (int b = currentZ - 1; b <= currentZ + 1; b++)
                    {
                        Tile tile_data = world.GetTileAt(a, b);

                        if (tile_data.Type != Tile.TileType.Building)
                        {
                            if ((a == currentX) && (b == currentZ))
                            {
                                Instantiate(prefab, objectHit.position, transform.rotation);
                            }
                            tile_data.Type = Tile.TileType.Building;
                        }

                        else
                        {
                            Debug.Log("Building already at location");
                        }
                    }
                }     
            }       
        }
    }
}