using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains functions for loading scenes for use in buttons
/// </summary>
public class MenuManager: MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("dungeonTest");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("InstructionMenu");
    }

    public void LoadPlayerWin()
    {
        SceneManager.LoadScene("PlayerWin");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
