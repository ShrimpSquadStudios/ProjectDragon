using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainController : MonoBehaviour {

    public WorldController worldcontroller;

    public Text ironCountText;
    public Text woodCountText;
    public Text workerCountText;
    public Text buildingCountText;

    public Button spawnWorkerButton;

    int ironCount = 0;
    int woodCount = 0;
    int workerCount = 0;
    int buildingCount = 0;


	void Start () {
        World world = worldcontroller.world;
    }

    // Update is called once per frame
    void Update () {
        ironCount = worldcontroller.world.GetIronCount();

        /// TODO: Hook up once the backend exists
        // woodCount = worldcontroller.world.GetWoodCount();
        // workerCount = worldcontroller.world.GetWorkerCount();
        // buildingCount = worldcontroller.world.GetBuildingCount();

        if (ironCount >= 5)
            ironCountText.text = "<color=green>" + ironCount.ToString() + "</color>";
        else
            ironCountText.text = "<color=white>" + ironCount.ToString() + "</color>";

        woodCountText.text = woodCount.ToString();
        workerCountText.text = workerCount.ToString();
        buildingCountText.text = buildingCount.ToString();
	}
}
