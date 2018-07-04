using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerMovement : MonoBehaviour {

    GameObject[] goals;
    public Text ironText;
    int ironCount;

    // Use this for initialization
    void Start()
    {
        ironCount = 0;
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
            Destroy(collision.gameObject);
            ironCount++;
            ironText.text = "Iron: " + ironCount.ToString();
        }
    }
}


