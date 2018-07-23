using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Starts a dialogue when item is interacted with
 * Should be on NPC.
 * */
public class DialogueTrigger : Interactable {

    public Dialogue dialogue;
    public SelectNPC highlight; //used to highlight npc if script is present
    private bool changed = true;

    private new void Start()
    {
        base.Start();
        highlight = GetComponent<SelectNPC>();
    }

    private new void Update()
    {
        base.Update();
        //if item isn't selected anymore
        if (!itemSelected)
        {
            //has script and the item had been selected before (changed = true)
            if (highlight && changed)
            {
                highlight.HighlightOff();
                changed = false; //turns changed off so that it doesn't turn off highlighting from mouse
            }
                
            //end dialogue
            FindObjectOfType<DialogueManager>().EndDialogue(dialogue.name);
        }
        else
        {
            //turns on highlight if object is selected
            if (highlight)
            {
                highlight.HighlightOn();
                changed = true;
            }
                
        }
    }

    //start or advance dialogue
    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public override void Interact()
    {
        base.Interact();
        TriggerDialogue();
    }
}
