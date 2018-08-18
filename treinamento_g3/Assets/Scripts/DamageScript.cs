using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

    public float Damage;
    public float Velocity;
    private Rigidbody2D RB;

	void Start () {
        RB = gameObject.GetComponent<Rigidbody2D>();
        RB.velocity += new Vector2(-Velocity, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().ReceivedDamage(Damage);
            Destroy(gameObject);
        }
    }
}
