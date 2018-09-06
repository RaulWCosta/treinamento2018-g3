using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the music playing in the game
/// Persists between scenes so that the music doesn't get interrupted
/// </summary>
public class MusicPlayer : MonoBehaviour
{

    public static MusicPlayer instance = null;
    private AudioSource musicPlayer;
    public bool musicOn = true;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicPlayer = this.GetComponent<AudioSource>();

    }

    //Switches music on and off
    public void SwitchMusic()
    {
        if (musicOn)
        {
            musicPlayer.Pause();
            musicOn = false;
        }
        else
        {
            musicPlayer.UnPause();
            musicOn = true;
        }

    }


}
