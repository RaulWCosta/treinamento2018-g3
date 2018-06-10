using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform Alvo;
    public float HP;
    public float Velocidade;
    public float Dano;
    public bool CombateProximo;
    
	
	void Start () {
       // NavMeshAgent Agente = gameObject.GetComponent<NavMeshAgent>();
       // Agente.destination = Alvo.position;
	}
	
	
    public void LevouDano(float DanoRecebido)
    {
        HP -= DanoRecebido;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

	void Update () {
		
	}
}
