using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles the equipment of weapons, spawning the correct 
 * game object at the player's hands
 **/
public class PlayerEquip : MonoBehaviour {


    public GameObject EquipmentManager;
    public Transform spawnPoint;
    GameObject currentItem;
    EquipmentManager equipmentManager;

    int currentSlot = 0; //0: melee, 1: ranged, 2: potion

	void Start () {
        //equips initial weapon
        UpdateWeapon();
        equipmentManager = EquipmentManager.GetComponent<EquipmentManager>();
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
        if(equipmentManager)
            if(equipmentManager.currentEquipment[currentSlot])
                currentItem = Instantiate(equipmentManager.currentEquipment[currentSlot].item, spawnPoint.transform.position, spawnPoint.transform.rotation, this.transform);

   

        //spawn gameObject stored in currentItem
       
    }

    /**
     * Switches from melee weapon (slot 0) to ranged weapon (slot 1) or vice versa
     **/
    public void SwitchWeapon()
    {
        if (currentSlot == 0)
            currentSlot = 1;
        else
            currentSlot = 0;

        UpdateWeapon();

    }
    
}
