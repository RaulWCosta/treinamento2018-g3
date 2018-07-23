using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * Highlights npc when it is selected or mousedOver, showing its name
 * if appropriate
 **/
public class SelectNPC : MonoBehaviour {

    public string characterName;
    public TextMesh namePanel;
    private SpriteRenderer sprite;
    private Color highlightColor = new Color(0.7f, 0.7f, 0.7f);

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        namePanel = GetComponentInChildren<TextMesh>();
        if (namePanel)
        {
            namePanel.text = characterName;
            namePanel.color = new Color(1f, 1f, 1f, 0f);
        }
	}
	
    //changes color of sprite
    public void HighlightOn()
    {
        sprite.color = highlightColor;
        if (namePanel)
        {
            namePanel.color = new Color(1, 1, 1, 1);
        }
    }

    //changes back the color of sprite to normal
    public void HighlightOff()
    {
        sprite.color = new Color(1f, 1f, 1f);
        if (namePanel)
        {
            namePanel.color = new Color(1, 1, 1, 0);
        }
    }

    //activates highlight on mouseover
    private void OnMouseOver()
    {
        HighlightOn();
    }

    //disactivates highlight on mouseexit
    private void OnMouseExit()
    {
        HighlightOff();
    }
}
