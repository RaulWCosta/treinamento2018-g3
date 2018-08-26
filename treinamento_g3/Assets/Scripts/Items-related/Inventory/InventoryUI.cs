using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    //An transform for refering to the parent of all items slots
    private Transform itemsParent;
    //An array for refering to all the slots in itemsParent
    InventorySlot[] slots;
    //A reference to our inventory
    Inventory inventory;
    Image inventoryUI;

	// Use this for initialization
	void Start () {
        //Our inventory
        inventory = Inventory.instance;
        //If an item was changed, update the UI
        inventory.onItemChangedCallBack += UpdateUI;
        itemsParent = this.gameObject.transform.GetChild(0);
        inventoryUI = itemsParent.GetComponent<Image>();
        //The array of slots receives the slots
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
        //If you press "i", the inventory is hidden
        if (Input.GetKeyDown("i"))
            OpenClose_Inventory();
	}

    //Maintain the inventory panel updated
    void UpdateUI ()
    {
        //checks all slots
        for (int i = 0; i < slots.Length; i++)
        {
            //adds the items from the inventory
            if (i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            //else, clear the slot
            else
                slots[i].ClearSlot();
        }
    }
    public void OpenClose_Inventory()
    {
        inventoryUI.enabled = !inventoryUI.enabled;
    }
}
