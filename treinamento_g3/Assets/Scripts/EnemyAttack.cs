using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public bool MeleeEnemy;                                     //Booleana que mostra se o inimigo é corpo a corpo ou a distância
    public float Damage;                                        //Dano causado pelo inimigo
    public float Range;                                         //A distancia que o inimigo pode atacar
    public float VisionRange;                                   //A distancia que o inimigo pode detectar o jogador
    public float DamageCooldown;                                //o tempo que leva para o inimigo desferir outro golpe
    private float DamageTimer;                                  //Contador que controla o tempo de recarga do ataque
    public GameObject RangedAttackObject;                       //O objeto que será atirado pelo inimigo
    public GameObject RangedAttackSpawner;                      //O local aonde o inimigo irá utilizar para disparar os projéteis
    private GameObject Player;                                  //O objeto do jogador


    void Start()
    {
        Player = GameObject.FindWithTag("Player");              //Find the object with the tag "Player"
        DamageTimer = 0;                                        //Reseta o contador de tempo        
    }

    private void Update()
    {
        if (!MeleeEnemy)                                        //Caso for um inimigo de ataque à distância 
        {
            float Dist;
            Dist = Distance(gameObject.transform.position, Player.transform.position);
            if (Dist < Range && gameObject.GetComponent<EnemyController>().DetectedPlayer)                                   //Ataque o joagador caso ele esteja dentro do raio de ataque e esteja vendo o jogador
            {
                RangedAttack();                                       
            }
        }
        else if(MeleeEnemy)                                      //Caso for um inimigo corpo a corpo 
        {
            float Dist;
            Dist = Distance(gameObject.transform.position, Player.transform.position);
            if (Dist < Range) //Ataque o joagador caso ele esteja dentro do raio de ataque
            {
                MeleeAttack();
            }
        }
    }

    public void MeleeAttack()
    {
        if (DamageTimer < Time.time)
        {
            // Player.GetComponent<PlayerController>().TakeDamage(Damage);
            print("Player Levou Dano:" + Damage);
            DamageTimer = Time.time + DamageCooldown;
        }
    }

    public float Distance(Vector3 VectorX, Vector3 VectorY)
    {
        float Distance;
        Vector3 Delta = VectorX - VectorY;
        Distance = Mathf.Sqrt((Delta.x * Delta.x) + (Delta.z * Delta.z));

        return Distance;
    }

    public void RangedAttack()
    {
        if (DamageTimer < Time.time)
        {
            DamageTimer = Time.time + DamageCooldown;
            Instantiate(RangedAttackObject, RangedAttackSpawner.transform);
        }
    }
}
