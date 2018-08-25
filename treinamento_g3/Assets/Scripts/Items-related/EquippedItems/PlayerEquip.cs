using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles the equipment of weapons, spawning the correct 
 * game object at the player's hands
 **/
public class PlayerEquip : MonoBehaviour {
    
    //gameobject attached to player which gives you the position where weapon should be spawned
    public Transform spawnPoint; 
    //item that is currently in use (melee or ranged weapon)
    GameObject currentItem;
    //The main script contained in the EquipmentManager objects
    EquipmentManager equipmentManager;
    
    //slot of the current weapon
    int currentSlot = 0; //0: melee, 1: ranged, 2: potion

	void Start () {
        //equips initial weapon
        UpdateWeapon();
        //gets equipmentManager script
        equipmentManager = GameObject.FindWithTag("Inventory").GetComponentInChildren<EquipmentManager>();
	}

    void Update()
    {


        //if player presses 1, switches weapon
        if (Input.GetKeyUp(KeyCode.Alpha1))
            SwitchWeapon();

    }

    /**
     * Updates the weapon the player is holding. 
     * Call this when the equipped weapons are changed in the inventory
     * or when the players switch between melee and ranged weapons
     **/
    public void UpdateWeapon()
    {
        //get rid of current weapon: currentItem
        if(currentItem != null)
            Destroy(currentItem);

        //put new weapon in place
        //get weapon from inventory
        //spawn gameObject stored in currentItem
        if (equipmentManager)
            if (equipmentManager.currentEquipment[currentSlot])
            {
                currentItem = Instantiate(equipmentManager.currentEquipment[currentSlot].item, spawnPoint.transform.position, spawnPoint.transform.rotation, this.transform);
                    
            }

    }

    /**
     * Switches from melee weapon (slot 0) to ranged weapon (slot 1) or vice versa
     **/
    public void SwitchWeapon()
    {
        //Switches the number of the slot
        if (currentSlot == 0)
            currentSlot = 1;
        else
            currentSlot = 0;

        //Actually switches weapon
        UpdateWeapon();

    }
    
}
