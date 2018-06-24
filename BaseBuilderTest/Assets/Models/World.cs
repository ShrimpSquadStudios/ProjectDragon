using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class World {

    // procedural generation settings
    const int MOUNTAIN_ODDS = 500; // inverse odds of a mountain spawning on a given tile
    const int MOUNTAIN_PASSES = 5; // number of map passes to fill out mountain tiles
    const int MOUNTAIN_FILL_ODDS = 3; // inverse odds of a mountain being filled by a pass

	Tile[,] tiles;

	int width;
	public int Width {
		get {
			return width;
		}
	}

	int height;
	public int Height {
		get {
			return height;
		}
	}

	public World(int width = 128, int height = 128) {
		this.width = width;
		this.height = height;

		tiles = new Tile[width, height];

		for (int x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				tiles[x,z] = new Tile(this, x, z);
			}
		}

		Debug.Log ("World created with " + width * height + " tiles.");
	}

    // Randomly assign a type to each tile
    public void RandomizeTiles() {
        Debug.Log("Randomizing tiles.");

        ////// RIVERS //////
        List riverTiles = new List();

        ////// MOUNTAINS //////
        List mountainTiles = new List();
        for (var x = 0; x < width; x++)
        {
            for (var z = 0; z < height; z++)
            {
                int rand = Random.Range(0, MOUNTAIN_ODDS);
                if (rand == 0)
                {
                    mountainTiles.Add(tiles[x, z]); // this tile
                    if (z > 0) // above
                        mountainTiles.Add(tiles[x, z - 1]);
                    if (z < height - 1) // below
                        mountainTiles.Add(tiles[x, z + 1]);
                    if (x > 0)
                    {
                        mountainTiles.Add(tiles[x - 1, z]); // left
                        if (z > 0)
                            mountainTiles.Add(tiles[x - 1, z - 1]); // top left
                        if (z < height - 1)
                            mountainTiles.Add(tiles[x - 1, z + 1]); // bottom left
                    }
                    if (x < width - 1)
                    {
                        mountainTiles.Add(tiles[x + 1, z]); // right
                        if (z > 0)
                            mountainTiles.Add(tiles[x + 1, z - 1]); // top right
                        if (z < height - 1)
                            mountainTiles.Add(tiles[x + 1, z + 1]); // bottom right
                    }
                    if (x > 1)
                        mountainTiles.Add(tiles[x - 2, z]); // two left
                    if (x < width - 2)
                        mountainTiles.Add(tiles[x + 2, z]); // two right
                    if (z > 1)
                        mountainTiles.Add(tiles[x, z - 2]); // two above
                    if (z < height - 2)
                        mountainTiles.Add(tiles[x, z + 2]); // two below
                }
            }
        }

        // update tile types
        foreach (Tile t in mountainTiles)
        {
            if (t.Type != Tile.TileType.Water)
                t.Type = Tile.TileType.Mountain;
        }
        // if a tile is next to two or more mountains it has a chance to become a mountain
        for (var i = 0; i < MOUNTAIN_PASSES; i++) // multiple passes to fill in the blanks
        {
            Debug.Log("Mountain pass " + i);
            for (var x = 0; x < width; x++)
            {
                for (var z = 0; z < height; z++)
                {
                    var numNearbyMountains = 0;
                    if (x > 0)
                        if (tiles[x - 1, z].Type == Tile.TileType.Mountain)
                            numNearbyMountains++;
                    if (x < width - 1)
                        if (tiles[x + 1, z].Type == Tile.TileType.Mountain)
                            numNearbyMountains++;
                    if (z > 0)
                        if (tiles[x, z - 1].Type == Tile.TileType.Mountain)
                            numNearbyMountains++;
                    if (z < height - 1)
                        if (tiles[x, z + 1].Type == Tile.TileType.Mountain)
                            numNearbyMountains++;

                    int rand = Random.Range(0, MOUNTAIN_FILL_ODDS);
                    if (rand == 0)
                    if (numNearbyMountains > 2)
                    {
                        tiles[x, z].Type = Tile.TileType.Mountain;
                        Debug.Log("A new mountain was born!");
                    }
                    else if (numNearbyMountains > 0 && rand == 0)
                    {
                        tiles[x, z].Type = Tile.TileType.Mountain;
                        Debug.Log("A new mountain was born!");
                    }
                }
            }

        }

        // update tile types
        foreach (Tile t in mountainTiles) {
            if (t.Type != Tile.TileType.Water)
                t.Type = Tile.TileType.Mountain;
        }
	}

	public Tile GetTileAt(int x, int z) {
		if (x > width || x < 0 || z > height || z < 0) {
			Debug.LogError("Tile coordinates are invalid. (" + x + "," + z + ")");
			return null;
		}
		return tiles[x,z];
	}

}
