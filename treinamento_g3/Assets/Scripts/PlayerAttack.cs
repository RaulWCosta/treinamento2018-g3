using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Basic player attack */
public class PlayerAttack : MonoBehaviour {

    Animator weaponAnimator;
    int weaponDamage;
    bool meleeWeapon;
    bool attacked = false;

	// Use this for initialization
	void Start () {
        WeaponProperties equippedWeaponProperties = GetComponent<WeaponProperties>();
        weaponDamage = equippedWeaponProperties.weaponDamage;
        meleeWeapon = equippedWeaponProperties.meleeWeapon;
        weaponAnimator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
        

        //left mouse click
        if (Input.GetMouseButton(0))
        {   
            //if player isn't already attacking
            if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                //turn on attack and reset "already attacked" bool
                weaponAnimator.SetBool("attacking", true);
                attacked = false;
            }

        }
        else //no click
        {
            if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !weaponAnimator.GetBool("attacking"))
            {
                //reset "already attacked" bool
                attacked = false;
            }
        }
        
	  
  
	}

    void OnTriggerStay2D(Collider2D other)
    {

        //if attacking and collider has tag Enemy
        if (meleeWeapon && other.tag == "Enemy" && !attacked)
        {
            //if attack is in progress
            if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                //make enemy take damage
                other.GetComponent<EnemyController>().EnemyTakeDamage(weaponDamage);
                attacked = true; //already attacked the enemy
                
            }
        }
      
            
    }
}
