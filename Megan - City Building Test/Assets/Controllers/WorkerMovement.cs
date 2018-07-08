using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMovement : MonoBehaviour {

    GameObject[] goals;

    World world;
    WorldController worldController;

    // Use this for initialization
    void Start()
    {
        worldController = GameObject.FindGameObjectWithTag("world").GetComponent<WorldController>();
        world = worldController.world;
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    // https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    // Find closest Iron and move towards it
    void MoveObject()
    {
        goals = GameObject.FindGameObjectsWithTag("Iron");
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

    // https://docs.unity3d.com/ScriptReference/Collision-gameObject.html
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Iron")
        {
            world = worldController.world;
            Destroy(collision.gameObject);
            world.IncrementIronCount(1);
            Debug.Log(world.GetIronCount());
        }
    }
}


