using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    private Animator animator;
    private Vector3 direction;
    public bool invencible;
    public float range;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        invencible = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Quando apertar o shift esquerdo, esquiva
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * range;
            invencible = true;
            Debug.Log("invencible = " + invencible);
            animator.SetTrigger("teleport");
        }
    }

    void Dodge()
    {
        this.transform.Translate(direction, Space.World);
        invencible = false;
        Debug.Log("invencible = " + invencible);
    }

    float RayCast()
    {
        return 0f;
    }
}
