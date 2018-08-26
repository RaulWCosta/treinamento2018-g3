using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnding : MonoBehaviour {

    GameObject boss;
    Animator doorAnimator;
    Collider doorCollider;
    MenuManager menumanager;


	// Use this for initialization
	void Start () {
        //get boss
        boss = GameObject.Find("Boss");
        doorAnimator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
		//check if boss is dead
        if(boss == null)
        {
            doorAnimator.SetBool("open", true);
            doorCollider.isTrigger = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //close elevator door and wait for animation
        }
    }
}
