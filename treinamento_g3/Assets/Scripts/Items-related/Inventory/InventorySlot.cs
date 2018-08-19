using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour {

    //A reference to the item
    public _item item = new _item();
    //A reference to the item's and quantity image
    public Image icon;
    public Image edge;
    //A reference to the item's remove button
    public Button removeButton;
    public Texture2D texture;
    private Sprite[] quantities;
    private Transform player;


    private Image imageParent;
    private Image[] thisChildImage;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        quantities = Resources.LoadAll<Sprite>(texture.name);
        imageParent = this.GetComponentInParent<Image>();
        thisChildImage = this.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        for (int i = 0; i < thisChildImage.Length; i++)
            if (thisChildImage[i].enabled != imageParent.enabled && item.element != null)
                thisChildImage[i].enabled = imageParent.enabled;
    }

    //Adds an item
    public void AddItem(_item newItem)
    {
        //the item to be added
        item = newItem;
        //its sprite
        icon.sprite = item.element.sprite;
        edge.sprite = quantities[item.amount - 1];
        edge.enabled = true;
        //enable its icon
        icon.enabled = true;
        //actives the remove button
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        //clears and disable the slot
        item = new _item();
        icon.enabled = false;
        icon.sprite = null;
        edge.enabled = false;
        edge.sprite = null;
        //removes the removebutton
        removeButton.interactable = false;
    }
    //THIS FUNCTION IS CALLED THROUGH THE BUTTON INSPECTOR
    public void OnRemoveButton()
    {
        Instantiate(item.element.item, player.position, player.rotation);
        Inventory.instance.Remove(item);
        
    }
    //THIS FUNCTION IS CALLED THROUGH THE BUTTON INSPECTOR
    public void UseItem()
    {
        //if there is an item in the slot, use it
        if (item.element != null)
        {

            item.element.Use();
            Inventory.instance.Remove(item);
        }
    }
}
