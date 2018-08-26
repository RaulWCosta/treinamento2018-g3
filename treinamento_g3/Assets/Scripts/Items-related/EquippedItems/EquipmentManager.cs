using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    //An transform for refering to the parent of all items slots
    private Transform itemsParent;
    //An array refering to all equipped items
    public Weapons[] currentEquipment;
    //Player script responsible for updating the weapon on the player
    [HideInInspector]
    public PlayerEquip playerEquip;
    public int slotIndex;
    //Reference to the quantity of equipment slots
    int numSlots;
    _item aux = new _item();


    //Equipped items slots
    EquipmentSlot[] slots;

    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
        playerEquip = GameObject.FindWithTag("Player").GetComponent<PlayerEquip>();
    }

    #endregion

    private void Start()
    {
        itemsParent = this.gameObject.transform.GetChild(0).transform;
        //Here it gets the number of equipment slots
        numSlots = System.Enum.GetNames(typeof(EquipmentSlotIndex)).Length;
        //an array with the size of equipment slots
        currentEquipment = new Weapons[numSlots];
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public void Equip (Weapons newItem)
    {
        //slotIndex receives the position of the given weapon, if melee, 0, else 1.
        slotIndex = (int) newItem.equipSlot;
        //If there is an item already equipped, add it to the inventory before changing equipped items
        if (currentEquipment[slotIndex] != null)
        {
            aux.element = currentEquipment[slotIndex];
            Inventory.instance.Add(aux);
            aux = new _item();
        }
        //equip the item
        currentEquipment[slotIndex] = newItem;
        playerEquip.UpdateWeapon();

    }

    public void UpdateUI()
    {
        //checks all slots
        for (int i = 0; i < slots.Length; i++)
        {
            //adds the items from the inventory
            if (i < numSlots && currentEquipment[i] != null)
                slots[i].AddItem(currentEquipment[i]);
            //else, clear the slot
            else
                slots[i].ClearSlot();
        }
    }
}
