using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour {

    public Sprite spr_grass;
    public Sprite spr_empty;
    public GameObject planePrefab;
    public GameObject WoodPrefab;
    public GameObject IronPrefab;
    public GameObject worker;

    public World world;

	// Use this for initialization
	void Start () {
        world = new World();

        // Create a GameObject for each tile for raycasting
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                GameObject tile_go = new GameObject();
                Tile tile_data = world.GetTileAt(x, y);

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X , 0 , tile_data.Y);
                tile_go.transform.SetParent(this.transform, true);

                Instantiate(planePrefab, tile_go.transform.position, transform.rotation);
                planePrefab.name = "Plane_" + x + "_" + y;

                // Randomly decide if a harvest node should be placed
                int placeNode = UnityEngine.Random.Range(0, 50);

                switch (placeNode)
                {
                    case 0:
                        Instantiate(WoodPrefab, tile_go.transform.position, transform.rotation);
                        tile_data.Type = Tile.TileType.Building;
                        break;
                    case 1:
                        Instantiate(IronPrefab, tile_go.transform.position, transform.rotation);
                        tile_data.Type = Tile.TileType.Building;
                        break;
                    default:
                        break;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("w"))
        {
            print("Spawn Worker");
            SpawnWorker();
        }
    }

    public void SpawnWorker()
    {
        if (world.GetIronCount() >= 5)
        {
            Instantiate(worker, this.transform.position, this.transform.rotation);
            world.IncrementIronCount(-5);
        }
    }
}