﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;

    // Get and set resources
    static int ironCount;
    public int GetIronCount()
    {
        return ironCount;
    }

    public void IncrementIronCount(int ironGathered)
    {
        ironCount = ironCount + ironGathered;
    }

    static int woodCount = 5;
    public int GetWoodCount()
    {
        return woodCount;
    }

    public void IncrementWoodCount(int woodGathered)
    {
        woodCount = woodCount + woodGathered;
    }

    int width;
    public int Width
    {
        get
        {
            return width;
        }
    }

    int height;
    public int Height
    {
        get
        {
            return height;
        }
    }

    // Creates a tiled world
    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }

        }

        Debug.Log("World created with " + (width * height) + " tiles.");
    }

    // Finds tile type at location
    public Tile GetTileAt(int x, int y)
    {
        if (x > width || x < 0 || y > height || y < 0)
        {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }

        return tiles[x, y];
    }
}
