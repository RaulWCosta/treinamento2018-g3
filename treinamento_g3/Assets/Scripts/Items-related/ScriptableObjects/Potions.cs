using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Invetory/Potion")]
public class Potions : Item {

    public potion type;
    public float bonus;
    private PlayerController player;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public override void Use ()
    {
        //if (quantity == 1)
            Inventory.instance.Remove(this);
        /*else
        {
            quantity--;
            sprite = quantitySprites[quantity - 1];
        }*/

        if ((int)type == 0)
        {
            player.hpCurrent += bonus;
        }
        else;
        {
            //player.Mana += bonus;
        }
    }

}

public enum potion { HP, Mana };