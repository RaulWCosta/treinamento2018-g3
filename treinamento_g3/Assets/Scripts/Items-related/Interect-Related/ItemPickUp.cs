using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {
    //The item to be picked up
    public Item item;
    public _item aux = new _item();

    //override for the interact funcion at "Interactable.cs"
    public override void Interact()
    {
        base.Interact();
        aux.element = item;
        PickUp(aux);
    }

    private void PickUp(_item element)
    {
        //true if the item was picked, false if not
        bool wasPickedUp = Inventory.instance.Add(element);
        //if it was picked, destroy it
        if (wasPickedUp == true)
            Destroy(this.gameObject);
    }
}
