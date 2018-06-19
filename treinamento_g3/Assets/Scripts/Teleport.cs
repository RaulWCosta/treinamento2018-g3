using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public bool invencible;
    private float teleportRange;
    private float timeBetweenDodges;
    private Vector3 teleport;
    private bool teleporting;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        teleportRange = 3f;
        timeBetweenDodges = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        //Quando apertar o shift esquerdo, esquiva
        if (Input.GetKeyDown(KeyCode.LeftShift) && teleporting == false && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            Debug.Log("Teste");
            teleporting = true;                                                                                                           //Esa varíávle impede que o teleporte possa ser feito varias vezes
            invencible = true;                                                                                                            //Fica invencível
            animator.SetTrigger("teleport");
            Invoke("NotInvencible", timeBetweenDodges / 2);                                                                               //Tempo pra deixar de ficar invencível
            Invoke("AllowTeleport", timeBetweenDodges);                                                                                   //Tempo pra permitir o teletransporte novamente
        }
    }

    void Dodge()
    {
        teleport = Vector3.right * Input.GetAxis("Horizontal") * teleportRange;
        transform.position += teleport;
        teleport = Vector3.up * Input.GetAxis("Vertical") * teleportRange;
        transform.position += teleport;
        animator.SetBool("walk", false);
    }

    void AllowTeleport()
    {
        teleporting = false;
    }
    void NotInvencible()
    {
        invencible = false;
    }
}
