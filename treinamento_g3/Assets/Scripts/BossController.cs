using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour {

    private float LastHitTime;
    private float LastFireTime;
    private bool Moved;
    private bool Shot;
    private bool GotPlayerPosition;
    private Vector3 PlayerPos;
    private NavMeshAgent Agent;
    private GameObject Player;
    private int ShotCounter;
    public int ShotNumber;
    public GameObject WeaponAxis;
    public GameObject ShotSpawn;
    public GameObject Projectile;
    public bool PlayerInZone;
    public bool Dash;
    public bool Fire;
    public bool Idle;
    public float FireInterval;
    public float IdleTimer;
    public float PreFireTimer;
    public float PosFireTimer;
    public float Velocity;
    public float DamageRanged;
    public float DamageMelee;
    public float Range;
    public float ProjectileSpeed;
    public float MeleeRange;
    public float HP;
    public float MaxHp;
    public float HitTimer;
    

    void Start ()
    {
        PlayerInZone = false;
        LastHitTime = 0;
        HP = MaxHp;
        Player = GameObject.FindWithTag("Player");
        Agent = gameObject.GetComponent<NavMeshAgent>();
        Agent.speed = Velocity;
        Idle = true;
        Shot = false;
        GotPlayerPosition = false;
        LastFireTime = 0;
        ShotCounter = 1;
	}
	
	void Update ()
    {
        if(PlayerInZone)
        {
            float Distance = (Player.transform.position - gameObject.transform.position).magnitude;
            if (MeleeRange > Distance)
            {
                if (LastHitTime + HitTimer < Time.time)
                {
                    LastHitTime = Time.time;
                    Player.GetComponent<PlayerController>().TakeDamage(DamageMelee);
                }
                Agent.isStopped = true;
                WeaponAxis.SetActive(false);
                Agent.destination = gameObject.transform.position;
            }
            else
            {
                Agent.isStopped = false;
            }

            if (Idle)
            {
                if (!GotPlayerPosition)
                {
                    StartCoroutine(Timer(IdleTimer, 0));
                    PlayerPos = Player.transform.position;
                    GotPlayerPosition = true;
                }
            }

            if (Dash)
            {
                Agent.destination = PlayerPos;

                if (Agent.velocity.magnitude != 0)
                {
                    Moved = true;
                }

                if (Agent.velocity.magnitude == 0 && Moved)
                {
                    Moved = false;
                    Dash = false;
                    Fire = true;
                    ShotCounter = 1;
                }

            }

            if (Fire)
            {
                WeaponAxis.SetActive(true);
                WeaponAxis.transform.LookAt(Player.transform);

                if (LastFireTime + FireInterval < Time.time)
                {
                    StartCoroutine(Timer(PreFireTimer, 1));
                    LastFireTime = Time.time;
                }

                if (Shot)
                {
                    ShotCounter++;
                    GameObject Bullet;
                    Shot = false;
                    Bullet = Instantiate(Projectile, ShotSpawn.transform.position, WeaponAxis.transform.rotation * Quaternion.Euler(new Vector3(90f, 0f, 0f)));
                    Bullet.GetComponent<BossProjectile>().InitiateProjectile(Range, ProjectileSpeed, DamageRanged);
                }

                if (ShotCounter > ShotNumber)
                {
                    ShotCounter = 1;
                    Fire = false;
                    Idle = true;
                    GotPlayerPosition = false;
                    WeaponAxis.SetActive(false);
                }
            }
        }
	}

    IEnumerator Timer(float Time,int Type)
    {
        yield return new WaitForSeconds(Time);
        if(Type == 0)
        {
            Idle = false;
            Dash = true;
        }
        else
        {
            Shot = true;
        }
    }

    public void ReceivedDamage(float DamageTaken)                           //Function to damage the enemy
    {
        HP -= DamageTaken;
        if (HP <= 0)
        {
            //call death animation
           // enemyAnimation.DeathAnimation();
            Idle = false;
            Fire = false;
            Dash = false;
            Agent.isStopped = true;
            //disactivates enemies

        }
        //call damage animation
        //enemyAnimation.DamageAnimation();
    }
}
