using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject Vision;
    public NavMeshAgent Agent;                                  //O agente do inimigo, entidade que controla a movimentação do inimigo no navmesh
    public Transform Target;                                    //O "Alvo" a posição atual do jogador
    public float HP;                                            //Vida do inimigo
    public float Velocity;                                      //Velocidade do inimigo
    public float HuntingTime;                                   //O tempo que o inimigo continuará perseguindo o jogador caso ele o perca de vista
    public float DetectRadius;                                  //Raio de visão para o inimigo enquanto ele está parado
    public bool DetectedPlayer;                                 //Booleana que mostra se o inimigo viu o jogador
    public bool HuntingPlayer;                                  //Booleana que mostra se o inimigo está atacando o jogador
    public bool Patrol;                                         //Booleana que mostra se o inimigo está em modo de patrulha
    public bool Idle;                                           //Booleana que mostra se o inimigo está em modo de descanço
    private GameObject Player;                                  //O objeto do jogador
    private float HuntingStart;                                 //O tempo inicial em que o inimigo perdeu de vista o jogador
    private float PatrolChance;                                 //A probabilidade dos inimigos entrarem em modo de patrulha
    private float PatrolWill;                                   //A varaiavel que armazena qual a vontade do inimigo patrulhar no momento
    private float PatrolTimer;                                  //O tempo que o inimigo leva para tentar começar uma nova patrulha
    private float LastPatrol;                                   //O tempo que iniciou a ultima patrulha
    private bool MemoryController;                              //Booleana que controla se o inimigo lembra da posição do jogador 
    private Vector3 PatrolPosition;                             //A posição atual que será o destino de patrulha do inimigo
    private Vector3 TargetPosition;                             //A posição do alvo do inimigo, o ultimo valor de posição do jogador que o inimigo se lembra
    private Vector3 Delta;

    void Start()
    {
        HuntingPlayer = false;                                  //Flags de estado inicial
        DetectedPlayer = false;
        Idle = true;
        PatrolTimer = 7;                                        //Tempo que o inimigo leva até tentar patrulhar novamente 7 seg
        PatrolChance = 850;                                     //Chance de iniciar patrulha 85%
        LastPatrol = 0;
        Player = GameObject.FindWithTag("Player");              //Find the player with the tag "Player                          //O alvo é a posição do jogador
        Agent = gameObject.GetComponent<NavMeshAgent>();        //Get the agent of the Enemy
        Agent.speed = Velocity;
    }

    void Update()
    {
        if (Idle)                                               //Enquanto o inimigo estiver no modo idle
        {
            Collider[] ColliderList;
            ColliderList = Physics.OverlapSphere(gameObject.transform.position, DetectRadius); //Faz um cast esférico com raio DetectRadius
            Agent.avoidancePriority = Random.Range(40, 50);     //Enqunto parado o agente inimigo tem prioridade menor para os que estão em movimento
            foreach (Collider element in ColliderList)
            {
                if (element.gameObject.tag == "Player")
                {
                    DetectedPlayer = true;
                }
            }

            if (PatrolTimer + LastPatrol < Time.time)
            {
                LastPatrol = Time.time;
                PatrolWill = Random.Range(0, 1000);
                if (PatrolWill > PatrolChance)
                {
                    StartCoroutine(StartPatrol());
                }
            }
        }

        if (Patrol)                                                         //Rotina da patrulha
        {
            Agent.destination = PatrolPosition;
            Agent.avoidancePriority = 5;
        }

        if (DetectedPlayer)                                                 //Caso o jogador foi encontrado
        {
            HuntingPlayer = true;                                           //Começa a caçar o jogador
            HuntingStart = Time.time;                                       //Inica o contador para o fim da caçada
            Patrol = false;
            Idle = false;
            MemoryController = true;
            StartCoroutine(Memory());
            if(gameObject.GetComponent<EnemyAttack>().MeleeEnemy)
            {
                Target = Player.transform;
                TargetPosition = Player.transform.position;
            }
            
        }

        if (HuntingStart + HuntingTime < Time.time && !Patrol)              //Se passou "HuntingTime" que o inimigo não encontrou o jogador, o inimigo para de caçar o jogador
        {
            HuntingPlayer = false;                                          //Termina a caçada do jogador
            Idle = true;
        }

        if (HuntingPlayer)                                                  //Se está caçando o jogador corre na direção dele
        {

            Agent.speed = Velocity;
            if(gameObject.GetComponent<EnemyAttack>().MeleeEnemy)
            {
                if (MemoryController) Agent.destination = Target.position;
                else Agent.destination = TargetPosition;
            }
            
        }
    }

    Vector3 CartesianCoords(float Radius,float Angle)
     {
        Vector3 cartesian;
        cartesian = new Vector3(Mathf.Sin(Angle) * Radius,0f, Mathf.Cos(Angle) * Radius);
        return cartesian;
     }

public Vector3 GeometricPath(Vector3 Position)
    {
        Vector3 vector = new Vector3();
        float XCoord, ZCoord;                                               //Trajetória Quadrada/Retangular;
        XCoord = Mathf.Ceil(Random.Range(-1, 2));
        ZCoord = Mathf.Ceil(Random.Range(-1, 2));
        vector = new Vector3(Random.Range(1, 10) * XCoord, 0f, Random.Range(1, 10) * ZCoord);
        Position = Position + vector;
        return vector;
    }

    IEnumerator StartPatrol()
    {
        Idle = false;
        Patrol = true;
        Agent.speed = Velocity * 0.6f;
        yield return new WaitForSeconds(Random.Range(1, 3));
        int k = 0;

        while (k < 4 && Patrol)
        {
            yield return new WaitForSeconds(Random.Range(3, 3 + Random.value * 3));
            PatrolPosition = GeometricPath(gameObject.transform.position);
            k++;
        }

        Patrol = false;
        Idle = true;
        Agent.speed = Velocity;
    }

    IEnumerator Memory()
    {
        yield return new WaitForSeconds(HuntingTime);
        if (!DetectedPlayer && HuntingStart + HuntingTime < Time.time) MemoryController = false;
    }

    public void ReceivedDamage(float DamageTaken)                           //Function to damage the enemy
    {
        HP -= DamageTaken;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}