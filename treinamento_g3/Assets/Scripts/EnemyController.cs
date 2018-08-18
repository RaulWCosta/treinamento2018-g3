using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject Vision;
    public NavMeshAgent Agent;                                 //O agente do inimigo, entidade que controla a movimentação do inimigo no navmesh
    public Transform Target;                                    //O "Alvo" a posição atual do jogador
    public Item[] drops;                                        //Items dropáveis
    public float HP;                                            //Vida do inimigo
    public float Velocity;                                      //Velocidade do inimigo
    public float HuntingTime;                                   //O tempo que o inimigo continuará perseguindo o jogador caso ele o perca de vista
    public float DetectRadius;
    public bool DetectedPlayer;                                 //Booleana que mostra se o inimigo viu o jogador
    public bool HuntingPlayer;                                  //Booleana que mostra se o inimigo está atacando o jogador
    public bool Patrol;                                          //Booleana que mostra se o inimigo está em modo de patrulha
    public bool Idle;                                           //Booleana que mostra se o inimigo está em modo de descanço
    private GameObject Player;
    private float HuntingStart;                                 //O tempo inicial em que o inimigo perdeu de vista o jogador
    private float PatrolChance;
    private float PatrolWill;
    private float PatrolTimer;
    private float LastPatrol;
    private bool MemoryController;
    private Vector3 PatrolPosition;
    private Vector3 TargetPosition;
    private NavMeshPath cam;

    void Start()
    {
        HuntingPlayer = false;                               
        DetectedPlayer = false;
        Idle = true;
        PatrolTimer = 7;
        PatrolChance = 850;
        LastPatrol = 0;
        Player = GameObject.FindWithTag("Player");              //Find the player with the tag "Player                          //O alvo é a posição do jogador
        Agent = gameObject.GetComponent<NavMeshAgent>();        //Get the agent of the Enemy
        Agent.speed = Velocity;
    }

     void Update()
    {
        if (Idle)
        {
            Collider [] ColliderList;
            ColliderList =  Physics.OverlapSphere(gameObject.transform.position, DetectRadius);
            Agent.avoidancePriority = Random.Range(40, 50);
            foreach(Collider element in ColliderList)
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
        
         if(Patrol)                                              //Rotina da patrulha
        {
            Agent.destination = PatrolPosition;
            Agent.avoidancePriority = 5;
        }

        if(DetectedPlayer)                                      //Caso o jogador foi encontrado
        {
            HuntingPlayer = true;                               //Começa a caçar o jogador
            HuntingStart = Time.time;                           //Inica o contador para o fim da caçada
            Patrol = false;
            Idle = false;
            MemoryController = true;
            StartCoroutine(Memory());
            Target = Player.transform;
            TargetPosition = Player.transform.position;
        }

        if(HuntingStart + HuntingTime < Time.time && !Patrol)              //Se passou "HuntingTime" que o inimigo não encontrou o jogador, o inimigo para de caçar o jogador
        {
            HuntingPlayer = false;                              //Termina a caçada do jogador
            Idle = true;
        }

        if(HuntingPlayer)                                       //Se está caçando o jogador corre na direção dele
        {
            Agent.speed = Velocity;
            if (MemoryController) Agent.destination = Target.position;
            else Agent.destination = TargetPosition;
        }
    }

    public Vector3 GeometricPath(Vector3 Position)
    {
        Vector3  vector = new Vector3();
        float XCoord, ZCoord;                       //Trajetória Quadrada/Retangular;
        XCoord = Mathf.Ceil(Random.Range(-1, 2));
        ZCoord = Mathf.Ceil(Random.Range(-1, 2));
        vector = new Vector3(Random.Range(1,10) * XCoord,0f, Random.Range(1, 10) * ZCoord);
        Position = Position + vector;
        return vector;
    }

    IEnumerator StartPatrol()
    {
        Idle = false;
        Patrol = true;
        Agent.speed = Velocity * 0.6f;
        yield return new WaitForSeconds(Random.Range(1,3));
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
        if(!DetectedPlayer) MemoryController = false;
    }

    public void ReceivedDamage(float DamageTaken)                           //Function to damage the enemy
    {
        HP -= DamageTaken;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Drop()
    {
        if (Random.Range(1, 2) > 1)
            Instantiate(drops[Random.Range(0, drops.Length)], this.transform.position, this.transform.rotation);
    }
}