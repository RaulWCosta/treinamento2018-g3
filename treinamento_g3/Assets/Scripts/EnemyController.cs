using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Range(1, 100)]
    public int hpMax;
    private float currentHp;
    private float range;
    [Range(1.0f, 10.0f)]
    private float movimentVelocity;
    private int level = 0;
    private int damage;

    // Use this for initialization
    void Start () {
        currentHp = hpMax + level * 10;
        damage = 10 * level / 2;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void EnemyTakeDamage(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
            Destroy(gameObject);
        /*healthBar.value = currentHp/hpMax * 100; // Diminuir a barra de vida de acordo com o currentHp*/
    }

    public void EnemyGainHp(float amount)
    {
        if (currentHp + amount <= hpMax)
            currentHp += amount;
        else
            currentHp = hpMax;
    }
}
