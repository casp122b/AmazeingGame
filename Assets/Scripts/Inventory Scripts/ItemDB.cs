using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class ItemDB : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        //Maps the path to locate the Items.json file
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructDatabase();
    }

    public Item FetchItemById(int id)
    {
        //Looks through the items by the id
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];
        return null;
    }

    void ConstructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"],
                itemData[i]["title"].ToString(),
                (int)itemData[i]["stats"]["damage"],
                (int)itemData[i]["stats"]["defense"],
                itemData[i]["description"].ToString(),
                (bool)itemData[i]["stackable"],
                itemData[i]["rarity"].ToString(),
                itemData[i]["slug"].ToString()));
        }
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public string Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, int damage, int defense, string description, bool stackable, string rarity, string slug)
    {
        ID = id;
        Title = title;
        Damage = damage;
        Defense = defense;
        Description = description;
        Stackable = stackable;
        Rarity = rarity;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }
    
    public Item()
    {
        this.ID = -1;
    }
}
