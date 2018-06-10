using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour {

    Animator playerAnimator; //player animator

	// Use this for initialization
	void Start () {
		playerAnimator = transform.parent.gameObject.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        FollowMouse();
	}

    //Make the weapon face the mouse pointer
    void FollowMouse()
    {

        //gets relative distance from weapon to camera
        float z = transform.position.z - Camera.main.transform.position.z;
        //mouse pointer position
        Vector3 p = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);

        //transforms mouse position coordinates to world coordinates
        p = Camera.main.ScreenToWorldPoint(p);
        //gets relative distance from mouse to weapon
        p = p - transform.position;

        //player is facing right
        if (playerAnimator.GetBool("faceRight"))
        {
            float newXScale;
            if (transform.localScale.x > 0) {
                newXScale = transform.localScale.x * -1;
                transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
            }
             transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.x, Vector3.SignedAngle(Vector3.right, p, Vector3.forward));
        }
        else //player is facing left
        {
        
            float newXScale;
            if (transform.localScale.x < 0)
            {
                newXScale = transform.localScale.x * -1;
                transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);

            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.x, Vector3.SignedAngle(Vector3.left, p, Vector3.forward));

        }
    }
}
