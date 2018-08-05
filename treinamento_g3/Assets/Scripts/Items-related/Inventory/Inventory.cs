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
    /*public struct _item
    {
        public Item element;
        public int amount;
    };
    public List<_item> items = new List<_item>();
    public _item aux;*/
    public List<Item> items = new List<Item>();
    private int index = 0;

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
        /*else if (item.packable == true && (index = SearchItem(item.name)) != -1)
        {
            

            return true;
        }*/
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

    /*public int SearchItem(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (name.Equals(items[i].name, System.StringComparison.Ordinal))
            {
                Debug.Log("Objeto já no invetario");
                return i;
            }
        }

        return -1;
    }*/
}
