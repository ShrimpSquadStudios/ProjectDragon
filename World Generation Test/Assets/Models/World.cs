using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class World {

    // global world settings
    const int WORLD_WIDTH = 128;
    const int WORLD_HEIGHT = 128;

    // mountain procedural generation settings
    const int MOUNTAIN_ODDS = 750; // inverse odds of a mountain spawning on a given tile
    const int MOUNTAIN_PASSES = 7; // number of map passes to fill out mountain tiles
    const int MOUNTAIN_FILL_ODDS = 3; // inverse odds of a mountain being filled by a pass
    const bool MOUNTAIN_FILL_GAPS = true; // whether to autofill 1x1 gaps in mountains

    // river procedural generation settings
    const int RIVER_CHANGE_ODDS = 10; // odds of the river changing in radius per z-value
    const int RIVER_SPAWN_RANGE = 4; // portion of the north side the river is allowed to spawn in
    const int RIVER_MIN_RADIUS = 3; // minimum radius of rivers
    const int RIVER_MAX_RADIUS = 5; // maximum radius of rivers

	Tile[,] tiles;
    List rvrTiles = new List();
    List mtnTiles = new List();

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

	public World(int width = WORLD_WIDTH, int height = WORLD_HEIGHT) {
		this.width = width;
		this.height = height;

        // Create a list of tiles
		tiles = new Tile[width, height];
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				tiles[x,z] = new Tile(this, x, z);
			}
		}

		Debug.Log ("World created with " + width * height + " tiles.");
	}

    // Randomly assign a type to each tile
    public void GenerateTiles() {
        ////// RIVERS //////
        Debug.Log("Generating river...");
        int midX = width / 2; // halfway X-value
        int curX = Random.Range(midX/RIVER_SPAWN_RANGE, width-midX/RIVER_SPAWN_RANGE); // x-value for the start point of the river
        bool startWest = curX < midX; // whether the river starts in the west half
        AddRvr(curX, height - 1);

        // basic generation (one tile wide from north to south)
        for (var z = height - 1; z >= 0; z--)
        {
            if ((curX < midX && startWest) || (curX > midX && !startWest))
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                    if (startWest)
                    {
                        if (curX < width - 1)
                            curX++;
                    }
                    else if (curX > 0)
                        curX--;
            }
            else // past center of map
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    if (curX < width - 1)
                        curX++;
                }
                else if (rand == 1)
                {
                    if (curX > 0)
                        curX--;
                }
            }
            AddRvr(curX, z);
        }
        UpdateTileTypes();

        // thicken river stochastically
        var radius = RIVER_MIN_RADIUS + (RIVER_MAX_RADIUS - RIVER_MIN_RADIUS) / 2;
        List newRiverTiles = new List();
        foreach (Tile t in rvrTiles)
        {
            int rand = Random.Range(0, RIVER_CHANGE_ODDS);
            switch (rand)
            {
                case 0:
                    if (radius < RIVER_MAX_RADIUS)
                        radius++;
                    break;
                case 1:
                    if (radius > RIVER_MIN_RADIUS)
                        radius--;
                    break;
            }
            for (var xPos = 1; xPos < radius; xPos++)
            {
                if (t.X - xPos > 0)
                    newRiverTiles.Add(tiles[t.X - xPos, t.Z]);
                if (t.X + xPos < width - 1)
                    newRiverTiles.Add(tiles[t.X + xPos, t.Z]);
            }            
        }

        foreach (Tile t in newRiverTiles)
            AddRvr(t.X, t.Z); // how can we make this not suck??
        UpdateTileTypes();
        
        ////// MOUNTAINS //////
        Debug.Log("Generating mountains...");

        // basic generation
        for (var x = 0; x < width; x++)
        {
            for (var z = 0; z < height; z++)
            {
                if (Random.Range(0, MOUNTAIN_ODDS) == 0)
                {
                    AddMtn(x, z); // this tile
                    if (z > 0) // above
                        AddMtn(x, z - 1);
                    if (z < height - 1) // below
                        AddMtn(x, z + 1);
                    if (x > 0)
                    {
                        AddMtn(x - 1, z); // left
                        if (z > 0)
                            AddMtn(x - 1, z - 1); // top left
                        if (z < height - 1)
                            AddMtn(x - 1, z + 1); // bottom left
                    }
                    if (x < width - 1)
                    {
                        AddMtn(x + 1, z); // right
                        if (z > 0)
                            AddMtn(x + 1, z - 1); // top right
                        if (z < height - 1)
                            AddMtn(x + 1, z + 1); // bottom right
                    }
                    if (x > 1)
                        AddMtn(x - 2, z); // two left
                    if (x < width - 2)
                        AddMtn(x + 2, z); // two right
                    if (z > 1)
                        AddMtn(x, z - 2); // two above
                    if (z < height - 2)
                        AddMtn(x, z + 2); // two below
                }
            }
        }
        UpdateTileTypes();

        // If a tile is next to two or more mountains it has a chance to become a mountain
        for (var i = 0; i < MOUNTAIN_PASSES; i++) // multiple passes to fill in the blanks
        {
            Debug.Log("Mountain pass " + i);
            for (var x = 0; x < width; x++)
            {
                for (var z = 0; z < height; z++)
                {
                    var numNearbyMountains = CountNearbyMountains(x, z);

                    int rand = Random.Range(0, MOUNTAIN_FILL_ODDS);

                    if (rand == 0)
                    {
                        if (numNearbyMountains > 2)
                            tiles[x, z].Type = Tile.TileType.Mountain;
                        else if (numNearbyMountains > 0 && rand == 0)
                            tiles[x, z].Type = Tile.TileType.Mountain;
                    }
                }
            }

        }
        UpdateTileTypes();

        // Fill mountain gaps
        if (MOUNTAIN_FILL_GAPS)
            for (var x = 0; x < width; x++)
                for (var z = 0; z < height; z++)
                    if (CountNearbyMountains(x, z) == 4)
                        tiles[x, z].Type = Tile.TileType.Mountain;
	}

    // Returns a tile at the given coordinates
	public Tile GetTileAt(int x, int z) {
		if (x > width || x < 0 || z > height || z < 0) {
			Debug.LogError("Tile coordinates are invalid. (" + x + "," + z + ")");
			return null;
		}
		return tiles[x,z];
    }

    // Sets tiles in tile lists to the appropriate type
    void UpdateTileTypes()
    {
        foreach (Tile t in rvrTiles)
            t.Type = Tile.TileType.Water;
        foreach (Tile t in mtnTiles)
            if (t.Type != Tile.TileType.Water)
                t.Type = Tile.TileType.Mountain;
    }

    // Adds a given tile to the list of mountain tiles
    void AddMtn(int x, int z)
    {
        mtnTiles.Add(tiles[x, z]);
    }

    // Adds a given tile to the list of river tiles
    void AddRvr(int x, int z)
    {
        rvrTiles.Add(tiles[x, z]);
    }

    // Returns the number of mountains adjacent to a given tile coordinate
    int CountNearbyMountains(int x, int z)
    {
        var count = 0;
        if (x > 0)
            if (tiles[x - 1, z].Type == Tile.TileType.Mountain)
                count++;
        if (x < width - 1)
            if (tiles[x + 1, z].Type == Tile.TileType.Mountain)
                count++;
        if (z > 0)
            if (tiles[x, z - 1].Type == Tile.TileType.Mountain)
                count++;
        if (z < height - 1)
            if (tiles[x, z + 1].Type == Tile.TileType.Mountain)
                count++;
        return count;
    }
}
