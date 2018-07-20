using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemsSlot : MonoBehaviour {

    //A reference to the item
    Item item;
    //A reference to the iten's image
    public Image icon;
    //A reference to the iten's remove button
    public Button removeButton;

    //Adds an item
    public void AddItem(Item newItem)
    {
        //the item to be added
        item = newItem;
        //its sprite
        icon.sprite = item.sprite;
        //enable its icon
        icon.enabled = true;
        //actives the remove button
        removeButton.interactable = true;

    }

    public void ClearSlot()
    {
        //clears and disable the slot
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        //removes the removebutton
        removeButton.interactable = false;
    }
    //THIS FUNCTION IS CALLED THROUGH THE BUTTON INSPECTOR (this functions )
    public void OnRemoveButton()
    {
        //removes an item from the inventory
        EquippedItems.instance.Remove(item);
    }
}
