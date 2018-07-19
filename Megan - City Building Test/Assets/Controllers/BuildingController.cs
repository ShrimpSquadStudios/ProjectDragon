using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    public GameObject prefabHouse;
    public GameObject prefabMill;
    public GameObject prefabCastle;

    World world;
    
    public WorldController worldController;

    Building.BuildingType buildingType;

    private GameObject selectionGO;

    // Use this for initialization
    void Start ()
    {
        world = worldController.world;
        buildingType = Building.BuildingType.Castle;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // switch building type on B press
        if (Input.GetKeyDown("b"))
        {
            if (buildingType == Building.BuildingType.House)
            {
                buildingType = Building.BuildingType.Mill;
            }

            else if (buildingType == Building.BuildingType.Mill)
            {
                buildingType = Building.BuildingType.House;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Ray cast to get mouse position
            // https://forum.unity.com/threads/placing-objects-with-a-mouse-click.66121/ 
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool validPlacement = true;

            if (Physics.Raycast (ray,out hit))
            {
                // Raycast found collider
                Debug.Log("Hit location" + hit.transform.position);
                Transform objectHit = hit.transform;

                int currentX = (int)objectHit.transform.position.x;
                int currentZ = (int)objectHit.transform.position.z;

                
                // If type of tile is not building, add the building and set the type to building
                if (world.GetWoodCount() >= 5)
                {
                    validPlacement = CheckPlacement(validPlacement, objectHit, currentX, currentZ);

                    if (validPlacement)
                    {
                        if (buildingType == Building.BuildingType.Castle)
                        {
                            selectionGO = prefabCastle;
                            buildingType = Building.BuildingType.House;
                            worldController.SpawnWorker(objectHit.position);
                            Building.castleCount ++;
                        }

                        else if (buildingType == Building.BuildingType.House)
                        {
                            selectionGO = prefabHouse;
                            Building.houseCount++;
                        }

                        else if (buildingType == Building.BuildingType.Mill)
                        {
                            selectionGO = prefabMill;
                            Building.millCount++;
                        }
                        placeBuilding(objectHit, currentX, currentZ);
                    }
                }
            }       
        }
    }

    private bool CheckPlacement(bool validPlacement, Transform objectHit, int currentX, int currentZ)
    {
        for (int a = currentX - 1; a <= currentX + 1; a++)
        {
            for (int b = currentZ - 1; b <= currentZ + 1; b++)
            {
                Tile tile_data = world.GetTileAt(a, b);
                if (tile_data.Type == Tile.TileType.Building)
                {
                    validPlacement = false;
                }
            }
        }

        return validPlacement;
    }

    private void placeBuilding(Transform objectHit, int currentX, int currentZ)
    {
        for (int a = currentX; a <= currentX; a++)
        {
            for (int b = currentZ; b <= currentZ; b++)
            {
                Tile tile_data = world.GetTileAt(a, b);
                tile_data.Type = Tile.TileType.Building;

                if (a == currentX && b == currentZ)
                {
                    Instantiate(selectionGO, objectHit.position, transform.rotation);
                    world.IncrementWoodCount(-5);
                }
            }
        }
    }
}
