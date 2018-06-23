using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    //An transform for refering to the parent of all items slots
    public Transform itemsParent;
    //An array for refering to all the slots in itemsParent
    InventorySlot[] slots;
    //A reference to our inventory
    Inventory inventory;
    //references to the entire inventory
    public GameObject inventoryUI;

	// Use this for initialization
	void Start () {
        //Our inventory
        inventory = Inventory.instance;
        //If an item was changed, update the UI
        inventory.onItemChangedCallBack += UpdateUI;
        //The array of slots receives the slots
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
        //If you press "i", the inventory is hidden
        if (Input.GetKeyDown("i"))
        {
            //this transform the inventory active property into its opposite
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}

    void UpdateUI ()
    {
        //checks all slots
        for (int i = 0; i < slots.Length; i++)
        {
            //adds the items from the inventory
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            //else, clear the slot
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
