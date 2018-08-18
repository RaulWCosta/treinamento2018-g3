using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Invetory/Potion")]
public class Potions : Item {

    public potion type;
    public float bonus;
    private PlayerController player = PlayerController.instance;

    public override void Use ()
    {
        if ((int)type == 0 && PlayerController.instance.hpCurrent + bonus <= PlayerController.instance.hpMax)
            PlayerController.instance.hpCurrent += bonus;
        else if ((int)type == 0)
            PlayerController.instance.hpCurrent = PlayerController.instance.hpMax;
        /* else if ((int) type == 1 && 'Mana' + bonus <= 'TotalMana')
         *      Mana += bonus;
         * else
         *      Mana = TotalMana;
        */
    }

}

public enum potion { HP, Mana };