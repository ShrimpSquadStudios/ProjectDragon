using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMovement : MonoBehaviour {

    // refactor this shit (worker stats) to be in a class

    // Resource for worker to collect
    GameObject[] goals;
    GameObject collisionGO;

    // Get the world and world controller classes
    World world;
    WorldController worldController;

    enum resourceType {Iron,Wood};

    // No other script needs to know about hunger... for now
    private float hunger;
    private resourceType currentResource;
    private bool resourceCollected = false;

    // Use this for initialization
    void Start()
    {
        // Initialize hunger to 100 for each worker
        this.hunger = 100;
        worldController = GameObject.FindGameObjectWithTag("world").GetComponent<WorldController>();
        world = worldController.world;
        currentResource = resourceType.Wood;
    }

    // Update is called once per frame
    void Update()
    {
        // Every tick, make the workers hungrier. The number of workers / mill count will change the speed of death
        hunger -= (0.01f * (float)worldController.numWorkers) / ((float)(Building.millCount) + 1);
        Debug.Log(hunger);

        // If the worker starves, kill it and reduce worker count, otherwise move the worker
        if (hunger <= 0)
        {
            Destroy(gameObject);
            Destroy(this);
            worldController.numWorkers -= 1;
        }
        else
        {
            MoveObject();
        }

        // Check what type of resource is collected and increase the count of that resource
        if (resourceCollected)
        {
            Destroy(collisionGO);
            if (collisionGO.gameObject.tag == "Iron")
            {
                world.IncrementIronCount(1);
            }
            else if (collisionGO.gameObject.tag == "wood")
            {
                world.IncrementWoodCount(1);
            }
            resourceCollected = false;
            Tile tile_data = world.GetTileAt((int) collisionGO.gameObject.transform.position.x, (int) collisionGO.gameObject.transform.position.z);
            tile_data.Type = Tile.TileType.Empty;
        }

        // on r press, toggle the type of resource the worker collects
        if (Input.GetKeyDown("r"))
        {
            if (currentResource == resourceType.Iron)
            {
                currentResource = resourceType.Wood;
            }

            else if (currentResource == resourceType.Wood)
            {
                currentResource = resourceType.Iron;
            }
        }
    }

    // https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    // Find closest Iron and move towards it
    void MoveObject()
    {
        if (currentResource == resourceType.Iron)
        {
            goals = GameObject.FindGameObjectsWithTag("Iron");
        }
        
        else if (currentResource == resourceType.Wood)
        {
            goals = GameObject.FindGameObjectsWithTag("wood");
        }

        GameObject closestGoal = null;
        float distance  = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in goals)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestGoal = go;
                distance = curDistance;
            }
        }
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = 3;
        agent.destination = closestGoal.transform.position;
    }

    // On collision with Iron, collect iron
    // https://docs.unity3d.com/ScriptReference/Collision-gameObject.html
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Iron" || collision.gameObject.tag == "wood")
        {
            resourceCollected = true;
            collisionGO = collision.gameObject;
            world = worldController.world;
        }
    }

}


