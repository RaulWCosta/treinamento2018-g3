using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region UniqueInvetory
    //this was copyed from internet and is usefull to refering to this script from another one
    public static Inventory instance;
    private void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("WARNING! More than one inventory found!");
            return;
        }
        instance = this;

    }
    #endregion

    public int space = 20;
    //Register if an item was changed
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;


    //Creates a list of items
    public List<Item> items = new List<Item>();

    //Adds items
    public bool Add (Item item)
    {
        //checks if there is enough space
        if (items.Count >= space)
        {
            // if not, returns false so that the item is not destroyed
            Debug.Log("Not enough room!");
            return false;
        }
        else
        {
            //else, adds the item and destroys it
            items.Add(item);
            if (onItemChangedCallBack != null)
                // calls the delegate function
                onItemChangedCallBack.Invoke();
            return true;
        }
    }

    //Removes items
    public void Remove(Item item)
    {
        items.Remove(item);
        // calls the delegate function
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
