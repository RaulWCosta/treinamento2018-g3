using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {
    //The item to be picked up
    public Item item;

    //override for the interact funcion at "Interactable.cs"
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        //true if the item was picked, false if not
        bool wasPickedUp = Inventory.instance.Add(item);
        //if it was picked, destroy it
        if (wasPickedUp == true)
            Destroy(this.gameObject);
    }
}
