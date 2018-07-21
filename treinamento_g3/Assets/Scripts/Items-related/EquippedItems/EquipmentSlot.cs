using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour {

    //A reference to the item
    Weapons item;
    //A reference to the iten's image
    public Image icon;
    //A reference to the iten's remove button
    public Button removeButton;

    public void AddItem(Weapons newItem)
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

    public void Unequip(int slotIndex)
    {
        if (EquipmentManager.instance.currentEquipment[slotIndex] != null)
        {
            Inventory.instance.Add(EquipmentManager.instance.currentEquipment[slotIndex]);
            EquipmentManager.instance.currentEquipment[slotIndex] = null;
            //this.GetComponent<EquipmentManager>().slots[this.GetComponent<EquipmentManager>().slotIndex].ClearSlot();
            //if (EquipmentManager.instance.onItemChangedCallBackEquipped != null)
                //EquipmentManager.instance.onItemChangedCallBackEquipped.Invoke();
        }
    }
}
