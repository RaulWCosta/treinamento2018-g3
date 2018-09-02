using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BossHPControl : MonoBehaviour
{
    private BossController bossController;
    private Image icon;                                                                             //An reference to this object's image component
    private float lastHp = 30;                                                                      //An variable to prevent the image to be changed too fast
    private float twoPercent;
    private Sprite[] quantities;                                                                    //An reference array to all images
    public Texture2D texture;                                                                       //An reference to the multiple sprite parent (The multiple sprite parent is "HP")

    // Use this for initialization
    void Start()
    {
        bossController = GameObject.Find("Boss").GetComponent<BossController>();
        icon = this.GetComponent<Image>();                                                          //Find the image
        quantities = Resources.LoadAll<Sprite>(texture.name);                                       //Loads all multiple sprites under texture **WARNING** texture MUST be under "Resources" folder
        twoPercent = bossController.MaxHp * 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (icon.enabled == false && bossController.PlayerInZone == true)
            icon.enabled = true;

        if (bossController.HP - lastHp >= twoPercent || bossController.HP - lastHp <= -twoPercent)                      //If there was an significant change in the player's life, change the sprite
        {
            if (bossController.HP <= 0)
                icon.sprite = quantities[49];
            else
                icon.sprite = quantities[50 - (int)((100 * bossController.HP) / bossController.MaxHp) / 2];
            lastHp = bossController.HP;
        }
    }
}
