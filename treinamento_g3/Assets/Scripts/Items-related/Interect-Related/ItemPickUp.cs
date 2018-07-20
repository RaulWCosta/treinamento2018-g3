using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {
    //The item to be picked up
    public Item item;

    //override for the interact funcion at "Interactable.cs"
    public override void Interact()
    {
        //Debug.Log("Interact2");
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        //Debug.Log("Picking up " + item.name);
        //true if the item was picked, false if not
        bool wasPickedUp = Inventory.instance.Add(item);
        //bool wasPickedUp = GameObject.Find("Player").GetComponent<Inventory>().Add(item);
        //if it was picked, destroy it
        if (wasPickedUp == true)
            Destroy(this.gameObject);
    }
}
