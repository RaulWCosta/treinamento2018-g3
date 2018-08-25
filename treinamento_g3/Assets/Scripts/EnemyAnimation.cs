using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAnimation : MonoBehaviour {

    Animator animator;
    Transform sprite;
    public bool flipped = false;
    NavMeshAgent agent;
    float deathTime;
    private float deathDuration = 3;
    private float timeforAlpha = 1;
    bool dead = false;



    // Use this for initialization
    void Start () {
        sprite = this.gameObject.transform.Find("Sprite");
        animator = sprite.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();        //Get the agent of the Enemy
    }
	
	// Update is called once per frame
	void Update () {
        //turn dragon on and off
        //call flip sprite if necessary
        //if enemy is moving
        if (agent.velocity != Vector3.zero)
        {
            //turns on walking animation
            animator.SetBool("turnedOn", true);
            if(agent.velocity.x > 0)
            {
                //going right
                flipped = true;  
            }
            else
            {
                //going left
                flipped = false;
            }
            //flips sprite to appropriate direction
            FlipSprite();
        }
        else
        {
            //turns off walking animation
            animator.SetBool("turnedOn", false);
        }

        //wait after enemy is dead for destroying it
        if (dead)
        {
            deathTime -= Time.deltaTime;

            //makes monster fade away with time
            Color tmp = sprite.GetComponent<SpriteRenderer>().color;
            if(deathTime - timeforAlpha < deathDuration - timeforAlpha)
            {
                tmp.a = deathTime / timeforAlpha;
            }
               
            sprite.GetComponent<SpriteRenderer>().color = tmp;
        }
            
        //time ran out, destroy enemy
        if (deathTime <= 0.0f && dead)
        {
            Destroy(gameObject);
        }

    }

    //Flips sprite from left and right
    void FlipSprite()
    {
        Vector3 scale = sprite.localScale;

        if (flipped)
        {
            scale.x = -1 * Mathf.Abs(scale.x);    
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        sprite.localScale = scale;
    }

    //Runs damage animation
    public void DamageAnimation()
    {
        animator.SetTrigger("attacked");
    }

    //Runs death animation
    public void DeathAnimation()
    {
        animator.SetBool("dead", true);
        deathTime = deathDuration;
        dead = true;
    }
}
