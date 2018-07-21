using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    public Transform itemsParent;
    public Weapons[] currentEquipment;
    public GameObject equipmentUI;
    public EquipmentSlot[] slots;
    public int slotIndex;
    //public delegate void OnItemChanged();
    //public OnItemChanged onItemChangedCallBackEquipped;
    int numSlots;
    

    private void Start()
    {
        numSlots = System.Enum.GetNames(typeof(EquipmentSlotIndex)).Length;
        currentEquipment = new Weapons[numSlots];
        //this.onItemChangedCallBackEquipped += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public void Equip (Weapons newItem)
    {
        slotIndex = (int) newItem.equipSlot;

        if (currentEquipment[slotIndex] != null)
            Inventory.instance.Add(currentEquipment[slotIndex]);

        currentEquipment[slotIndex] = newItem;

        /*if (onItemChangedCallBackEquipped != null)
            // calls the delegate function
            onItemChangedCallBackEquipped.Invoke();*/
    }

    // Update is called once per frame
    void Update()
    {
        //If you press "i", the inventory is hidden
        if (Input.GetKeyDown("i"))
        {
            //this transform the inventory active property into its opposite
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
    }

    //Maintain the inventory panel updated
    /*void UpdateUI()
    {
        //checks all slots
        for (int i = 0; i < numSlots; i++)
        {
            //adds the items from the inventory
            if (currentEquipment[i] != null)
            {
                slots[i].AddItem(currentEquipment[i]);
            }
            else
                currentEquipment[slotIndex] = null;
        }
        
    }*/
}
