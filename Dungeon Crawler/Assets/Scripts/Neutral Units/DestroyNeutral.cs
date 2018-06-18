using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNeutral : MonoBehaviour {

	NeutralStats neutralStats;
	PlayerStats playerStats;
	Inventory inventory;
	int toHit;
	int playerDamageDealt;
	int monsterRoll;

	void Start()
	{
		neutralStats = this.GetComponent<NeutralStats> ();

		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
	}

    void OnTriggerEnter(Collider other) //if an enemy colides with the weapon this calculates toHit and compares it
	//to a d20 roll. If it is a success the player deals 1Dweapondamage to the monster.
    {

        if (other.gameObject.CompareTag("Weapon"))
        {
			playerStats = other.gameObject.GetComponentInParent<PlayerStats> ();
			toHit = (playerStats.Dexterity + playerStats.Strength);
			monsterRoll = Random.Range (1, 20);
			if (toHit > monsterRoll) 
			{
				playerDamageDealt = Random.Range (1, Inventory.weaponDamage);
				neutralStats.monsterHealth -= playerDamageDealt;
				Debug.Log ("To hit is " + toHit + ". Monster roll was " + monsterRoll + ".");
				Debug.Log ("You have struck the monster for " + playerDamageDealt + " damage.");
			} 
			else 
			{
				Debug.Log ("To hit is " + toHit + ". Monster roll was " + monsterRoll + ".");
				Debug.Log ("You miss the monster.");
			}
				
            if (neutralStats.monsterHealth <= 0)
            {
				PlayerStats.XP += neutralStats.monsterXP;
				PlayerStats.TallyXP ();
				inventory.UpdateBottomDisplay ();
				Debug.Log ("you are now level " + PlayerStats.Level);
                Destroy(this.gameObject); //object is deleted if health falls below zero.
            }
        }
    }    
}
