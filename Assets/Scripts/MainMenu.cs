using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string gameScene;
    [SerializeField]
    private string creditsScene;
    [SerializeField]
    private string mainMenu;

    public void loadGameScene()
    {
        //AUDIO button click sound
        SceneManager.LoadScene(gameScene);
    }

    public void goToCredits()
    {
        //AUDIO button click sound
        SceneManager.LoadScene(creditsScene);
    }

    public void backToMenu()
    {
        //AUDIO button click sound
        SceneManager.LoadScene(mainMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
