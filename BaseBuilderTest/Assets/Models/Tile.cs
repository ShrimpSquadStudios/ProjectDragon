using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

	public enum TileType { Grass, Dirt, Sand, Path };

    Action<Tile> changeTileType;


	TileType type = TileType.Grass;
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
    public int X {
        get {
            return x;
        }
    }

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
		this.z = z;
	}

    public void RegisterChangeTileType(Action<Tile> callback)
    {
        changeTileType += callback;
    }

    public void UnregisterChangeTileType(Action<Tile> callback)
    {
        changeTileType -= callback;
    }
}
