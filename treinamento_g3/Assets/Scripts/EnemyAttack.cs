﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool MeleeEnemy;                                     //Booleana que mostra se o inimigo é corpo a corpo ou a distância
    public float Damage;                                        //Dano causado pelo inimigo
    public float Range;                                         //A distancia que o inimigo pode atacar
    public float VisionRange;                                   //A distancia que o inimigo pode detectar o jogador
    public float DamageCooldown;                                //o tempo que leva para o inimigo desferir outro golpe
    public float ProjectileSpeed;                               //Velocidade dos ataques do inimigo
    private float DamageTimer;                                  //Contador que controla o tempo de recarga do ataque
    public GameObject RangedAttackObject;                       //O objeto que será atirado pelo inimigo
    public GameObject RangedAttackSpawner;                      //O local aonde o inimigo irá utilizar para disparar os projéteis
    private GameObject Player;                                  //O objeto do jogador
    private GameObject Projectile;
    private AudioSource audioSource;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");              //Find the object with the tag "Player"
        DamageTimer = 0;                                        //Reseta o contador de tempo     
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!MeleeEnemy && !gameObject.GetComponent<EnemyController>().dead)                                        //Caso for um inimigo de ataque à distância 
        {
            float Dist;
            Dist = Distance(gameObject.transform.position, Player.transform.position);
            if (Dist < Range && gameObject.GetComponent<EnemyController>().DetectedPlayer)                                   //Ataque o joagador caso ele esteja dentro do raio de ataque e esteja vendo o jogador
            {
                RangedAttack();      
                if(Dist <  Range * 0.2f)
                {
                    gameObject.GetComponent<EnemyController>().Agent.isStopped = true;
                }
            }
        }
        else if(MeleeEnemy && !gameObject.GetComponent<EnemyController>().dead)                                      //Caso for um inimigo corpo a corpo 
        {
            
            float Dist;
            Dist = Distance(gameObject.transform.position, Player.transform.position);
            
            if (Dist < Range)                                   //Ataque o joagador caso ele esteja dentro do raio de ataque
            {
                MeleeAttack();
                gameObject.GetComponent<EnemyController>().Agent.isStopped = true;
            }
        }
    }

    public void MeleeAttack()
    {
        if (DamageTimer < Time.time && !gameObject.GetComponent<EnemyController>().dead)
        {
            Player.GetComponent<PlayerController>().TakeDamage(Damage);
            DamageTimer = Time.time + DamageCooldown;
        }
    }

    public float Distance(Vector3 VectorX, Vector3 VectorY)
    {
        float Distance;
        Vector3 Delta = VectorX - VectorY;
        Distance = Delta.magnitude;
        return Distance;
    }

    public void RangedAttack()
    {
        if (DamageTimer < Time.time)
        {
            RangedAttackSpawner.transform.LookAt(Player.transform);
            if (audioSource!= null)
            {
                Debug.Log("Attacking");
                audioSource.Play();
            }
                
            
            DamageTimer = Time.time + DamageCooldown;
            Projectile = Instantiate(RangedAttackObject, RangedAttackSpawner.transform);
            Projectile.GetComponent<RangedEnemyProjectile>().InitiateProjectile(Range, ProjectileSpeed,Damage);
        }
    }
}
