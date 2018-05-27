using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    ItemDB database;
    GameObject inventoryPanel;          //Reference for the Inventory Panel
    GameObject slotPanel;               //Reference for the Slot Panel
    public GameObject inventorySlot;    //Reference for the Slot prefab
    public GameObject inventoryItem;    //Reference for the Item prefab

    public List<Item> items = new List<Item>();                     //List of items
    public List<GameObject> slots = new List<GameObject>();         //List of slot game objects

    int slotAmount;                     //reference for the total amount of slots in the inventory


    void Start()
    {
        database = GetComponent<ItemDB>();
        slotAmount = 20;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = GameObject.Find("Slot Panel");
        //Genrates the inventory slots
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);
        AddItem(1);
    }

    //Used to add an item to the inventory
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemById(id);
        if (itemToAdd.Stackable && CheckIfItemInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                //Checks if the item ID is -1 which means it does not contain an item
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    break;
                }
            }
        }
    }

    bool CheckIfItemInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return true;
        }
        return false;
    }

}
