using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class HpControl : MonoBehaviour {

    private PlayerController player;                                                                //An reference to the player
    private Image icon;                                                                             //An reference to this object's image component
    private float lastHp = 100;                                                                     //An variable to prevent the image to be changed too fast
    private Sprite[] quantities;                                                                    //An reference array to all images
    public Texture2D texture;                                                                       //An reference to the multiple sprite parent (The multiple sprite parent is "HP")

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();                 //Finds the player
        icon = this.GetComponent<Image>();                                                          //Find the image
        quantities = Resources.LoadAll<Sprite>(texture.name);                                       //Loads all multiple sprites under texture **WARNING** texture MUST be under "Resources" folder
    }
	
	// Update is called once per frame
	void Update () {
        if (player.hpCurrent - lastHp >= 2 || player.hpCurrent - lastHp <= -2)                      //If there was an significant change in the player's life, change the sprite
        {
            icon.sprite = quantities[50 - (int) player.hpCurrent / 2];
            lastHp = player.hpCurrent;
        }
	}
}
