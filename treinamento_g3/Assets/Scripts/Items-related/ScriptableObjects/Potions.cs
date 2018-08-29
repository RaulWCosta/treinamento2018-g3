using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Invetory/Potion")]
public class Potions : Item {

    public float bonus;

    public override void Use ()
    {
        if (PlayerController.instance.hpCurrent + bonus <= PlayerController.instance.hpMax)
            PlayerController.instance.hpCurrent += bonus;
        else
            PlayerController.instance.hpCurrent = PlayerController.instance.hpMax;
    }

}