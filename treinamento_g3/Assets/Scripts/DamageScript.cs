using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

    public float Dano;
    public float Velocidade;
    private Rigidbody2D RB;

	void Start () {
        RB = gameObject.GetComponent<Rigidbody2D>();
        RB.velocity += new Vector2(-Velocidade, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().LevouDano(Dano);
            Destroy(gameObject);
        }
    }
}
