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
    //Player script responsible for updating the weapon on the player
    private PlayerEquip playerEquip;

    _item aux = new _item();

    private void Start()
    {
        playerEquip = this.GetComponentInParent<EquipmentManager>().playerEquip;
    }

    //Adds an item
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

    //THIS FUNCTION IS CALLED THROUGH THE BUTTON INSPECTOR
    public void Unequip()
    {
        int slotIndex = (int)item.equipSlot;
        if (EquipmentManager.instance.currentEquipment[slotIndex] != null)
        {
            aux.element = EquipmentManager.instance.currentEquipment[slotIndex];
            //Adds the item to the inventory
            Inventory.instance.Add(aux);
            aux = new _item();
            //Removes the item from the array of equipped items
            EquipmentManager.instance.currentEquipment[slotIndex] = null;
            //this.GetComponent<EquipmentManager>().slots[this.GetComponent<EquipmentManager>().slotIndex].ClearSlot();
            //if (EquipmentManager.instance.onItemChangedCallBackEquipped != null)
                //EquipmentManager.instance.onItemChangedCallBackEquipped.Invoke();
            ClearSlot();
            playerEquip.UpdateWeapon();
        }
    }
}
