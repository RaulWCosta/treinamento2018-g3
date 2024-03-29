﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the player with input and make it face the mouse pointer
public class PlayerMovement : MonoBehaviour {

    public float speed; //movement speed
    public Interactable selected = null;
    private CharacterController Controller;
    Animator animator;
    Vector3 mouseToPlayer;

    // Use this for initialization
    void Start() {
        animator = this.GetComponent<Animator>();
        Controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        //moves player on screen
        Vector3 oldPos = transform.position;
        Controller.Move(Vector3.forward * Input.GetAxis("Vertical") * speed);
        Controller.Move(Vector3.right * Input.GetAxis("Horizontal") * speed);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z);

        //if player moved, turn on walk animation
        if (oldPos != transform.position)
        {
            animator.SetBool("walk", true);
            selected = null; //turns off selected item if player decides to move somewhere else
        }
        else //turn off walk animation
        {
            animator.SetBool("walk", false);
        }

        //make player face the mouse pointer
        FollowMouse();

        //if left click on an interactable item, go to it and then take it
        if (Input.GetMouseButton(1))
        {
            TakeObject();
        }

        //if there is an selected object, follow it
        if (selected != null)
        {
            float delta = 1.5f;
            float distanceToItem = (this.transform.position - selected.transform.position).sqrMagnitude;
            if (oldPos != transform.position)
            {
                StopFollowingObject();
            }
            //checks to see if the player passes the radius to interact with the item (not sure if the +-0.1 is necessary)
            if (distanceToItem > (delta - 0.1) * (delta - 0.1) && distanceToItem < (delta + 0.1) * (delta + 0.1))
            {
                //if yes, stop following
                StopFollowingObject();
                animator.SetBool("walk", false);
            }
            //if he still far, follows the item
            else if (distanceToItem > delta * delta)
            {
                //else follows the player
                this.transform.position = Vector3.MoveTowards(this.transform.position, selected.transform.position, speed);
                animator.SetBool("walk", true);
            }
        }

    }

    //Make the player face the mouse pointer (left or right)
    void FollowMouse() {

        //gets relative distance from player to camera
        float y = transform.position.y - Camera.main.transform.position.y;
        //mouse pointer position

        Vector3 p = new Vector3(Input.mousePosition.x, y, Input.mousePosition.z); 


        //transforms mouse position coordinates to world coordinates
        p = Camera.main.ScreenToWorldPoint(p);
        //gets relative distance from mouse to player position
        p = p - transform.position;

        //mouse to the right of player
        if (p.x > 0)
        {
            animator.SetBool("faceRight", true);
        }
        else //mouse to the left of player
        {
            animator.SetBool("faceRight", false);
        }
    }

    //Take the selected object
    void TakeObject()
    {
        //A ray that goes from the camera to the mouse position
        Ray origin = Camera.main.ScreenPointToRay(Input.mousePosition);
        //a reference to a object that may be hit by the ray
        RaycastHit hit;

        //If the ray hits
        if (Physics.Raycast(origin, out hit))
        {
            //creates and checks if the hit object is Interactable (has a Interactable Component)
            Interactable objectHitted = hit.collider.GetComponent<Interactable>();
            if (objectHitted != null)
            {
                //if it does, select the object
                selected = objectHitted;
                //Debug.Log("Ray casted and object selected");
            }
        }
    }

    //stops following the player
    void StopFollowingObject()
    {
        selected = null;
        //Debug.Log("Stopped Following");
    }
}
