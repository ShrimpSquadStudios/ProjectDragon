using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

// for reference this inventory was based off or the tutorial by gamegrind at: https://www.youtube.com/watch?v=x24t4DCxGh8

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item> ();
	private JsonData itemData;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	void Start()
	{
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); // this grabs and stores the data in items.json. We use the data in our constructor
		ConstructItemDatabase ();

		Debug.Log(database[1].Title);
		Debug.Log (FetchItemById (0).Title);
	}

	public Item FetchItemById (int id)
	{
		for (int i = 0; i < database.Count; i++)
			if (database [i].Id == id) 

				return database [i];
			return null;
	}

	void ConstructItemDatabase()
	{
		for ( int i = 0; i < itemData.Count; i++) 
		{
			database.Add(new Item (
				(int)itemData [i] ["id"],
				(string)itemData [i] ["type"],
				(string)itemData [i] ["title"], 
				(int)itemData [i] ["damage"], 
				(int)itemData [i] ["armor"],
				(int)itemData [i] ["hands"],
				(int)itemData [i] ["feet"],
				(int)itemData [i] ["head"],
				(int)itemData [i] ["body"],
				(int)itemData [i] ["legs"])); // this constructs an object of an item and adds it to the database list. It is taking data from the 
			//items.json that was stored in item data. It will run until there are no more lines of itemData left to read.
		}
	}

}

public class Item // this class is a template for each item
{
	public int Id { get; set; }
	public string Type { get; set; }
	public string Title { get; set; }
	public int Damage { get; set; }
	public int Armor { get; set; }
	public int Hands { get; set; }
	public int Feet { get; set; }
	public int Head { get; set; }
	public int Body { get; set; }
	public int Legs { get; set; }

	public Item (int id, string type, string title, int damage, int armor, int hands, int feet, int head, int body, int legs)
	{
		this.Id = id;
		this.Type = type;
		this.Title = title;
		this.Damage = damage;
		this.Armor = armor;
		this.Hands = hands;
		this.Feet = feet;
		this.Head = head;
		this.Body = body;
		this.Legs = legs;
	}
}