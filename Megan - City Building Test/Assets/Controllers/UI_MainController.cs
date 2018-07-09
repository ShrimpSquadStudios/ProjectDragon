using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainController : MonoBehaviour {

    public WorldController worldcontroller;

    public Text ironCountText;
    public Text workerCountText;
    public Text buildingCountText;

    int ironCount = 0;
    int workerCount = 0;
    int buildingCount = 0;


	void Start () {
        World world = worldcontroller.world;
    }

    // Update is called once per frame
    void Update () {
        ironCount = worldcontroller.world.GetIronCount();
        // workerCount = worldcontroller.world.GetWorkerCount();
        // buildingCount = worldcontroller.world.GetBuildingCount();
        ironCountText.text = ironCount.ToString();
        Debug.Log(ironCount);
	}
}
