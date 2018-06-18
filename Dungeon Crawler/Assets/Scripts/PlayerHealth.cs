using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int playerHealth;
	NeutralStats neutralStats;
	public static int AC;
	int hitChance;
	int damageReduction;
	int damageTakenAfterReduction;

	void Start()
	{
	AC = 10 - Inventory.armorModifier;
	}

	void OnTriggerEnter(Collider other) // if the player collides with a monster it compares the players hitchance to a D20.
	// if the player fails they take damage based on the monsters damage and if they have damage reduction.
	{
		if (other.gameObject.CompareTag("Neutral Unit"))
		{
			neutralStats = other.GetComponent<NeutralStats> ();

			if (AC >= 0) 
			{
				hitChance = 10 + AC + neutralStats.monsterLevel;
			} 
			else if (AC < 0) 
			{
				hitChance = 10 + Random.Range (AC, 0) + neutralStats.monsterLevel;
			}

			int rollDTwenty = Random.Range (1, 21);
			Debug.Log ("hitchance is " + hitChance + " roll is " + rollDTwenty);

			if (rollDTwenty < hitChance) 
			{
				if (AC >= 0) {
					playerHealth -= neutralStats.monsterDamage; 
					Debug.Log ("You have taken " + neutralStats.monsterDamage + " damage.");
				} 
				else if (AC < 0) 
				{
					damageReduction = Random.Range (1, Mathf.Abs (AC));
					damageTakenAfterReduction = neutralStats.monsterDamage - damageReduction;
					if (damageTakenAfterReduction < 1) 
					{
						damageTakenAfterReduction = 1;
					}
					playerHealth -= neutralStats.monsterDamage - damageReduction;
					Debug.Log ("You have taken " + damageTakenAfterReduction +
						" damage. " + neutralStats.monsterDamage + " minus " + damageReduction);
				}
			} 
			else 
			{
				Debug.Log ("Missed");
			}
				
		if (playerHealth <= 0) 
			{
			Destroy (this.gameObject); //player is deleted if health falls below zero.
			Debug.Log ("You are dead.");
		}
	}
			
}
}