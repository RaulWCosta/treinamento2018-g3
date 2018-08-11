using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Item", menuName = "Invetory/Weapon")]
public class Weapons : Item {
    //these are the specific properties for weapons
    public int weaponDamage;
    public float bulletSpeed;
    //this enum separetes melee and renged weapons
    public EquipmentSlotIndex equipSlot;
    public float range;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        //AddItem equipment Slot
        EquipmentManager.instance.UpdateUI();
    }
}

public enum EquipmentSlotIndex { Melee, ranged}
