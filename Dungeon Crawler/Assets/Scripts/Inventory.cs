using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	ItemDatabase database;

	public static List<Item> items = new List<Item> ();
	public static List<Item> equiped = new List<Item> ();

	public InputField inputField;
	public Text invText;

	public static string displayText;
	public static string userInput;

	public static int weaponDamage = 1;
	public static int armorModifier = 0;

	// For Bottom Display
	PlayerHealth playerHealth;

	//this value is modified first and sent to the display to be printed.
	public string healthText;
	public string aCText;
	public string attackText;
	public string hungerText;
	public string levelText;
	public string xPText;

	//this value is printed on the display.
	public Text healthDisplay;
	public Text aCDisplay;
	public Text attackDisplay;
	public Text hungerDisplay;	
	public Text levelDisplay;
	public Text xPDisplay;




	// Use this for initialization
	void Start () 
	{
		database = GetComponent<ItemDatabase> ();

		displayText = "Welcome to the game."; // this is the welcome message to the game.
		invText.text = displayText; // invText is what will be displayed to the player in the UI. displayText is what the functions feed into to store what will be fed into the UI.
		playerHealth = GetComponent<PlayerHealth> ();
		PlayerStats.TallyXP ();
		UpdateBottomDisplay ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.I)) //Player presses i to see what they are carrying.
		{
			DisplayInventory ();
			DisplayEquiped ();
		}
		if (Input.GetKeyDown (KeyCode.E)) //Player presses e to equip an item from their inventory.
		{
			DisplayInventory ();
			DisplayEquiped ();
			displayText = "What would you like to equip?" + "\n" + displayText + "\n" + " Select an Item:";
			invText.text = displayText;
			StartCoroutine (Equip ());
		}

		//this takes care of the hunger counter for now.
		hungerText = "Hunger: " + Hunger.hunger;
		hungerDisplay.text = hungerText;
	}

	public void DisplayInventory () //Prints what the player is carrying in their inventory to the display.
	{
		int i = 0;
		displayText = "Inventory:" + "\n";
		foreach (var item in items) 
		{
			displayText += i++ + " - " + item.Title + "\n";
		}
		invText.text = displayText;
	}
	public void DisplayEquiped () //Prints what the player has equiped to the display.
	{
		int i = 0;
		displayText += "\n" + "Equiped:" + "\n";
		foreach (var item in equiped) 
		{
			displayText += i++ + " - " + item.Title + "\n";
		}
		invText.text = displayText;
	}

	IEnumerator Equip()
	{
		//This coroutine activates the input field and waits for the user to enter a command by hitting return.
		//It then calls upon the function to add inventory to equiped.
		inputField.ActivateInputField ();
		inputField.text = "";

		while (!Input.GetKeyDown (KeyCode.Return)) 
		{
			userInput = inputField.text;
			yield return null;
		}
		AddInventoryToEquiped ();
	}

	IEnumerator Unequip()
	{
		Debug.Log ("in unequip");
		//This coroutine activates the input field and waits for the user to enter a command by hitting return.
		//It then calls upon the function to add equiped to inventory.
		invText.text = displayText;
		inputField.ActivateInputField ();
		inputField.text = "";

		while (!Input.GetKeyDown (KeyCode.Return)) 
		{
			userInput = inputField.text;
			yield return null;
		}
		AddEquipedToInventory ();
		DisplayInventory ();
		DisplayEquiped ();
	}

	IEnumerator WaitForYesNo()
	{
		bool YesNo = true;

		while (YesNo)
		{
			if (Input.GetKeyDown (KeyCode.Y)) 
			{
				DisplayInventory ();
				DisplayEquiped ();
				displayText = "What would you like to unequip?" + "\n" + displayText;
				invText.text = displayText;	
				StartCoroutine (Unequip ());
				YesNo = false;
			}
			else if (Input.GetKeyDown (KeyCode.N)) 
			{
				invText.text = "Nevermind.";
				YesNo = false;
			}
			yield return null;
		}

			
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Item")) //If player bumps into an item, this detects the other object as an item.
		{
			ItemIdentity newItem = other.gameObject.GetComponent<ItemIdentity> ();
			//This creates a new instance of the itemIdentity class which we can store the information held in the object we collided with

			SendItemToInventory (newItem.itemId); //here we use the information we just stored (just an int) and use that int to call upon the send item to inventory function, which creates an item and stores it in our inventory.
			Destroy(other.gameObject);
		}
	}

	public void SendItemToInventory (int id)
	{
		Item itemToAdd = database.FetchItemById (id);
		items.Add (itemToAdd);
		Debug.Log(itemToAdd.Title + (" has been added to the inventory."));
	}

	public void AddInventoryToEquiped()
	{
		Item itemToAdd = items [int.Parse (userInput)];
		if (CheckOverequiped (itemToAdd) == true)
		{
			invText.text = displayText;
			StartCoroutine (WaitForYesNo());
		} 
		else 
		{
			equiped.Add (itemToAdd);
			items.Remove (items [int.Parse (userInput)]);
			UpdateItemModifiers ();
			UpdateBottomDisplay ();
			DisplayInventory ();
			DisplayEquiped ();
		}
	}

	public bool CheckOverequiped(Item newItem)
	{
		//This function checks if the player is allowed to equip a new item. The player has a finite number of slots to fill.
		//It creates an int for each slot the player can fill and cycles through each item the player has equiped with a for loop.
		//It then adds the current value in the slot to the new items required number of slots (Item class can be passed through)
		//If any slot will be above limit, return true. Else, return false.
		int equipedHands = 0;
		int equipedFeet = 0;
		int equipedHead = 0;
		int equipedBody = 0;
		int equipedLegs = 0;

		for (int i = 0; i < equiped.Count; i++) 
		{
			equipedHands += equiped [i].Hands;
			equipedFeet += equiped [i].Feet;
			equipedHead += equiped [i].Head;
			equipedBody += equiped [i].Body;
			equipedLegs += equiped [i].Legs;
		}

		if (equipedHands + newItem.Hands > 2) 
		{
			displayText = "Your hands are full." + "\n" + "Would you like to unequip an item?" + "\n" + "Y/N";
			return true;
		} 
		else if (equipedFeet + newItem.Feet > 1) 
		{
			displayText = "You already have something on your feet." + "\n" + "Would you like to unequip an item?" + "\n" + "Y/N";
			return true;
		}
		else if (equipedHead + newItem.Head > 1) 
		{
			displayText = "You already have something on your head." + "\n" + "Would you like to unequip an item?" + "\n" + "Y/N";
			return true;
		}
		else if (equipedBody + newItem.Body > 1) 
		{
			displayText = "You are already wearing something on your body." + "\n" + "Would you like to unequip an item?" + "\n" + "Y/N";
			return true;
		}
		else if (equipedLegs + newItem.Legs > 1) 
		{
			displayText = "You are already wearing something on your legs." + "\n" + "Would you like to unequip an item?" + "\n" + "Y/N";
			return true;
		}
		else 
		{
			return false;
		}
	}

	public void AddEquipedToInventory()
	{
		Item itemToAdd = equiped [int.Parse (userInput)];
		items.Add (itemToAdd);
		equiped.Remove (equiped [int.Parse (userInput)]);
		UpdateItemModifiers ();
		UpdateBottomDisplay ();
	}

	public void UpdateItemModifiers()
	{
		weaponDamage = equiped.Sum (item => item.Damage);
		armorModifier = equiped.Sum (item => item.Armor);
		PlayerHealth.AC = 10 - armorModifier;
		Debug.Log("weaponDamage is now " + weaponDamage + " based off of " + equiped [0].Title);
		Debug.Log("armorModifier is now " + armorModifier + " based off of " + equiped [0].Title);
	}

	public void UpdateBottomDisplay () //this function updates the text ui at the bottom of the view.
	{
		healthText = "Health: " + playerHealth.playerHealth;
		aCText = "AC: " + PlayerHealth.AC;
		attackText = "Attack: D" + Inventory.weaponDamage;
		levelText = "Level: " + PlayerStats.Level;
		xPText = "XP: " + PlayerStats.XP;

		healthDisplay.text = healthText;
		aCDisplay.text = aCText;
		attackDisplay.text = attackText;
		levelDisplay.text = levelText;
		xPDisplay.text = xPText;
	}
}