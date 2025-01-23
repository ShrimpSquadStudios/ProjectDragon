using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

	public enum TileType { Empty, Water, Mountain, Building, Path };

    Action<Tile> changeTileType;


	TileType type = TileType.Empty;
	public TileType Type {
		get {
			return type;
		} 
		set {
            TileType oldType = type;

			type = value;

            // update tile type if needed
            if (changeTileType != null && oldType != type)
            {
                changeTileType(this);
            }
		}
	}

	StaticObject staticObject;
	MovableObject movableObject;

	World world;

	int x;
    public int X
    {
        get
        {
            return x;
        }
    }

    public int y;

	int z;
    public int Z
    {
        get
        {
            return z;
        }
    }

	public Tile(World world, int x, int z) {
		this.world = world;
		this.x = x;
        this.y = 0;
		this.z = z;
        type = TileType.Empty;
	}

    public void RegisterTileUpdated(Action<Tile> callback)
    {
        changeTileType += callback;
    }

    public void UnregisterTileUpdated(Action<Tile> callback)
    {
        changeTileType -= callback;
    }
}
