using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This command adds a create option for "item" in the assets folder
[CreateAssetMenu(fileName = "New Item", menuName = "Invetory/Item")]
public class Item : ScriptableObject {
    //these are the properties of general items for the invetory;
    new public string name = "New Item";
    public Sprite sprite;
    public GameObject item = null;
    public bool equipable = false;
    public bool packable = false;

    //If an item may be used, then this function shall be overrided with its use
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}
