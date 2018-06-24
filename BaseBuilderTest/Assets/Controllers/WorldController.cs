using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    // tile sprites
	public Sprite spr_grass;
	public Sprite spr_dirt;
	public Sprite spr_sand;
    public Sprite spr_path;

	World world;

	void Start () {
        // spawn world with default tiles
		world = new World();
			
        // Create a game object for each tile
		for (var x = 0; x < world.Width; x++) {
			for (var z = 0; z < world.Height; z++) {
				GameObject tile_go = new GameObject();
                Tile tile_data = world.GetTileAt(x, z);
                
                tile_go.name = "Tile_" + x + '_' + z;
                tile_go.transform.position = new Vector3(tile_data.X, 0, tile_data.Z);
                tile_go.transform.Rotate(Vector3.right * 90);
                tile_go.transform.SetParent(this.transform, true);

				tile_go.AddComponent<SpriteRenderer>();
                tile_go.GetComponent<SpriteRenderer>().sprite = spr_grass;

                tile_data.RegisterChangeTileType( 
                    (tile) => { OnTileTypeChanged(tile, tile_go); } 
                );
			}
		}

        world.RandomizeTiles();
	}

    void Update() { }

    // Update tile sprite when tile type changes
    void OnTileTypeChanged(Tile tile, GameObject tile_go)
    {
        switch (tile.Type)
        {
            case Tile.TileType.Grass:
                tile_go.GetComponent<SpriteRenderer>().sprite = spr_grass;
                break;
            case Tile.TileType.Dirt:
                tile_go.GetComponent<SpriteRenderer>().sprite = spr_dirt;
                break;
            case Tile.TileType.Sand:
                tile_go.GetComponent<SpriteRenderer>().sprite = spr_sand;
                break;
            case Tile.TileType.Path:
                tile_go.GetComponent<SpriteRenderer>().sprite = spr_path;
                break;
            default:
                Debug.LogError("Invalid tile type in OnTileTypeChanged()");
                break;
        }
    }
}
