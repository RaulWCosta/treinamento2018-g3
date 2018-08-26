using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {

    float speed;
    float range = 100000;
    float damage;
    float distance=0;

	
	// Update is called once per frame
	void Update () {
        //move bullet
        //check if range has been reached, if so, destroy itself
        transform.Translate(new Vector3(speed, 0, 0) * UnityEngine.Time.deltaTime*transform.localScale.x * -1);
        distance += speed * UnityEngine.Time.deltaTime;
    
        if (distance >= range)
            Destroy(this.gameObject);


    }

    public void InitiateBulletParameters(float speed, float range, float damage)
    {
        this.speed = speed;
        this.range = range;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag != "Player" && collision.tag != "Weapon")
        {
            print(collision.gameObject.name);
            //damage to enemy
            if (collision.tag == "Enemy" && collision.gameObject.name != "Boss")
            {
                collision.GetComponent<EnemyController>().ReceivedDamage(damage);
            }
            else if(collision.gameObject.name == "Boss")
            {
                collision.GetComponent<BossController>().ReceivedDamage(damage);
            }

            //gets destroyed if it hits anything
            Destroy(this.gameObject);
        }
        
    }
}
