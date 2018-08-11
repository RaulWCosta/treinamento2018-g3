using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour {

    //A reference to the item
    public _item item = new _item();
    //A reference to the item's and quantity image
    public Image icon;
    public Image quantity;
    //A reference to the item's remove button
    public Button removeButton;
    public Sprite[] quantities;
    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    //Adds an item
    public void AddItem(_item newItem)
    {
        //the item to be added
        item = newItem;
        //its sprite
        icon.sprite = item.element.sprite;
        quantity.sprite = QuantitySprite(item);
        //enable its icon
        icon.enabled = true;
        //actives the remove button
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        //clears and disable the slot
        item = new _item();
        icon.sprite = null;
        icon.enabled = false;
        quantity.sprite = null;
        quantity.enabled = false;
        //removes the removebutton
        removeButton.interactable = false;
    }
    //THIS FUNCTION IS CALLED THROUGH THE BUTTON INSPECTOR
    public void OnRemoveButton()
    {
        /*_item removeItem = Inventory.instance.items.FirstOrDefault(items => items.element.name.Equals(item.element.name, System.StringComparison.Ordinal));
        if (removeItem != null)
        {*/
            Instantiate(item.element.item, player.position, player.rotation);
            Inventory.instance.Remove(item);
        //}
    }
    //THIS FUNCTION IS CALLED THROUGH THE BUTTON INSPECTOR
    public void UseItem()
    {
        //if there is an item in the slot, use it
        if (item.element != null)
        {
            item.element.Use();
            Inventory.instance.Remove(item);
        }
    }
    public Sprite QuantitySprite(_item element)
    {
        if (element.amount == 1)
            quantity.enabled = false;
        else if (element.amount == 2)
        {
            quantity.enabled = true;
            return quantities[0];
        }
        else if (element.amount == 3)
        {
            quantity.enabled = true;
            return quantities[1];
        }
        else if (element.amount == 4)
        {
            quantity.enabled = true;
            return quantities[2];
        }
        else if (element.amount == 5)
        {
            quantity.enabled = true;
            return quantities[3];
        }
        return null;
    }
}
