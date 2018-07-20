using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsUI : MonoBehaviour
{

    //An transform for refering to the parent of all items equipmentSlots
    public Transform itemsParent;
    //An array for refering to all the equipmentSlots in itemsParent
    EquippedItemsSlot[] equipmentSlots;
    //A reference to our inventory
    EquippedItems inventory;
    //references to the entire inventory
    public GameObject equippeditemsUI;

    // Use this for initialization
    void Start()
    {
        //Our inventory
        inventory = EquippedItems.instance;
        //If an item was changed, update the UI
        inventory.onItemChangedCallBack += UpdateUI;
        //The array of equipmentSlots receives the equipmentSlots
        equipmentSlots = itemsParent.GetComponentsInChildren<EquippedItemsSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        //If you press "i", the inventory is hidden
        if (Input.GetKeyDown("i"))
        {
            //this transform the inventory active property into its opposite
            equippeditemsUI.SetActive(!equippeditemsUI.activeSelf);
        }
    }

    //Maintain the inventory panel updated
    void UpdateUI()
    {
        //checks all equipmentSlots
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            //adds the items from the inventory
            if (i < inventory.items.Count)
            {
                equipmentSlots[i].AddItem(inventory.items[i]);
            }
            //else, clear the slot
            else
            {
                equipmentSlots[i].ClearSlot();
            }
        }
    }
}
