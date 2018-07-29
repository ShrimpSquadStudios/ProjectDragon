using UnityEngine;

public class WorldController : MonoBehaviour {

    // temp tile sprites
	public Sprite sprGrass;
	public Sprite sprWater;
	public Sprite sprMountain;
    public Sprite sprBuilding;
    public Sprite sprPath;

	World world;

	void Start () {
        // spawn world with default tiles
		world = new World();
			
        // Create a game object for each tile
		for (var x = 0; x < world.Width; x++) {
			for (var z = 0; z < world.Height; z++) {
				GameObject goTile = new GameObject();
                Tile tile = world.GetTileAt(x, z);
                
                goTile.name = "Tile_" + x + '_' + z;
                goTile.transform.position = new Vector3(tile.X, 0, tile.Z);
                goTile.transform.Rotate(Vector3.right * 90);
                goTile.transform.SetParent(this.transform, true);

				goTile.AddComponent<SpriteRenderer>();
                goTile.GetComponent<SpriteRenderer>().sprite = sprGrass;

                tile.RegisterTileUpdated ( 
                    (t) => { OnTileUpdated(t, goTile); } 
                );
			}
		}

        world.GenerateTiles();
	}

    void Update() { }

    // Update tile sprite when tile type changes
    void OnTileUpdated(Tile tile, GameObject goTile)
    {
        switch (tile.Type)
        {
            case Tile.TileType.Empty:
                goTile.GetComponent<SpriteRenderer>().sprite = sprGrass;
                break;
            case Tile.TileType.Water:
                goTile.GetComponent<SpriteRenderer>().sprite = sprWater;
                break;
            case Tile.TileType.Mountain:
                goTile.GetComponent<SpriteRenderer>().sprite = sprMountain;
                break;
            case Tile.TileType.Building:
                goTile.GetComponent<SpriteRenderer>().sprite = sprBuilding;
                break;
            case Tile.TileType.Path:
                goTile.GetComponent<SpriteRenderer>().sprite = sprPath;
                break;
            default:
                Debug.LogError("Invalid tile type in OnTileTypeChanged()");
                break;
        }
    }
}
