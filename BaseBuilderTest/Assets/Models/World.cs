using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {
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
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				int rand = Random.Range(0, 3);
				switch(rand) {
					case 0:
						tiles[x,z].Type = Tile.TileType.Grass;
						break;
					case 1:
						tiles[x,z].Type = Tile.TileType.Dirt;
						break;
					case 2:
						tiles[x,z].Type = Tile.TileType.Sand;
						break;
					default:
                        Debug.LogError("Invalid tile type in RandomizeTiles()");
                        break;
				}
			}
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
