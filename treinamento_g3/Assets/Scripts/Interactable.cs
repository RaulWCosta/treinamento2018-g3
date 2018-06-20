using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    //radius for taking interacting with an object
    public float radius = 2f;
    //If there is an selected item, this varible will receive true;
    private bool itemSelected = false;
    private GameObject player;

    private void Start()
    {
        //Finds the player
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        float dist = GetClosestItem(player.transform);

        //If there is an selected item, itemSelected receives true;
        if (player.GetComponent<PlayerMovement>().selected != null)
        {
            itemSelected = true;
        }
        else if (dist <= radius * radius)
        {
            Debug.Log("Item selected through interactable");
            player.GetComponent<PlayerMovement>().selected = this.GetComponent<Interactable>();
            itemSelected = true;
        }
        else
        {
            //else it receives false;
            //Debug.Log("Deselected");
            itemSelected = false;
            player.GetComponent<PlayerMovement>().selected = null;
        }

        //if there is an selected object,
        if (itemSelected == true && dist <= radius * radius)
        {
            if (Input.GetKeyDown("e"))
            {
                Interact();
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void Interact ()
    {
        Debug.Log("Interacting with " + this.name);
    }

    //this function return the distance between the player and the object of this component
    float GetClosestItem(Transform playerTransform)
    {
        float dist;
        //player location
        Vector3 playerLocation = playerTransform.position;
        //this object's location
        Vector3 itemLocation = this.transform.position;

        //the distance between the player and the object **IN A SQUARE ROOT**
        dist = (playerLocation - itemLocation).sqrMagnitude;

        return dist;
    }

    //this function draws an sphere of radius "radius" when an intereractable object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
