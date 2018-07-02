using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

    public float damage;
    public float speed;
    private Rigidbody2D rigidBody;

	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.velocity += new Vector2(-speed, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().EnemyTakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
