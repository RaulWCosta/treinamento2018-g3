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
    public Dialogue.DialogueType type;
    public Text nameText;
    public GameObject dialogueText;
    public Animator dialogueBox;
    public GameObject shopOptions;

    private bool shopDialogueOpen = false;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
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
            type = dialogue.type;
            nameText.text = dialogue.name;

            //doesn't show shop dialogue if it's another kind of dialogue
            //or if shop dialogue hasn't reached the end
            if (type != Dialogue.DialogueType.ShopDialogue || sentences.Count>0)
            {
                shopOptions.SetActive(false);
                dialogueText.SetActive(true);
            }
                

        }

        //continues dialogue
        DisplayNextSentence();
        
    }

    //displays next sentence in dialogue
    public void DisplayNextSentence()
    {
        //dialogue over
        if (sentences.Count == 0)
        {
            //if it's the last sentence of a shop dialogue
            //and shop dialogue still isn't open
            if (type == Dialogue.DialogueType.ShopDialogue && !shopDialogueOpen)
            {
                //show shop dialogue
                shopOptions.SetActive(true);
                dialogueText.SetActive(false);
                shopDialogueOpen = true;
                return;

            }
            else
            {
                EndDialogue(this.dialogueName);
                shopDialogueOpen = false;
                return;
            }
           
        }

        string sentence = sentences.Dequeue();
        dialogueText.GetComponent<Text>().text = sentence;
        
    }

    //ends all dialogue
    public void EndDialogue()
    {
        //remove sentences and set name to null
        sentences.Clear();
        this.dialogueName = null;

        //close dialogue box
        dialogueBox.SetBool("DialogueOpen", false);
        shopOptions.SetActive(false);
    }

    //ends specific dialogue (used to avoid closing the wrong dialogue
    //when starting a new conversation
    public void EndDialogue(string dialogueName)
    {
        if (this.dialogueName == dialogueName)
        {
            //remove sentences and set name to null
            sentences.Clear();
            this.dialogueName = null;

            //close dialogue box
            dialogueBox.SetBool("DialogueOpen", false);
            shopOptions.SetActive(false);
        }
    }
}
