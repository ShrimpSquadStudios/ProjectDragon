using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMovement : MonoBehaviour {

    GameObject[] goals;

    public float collectTime = 3.0f;
    public GameObject collisionGO;

    private float timer;
    private bool timerActive = false;
    private bool resourceCollected = false;

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
         if (timerActive)
        {
            timer += 1 * Time.deltaTime;
            if (timer > collectTime)
            {
                timerActive = false;
                timer = 0.0f;
            }
        }

        if (!timerActive && resourceCollected)
        {
            Destroy(collisionGO);
            world.IncrementIronCount(1);
            resourceCollected = false;
            Debug.Log(world.GetIronCount());
        }

        else
        {
            MoveObject();
        }

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

    // On collision with Iron, collect iron
    // https://docs.unity3d.com/ScriptReference/Collision-gameObject.html
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Iron")
        {
            timerActive = true;
            resourceCollected = true;
            collisionGO = collision.gameObject;
            world = worldController.world;
        }
    }

}


