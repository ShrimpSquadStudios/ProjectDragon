using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class PlayerStats : MonoBehaviour 
{
	PlayerHealth playerHealth;

	//public static int Level;
	public static int XP;
	
	public int Strength;
	public int Dexterity;
	public int Constitution;
	public int Intelligence;
	public int Wisdom;
	public int Charisma;

	//values for storing levels.
	public static int NewLevel;
	public int ActiveLevel;	
	public int [ , ] StoredLevelsArray = new int [2,11]; // This stores how many hit points the player had when they were at a particular
	//level. If the player falls below a level and reatains it, he will not re-roll health for that level.

	void Start()
	{
		playerHealth = GetComponent<PlayerHealth> ();
		ActiveLevel = 1;
	}

	public void CheckLevelUp() // level will feed into the stat upgrade function, but also the Class StoredLevels.
	{
	//	if (StoredLevelsArray [0, ActiveLevel] /= null) 
	//	{
	//		playerHealth.maxHealth = StoredLevelsArray [1, ActiveLevel];
	//	}
		if (NewLevel > ActiveLevel && StoredLevelsArray [0, NewLevel] == 0) {
			StatsUpgrade ();
			ActiveLevel = NewLevel;
			StoredLevelsArray [0, ActiveLevel] = ActiveLevel;
			StoredLevelsArray [1, ActiveLevel] = playerHealth.maxHealth;
			Debug.Log ("The array contains maxhealth of: " + StoredLevelsArray [1, ActiveLevel] + " for level: " + StoredLevelsArray [0, ActiveLevel]);
		} 
		else if (NewLevel > ActiveLevel && StoredLevelsArray [0, NewLevel] != 0) 
		{
			playerHealth.maxHealth = StoredLevelsArray [1, NewLevel];
		}
	}

	public void StatsUpgrade ()
	{
		playerHealth.maxHealth = playerHealth.maxHealth + Constitution;
		playerHealth.playerHealth = playerHealth.playerHealth + Constitution;
	}




	public static void TallyXP() // This function checks xp and sets player to appropriate level.
	{ 
		if (0 <= XP && XP < 20) 
		{
			NewLevel = 1;
		} 
		else if (20 <= XP && XP < 40)
		{
			NewLevel = 2;
		} 
		else if (40 <= XP && XP < 80) 
		{
			NewLevel = 3;
		} 
		else if (80 <= XP && XP < 160) 
		{
			NewLevel = 4;
		} 
		else if (160 <= XP && XP < 320) 
		{
			NewLevel = 5;
		} 
		else if (320 <= XP && XP < 640)
		{
			NewLevel = 6;
		} 
		else if (640 <= XP && XP < 1280)
		{
			NewLevel = 7;
		} 
		else if (1280 <= XP && XP < 2560)
		{
			NewLevel = 8;
		}
		else if (2560 <= XP && XP < 5120)
		{
			NewLevel = 9;
		}
		else if (5120 <= XP && XP < 10000) 
		{
			NewLevel = 10;
		}
	}
}
