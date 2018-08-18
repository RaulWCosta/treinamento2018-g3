using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScriot : MonoBehaviour {

    private CharacterController Controller;
    public float Velocity;

	void Start () {
        Controller = gameObject.GetComponent<CharacterController>();
    }
	
	void Update () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
        Controller.Move(new Vector3(0, 0, Velocity * Input.GetAxis("Vertical")));
        Controller.Move(new Vector3(Velocity * Input.GetAxis("Horizontal"), 0,0));
    }
}