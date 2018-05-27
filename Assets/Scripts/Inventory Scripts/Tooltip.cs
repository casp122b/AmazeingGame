using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Item item;
    private string data;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ContructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ContructDataString()
    {
        data = "<color=#445BA6><b>" + item.Title + "</b></color>" + "\n\n<b>Stats:</b>" + "\nDamage: " + item.Damage + "\nDefense: " + item.Defense + "\n\n" + item.Description;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
