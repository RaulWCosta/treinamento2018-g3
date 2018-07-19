using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Starts a dialogue when item is interacted with
 * Should be on NPC.
 * */
public class DialogueTrigger : Interactable {

    public Dialogue dialogue;

    private new void Update()
    {
        base.Update();
        //if item isn't selected anymore
        if (!itemSelected)
        {
            //end dialogue
            FindObjectOfType<DialogueManager>().EndDialogue(dialogue.name);
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
