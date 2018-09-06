using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
///  Controls the Music Button, switching music on and off.
/// </summary>
public class MusicButtonScript : MonoBehaviour
{

    private MusicPlayer musicManager;
    public Sprite OffSprite;
    public Sprite OnSprite;
    private Button btn;


    void Start()
    {
        btn = this.GetComponent<Button>(); //gets music button
        btn.onClick.AddListener(SwitchMusic);
        //gets the script that controls music from main camera
        musicManager = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        if (!musicManager.musicOn)
        {
            ChangeImage();
        }

    }

    //Switches music on and off
    void SwitchMusic()
    {
        musicManager.SwitchMusic();
        ChangeImage(); //change button image

    }

    //Changes button image from off to on and vice versa
    public void ChangeImage()
    {
        if (btn.image.sprite == OnSprite)
            btn.image.sprite = OffSprite;
        else
        {
            btn.image.sprite = OnSprite;
        }
    }
}
