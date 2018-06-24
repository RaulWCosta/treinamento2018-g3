using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the player with input and make it face the mouse pointer
public class PlayerMovement : MonoBehaviour {

    public float speed; //movement speed

    Animator animator;
    Vector3 mouseToPlayer;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        //moves player on screen
        Vector3 oldPos = transform.position;
        transform.position += Vector3.forward * Input.GetAxis("Vertical") * speed;
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed;

        //if player moved, turn on walk animation
        if (oldPos != transform.position)
        {
            animator.SetBool("walk", true);
        }
        else //turn off walk animation
        {
            animator.SetBool("walk", false);
        }

        //make player face the mouse pointer
        FollowMouse();
      
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
            animator.SetBool("faceRight",true);
        }
        else //mouse to the left of player
        {
            animator.SetBool("faceRight", false);
        }
    }
}
