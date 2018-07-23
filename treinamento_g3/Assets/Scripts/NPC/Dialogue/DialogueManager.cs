using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Controls dialogue.
 * Only one per scene
 * */
public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;
    private string dialogueName;
    private bool inDialogue;
    public Text nameText;
    public Text dialogueText;
    public Animator dialogueBox;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        inDialogue = false;
	}

    //Starts or continues dialogue
    public void StartDialogue (Dialogue dialogue)
    {
        dialogueBox.SetBool("DialogueOpen", true);
        //If the current dialogue isn't the one given
        //changes dialogue
        if (dialogueName != dialogue.name)
        {
            
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            dialogueName = dialogue.name;
            nameText.text = dialogue.name;

        }

        //continues dialogue
        DisplayNextSentence();
        
    }

    //displays next sentence in dialogue
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue(this.dialogueName);
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        
    }


    public void EndDialogue(string dialogueName)
    {
        if (this.dialogueName == dialogueName)
        {
            //remove sentences and set name to null
            sentences.Clear();
            this.dialogueName = null;

            //close dialogue box
            dialogueBox.SetBool("DialogueOpen", false);
        }
    }
}
