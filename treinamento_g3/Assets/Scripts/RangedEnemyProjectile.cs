using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour
{

    float speed;
    float range;
    float damage;
    float distance = 0;

    public void InitiateProjectile(float Range, float ProjectileSpeed,float Damage)
    {
        range = Range;
        speed = ProjectileSpeed;
        damage = Damage;

    }
        
    

    // Update is called once per frame
    void Update()
    {
        //move bullet
        //check if range has been reached, if so, destroy itself
        transform.Translate(UnityEngine.Time.deltaTime * transform.forward * -speed);
        distance += speed * UnityEngine.Time.deltaTime;

        if (distance >= range)
            Destroy(this.gameObject);


    }

    private void OnTriggerEnter(Collider collision)
    {
        print("AKI");
        if (collision.tag != "Enemy")
        {
            //damage to enemy
            if (collision.tag == "Player")
            {
                collision.GetComponent<PlayerController>().TakeDamage(damage);
                //gets destroyed if it hits anything
                Destroy(this.gameObject);
            }
        }

    }
}
