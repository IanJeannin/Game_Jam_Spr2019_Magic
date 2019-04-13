using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string gameScene;
    [SerializeField]
    private string creditsScene;
    [SerializeField]
    private string mainMenu;

    [SerializeField]
    private GameObject instructionsPanel, creditsPanel;

    //AUDIO
    [SerializeField]
    private float hoverVolume, clickVolume;
    [SerializeField]
    private AudioClip hover, click;

    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    public void loadGameScene()
    {
        audio.PlayOneShot(click, clickVolume);
        SceneManager.LoadScene(gameScene);
    }

    public void clickPlay()
    {
        audio.PlayOneShot(click, clickVolume);
        instructionsPanel.SetActive(true);
    }

    public void goToCredits()
    {
        audio.PlayOneShot(click, clickVolume);
        creditsPanel.SetActive(true);
    }

    public void backToMenu()
    {
        audio.PlayOneShot(click, clickVolume);
        creditsPanel.SetActive(false);
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
