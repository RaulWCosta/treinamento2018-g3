using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls dialogue.
 * Only one per scene
 * */
public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;
    private string name;
    private bool inDialogue;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        inDialogue = false;
	}

    //Starts or continues dialogue
    public void StartDialogue (Dialogue dialogue)
    {
        //If the current dialogue isn't the one given
        //changes dialogue
        if (name != dialogue.name)
        {
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            name = dialogue.name;

        }

        //continues dialogue
        DisplayNextSentence();
        
    }

    //displays next sentence in dialogue
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue(this.name);
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }


    public void EndDialogue(string name)
    {
        if (this.name == name)
        {
            sentences.Clear();
            this.name = null;

            //close dialogue box
            Debug.Log("End of conversation");
        }
    }
}
