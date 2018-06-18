using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
	public static int Level;
	public static int XP;
	
	public int Strength;
	public int Dexterity;
	public int Constitution;
	public int Intelligence;
	public int Wisdom;
	public int Charisma;


	public static void TallyXP() // This function checks xp and sets player to appropriate level.
	{ 
		if (0 <= XP && XP < 20) 
		{
			Level = 1;
		} 
		else if (20 <= XP && XP < 40)
		{
			Level = 2;
		} 
		else if (40 <= XP && XP < 80) 
		{
			Level = 3;
		} 
		else if (80 <= XP && XP < 160) 
		{
			Level = 4;
		} 
		else if (160 <= XP && XP < 320) 
		{
			Level = 5;
		} 
		else if (320 <= XP && XP < 640)
		{
			Level = 6;
		} 
		else if (640 <= XP && XP < 1280)
		{
			Level = 7;
		} 
		else if (1280 <= XP && XP < 2560)
		{
			Level = 8;
		}
		else if (2560 <= XP && XP < 5120)
		{
			Level = 9;
		}
		else if (5120 <= XP && XP < 10000) 
		{
			Level = 10;
		}
	}
}
