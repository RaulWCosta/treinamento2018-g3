using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Invetory/Weapon")]
public class Weapons : Item {
    //these are the specific properties for weapons
    public int weaponDamage;
    public bool meleeWeapon;
    public float range;

    public override void Use()
    {
        base.Use();

    }
}
