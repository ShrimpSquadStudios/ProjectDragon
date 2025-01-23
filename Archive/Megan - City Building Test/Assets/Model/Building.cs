using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {

    public enum BuildingType { House, Mill, Castle };
    public static int castleCount = 0;
    public static int houseCount = 0;
    public static int millCount = 0;

    BuildingType Type = BuildingType.House;

}