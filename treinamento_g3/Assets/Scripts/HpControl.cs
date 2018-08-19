using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class HpControl : MonoBehaviour {

    private PlayerController player;
    private Image icon;
    private float lastHp = 100;
    private Sprite[] quantities;
    public Texture2D texture;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        icon = this.GetComponent<Image>();
        quantities = Resources.LoadAll<Sprite>(texture.name);
    }
	
	// Update is called once per frame
	void Update () {
        if (player.hpCurrent - lastHp >= 2 || player.hpCurrent - lastHp <= -2)
        {
            icon.sprite = quantities[50 - (int) player.hpCurrent / 2];
            lastHp = player.hpCurrent;
        }
	}
}
