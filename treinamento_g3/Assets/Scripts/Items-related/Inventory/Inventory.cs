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
    public List<_item> items = new List<_item>();
    //public List<Item> items = new List<Item>();
    private int index = 0;

    //Adds items
    public bool Add (_item item)
    {
        //checks if there is enough space
        if (items.Count >= space)
            // if not, returns false so that the item is not destroyed
            return false;
        else if (item.element.packable == true && (index = SearchItem(item.element.name)) != -1)
        {
            items[index].amount += 1;

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();

            return true;
        }
        else
        {
            //else, adds the item and destroys it
            item.amount += 1;
            items.Add(item);
            if (onItemChangedCallBack != null)
                // calls the delegate function
                onItemChangedCallBack.Invoke();
            return true;
        }
    }

    //Removes items
    public void Remove(_item item)
    {
        if ((index = SearchItem(item.element.name)) != -1 && items[index].amount != 1)
            items[index].amount--;
        else
            items.Remove(item);
        // calls the delegate function
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public int SearchItem(string name)
    {
        for (int i = 0; i < items.Count; i++)
            if (name.Equals(items[i].element.name, System.StringComparison.Ordinal))
                return i;
        return -1;
    }
}
