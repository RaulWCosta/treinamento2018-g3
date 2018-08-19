using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Basic player attack */
public class PlayerAttack : MonoBehaviour {

    public GameObject bulletPrefab; 

    Animator weaponAnimator;
    bool meleeWeapon;
    bool attacked = false;
    Transform bulletExitPosition;
    WeaponProperties equippedWeaponProperties;

	// Use this for initialization
	void Start () {
        equippedWeaponProperties = GetComponent<WeaponProperties>();
        meleeWeapon = equippedWeaponProperties.meleeWeapon;
        weaponAnimator = GetComponent<Animator>();

        //if this is a ranged weapon
        //BulletExit should be an empty GameObject placed at where the bullet should first appear on the gun
        bulletExitPosition = transform.Find("BulletExit");

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

                //if it's a ranged weapon, shoot
                if (!meleeWeapon)
                    ShootWeapon();
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

    void OnTriggerStay(Collider other)
    {   
        //if attacking and collider has tag Enemy
        if (meleeWeapon && other.tag == "Enemy" && !attacked)
        {
            Debug.Log("Enemy attacked");
            
            //if attack is in progress
            if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                //make enemy take damage
                other.GetComponent<EnemyController>().ReceivedDamage(equippedWeaponProperties.weaponDamage);
                attacked = true; //already attacked the enemy
            }
        }
      
            
    }

    void ShootWeapon()
    {
        //instantiate shot out of bullet exit
        GameObject bullet = Instantiate(bulletPrefab, bulletExitPosition.position, bulletExitPosition.rotation);
        bullet.transform.localScale = transform.localScale;

     
        //shot should have the damage and the range of the weapon
        bullet.GetComponent<ProjectileControl>().InitiateBulletParameters(equippedWeaponProperties.bulletSpeed ,
            equippedWeaponProperties.range, equippedWeaponProperties.weaponDamage);


    }
}
