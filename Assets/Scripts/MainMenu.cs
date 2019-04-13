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

    //AUDIO
    [SerializeField]
    private float hoverVolume, clickVolume;
    [SerializeField]
    private AudioClip hover, click;

    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void loadGameScene()
    {
        //AUDIO button click sound
        audio.PlayOneShot(click, clickVolume);
        SceneManager.LoadScene(gameScene);
    }

    public void goToCredits()
    {
        //AUDIO button click sound
        audio.PlayOneShot(click, clickVolume);
        SceneManager.LoadScene(creditsScene);
    }

    public void backToMenu()
    {
        //AUDIO button click sound
        audio.PlayOneShot(click, clickVolume);
        SceneManager.LoadScene(mainMenu);
    }

    public void ExitGame()
    {
        audio.PlayOneShot(click, clickVolume);
        Application.Quit();
    }

    public void PlayHoverSound()
    {
        audio.PlayOneShot(hover, hoverVolume);
    }
}
