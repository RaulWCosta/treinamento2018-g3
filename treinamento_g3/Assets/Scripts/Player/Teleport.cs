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
    private float realTeleportRange;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        teleportRange = 3f;
        timeBetweenDodges = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        //Quando apertar o shift esquerdo, esquiva
        if (Input.GetKeyDown(KeyCode.LeftShift) && teleporting == false && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && (realTeleportRange = RayCast()) > 0)
        {
            teleporting = true;                                                                                                           //Esa varíávle impede que o teleporte possa ser feito varias vezes
            invencible = true;                                                                                                            //Fica invencível
            animator.SetTrigger("teleport");
            Invoke("NotInvencible", timeBetweenDodges / 2);                                                                               //Tempo pra deixar de ficar invencível
            Invoke("AllowTeleport", timeBetweenDodges);                                                                                   //Tempo pra permitir o teletransporte novamente
        }
    }

    void Dodge()
    {
        Debug.Log(realTeleportRange);
        teleport = Vector3.right * Input.GetAxis("Horizontal") * realTeleportRange;
        transform.position += teleport;
        teleport = Vector3.forward * Input.GetAxis("Vertical") * realTeleportRange;
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

    float RayCast()
    {
        RaycastHit ray;
        Vector3 direction = gameObject.GetComponent<CharacterController>().velocity;
        Physics.Raycast(this.transform.position, direction, out ray, teleportRange, 2);
        return ray.distance;
    }
}
