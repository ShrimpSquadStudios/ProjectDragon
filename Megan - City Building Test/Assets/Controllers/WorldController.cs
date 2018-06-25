using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController : MonoBehaviour {

    public Sprite spr_grass;
    public Sprite spr_empty;
    public GameObject planePrefab;

    World world;

	// Use this for initialization
	void Start () {
        world = new World();

        // Create a GameObject for each tile
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                GameObject tile_go = new GameObject();
                Tile tile_data = world.GetTileAt(x, y);

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X , 0 , tile_data.Y);
                tile_go.transform.Rotate(Vector3.right * 90);
                
                tile_go.transform.SetParent(this.transform, true);

                tile_go.AddComponent<SpriteRenderer>();
                tile_go.GetComponent<SpriteRenderer>().sprite = spr_empty;

                Instantiate(planePrefab, tile_go.transform.position, transform.rotation);
                tile_data.RegisterTileTypeChangedCallback( (tile) => { OnTileTypeChanged(tile, tile_go); });
            }
        }

        world.RandomizeTiles();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if (tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = spr_grass;
        }
        else
        {
            Debug.Log("Empty Chosen");
            return;
        }
    }
}