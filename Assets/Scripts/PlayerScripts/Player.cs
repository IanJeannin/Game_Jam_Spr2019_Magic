using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxCharge;
    [SerializeField]
    private ResourceBars resourceBar;
    [SerializeField]
    private GameObject finalUnlockedMarkers;
    [SerializeField]
    private float fadeOutTime;
    [SerializeField]
    private AudioClip defeatSound;
    [SerializeField]
    private float defeatVolume;
    [SerializeField]
    private AudioClip playerHurtSound;

    private AudioSource audio;
    private SpriteRenderer renderer;

    private float currentHealth;
    private static float currentCharge;
    private float unlockedMarkersAlpha;
    //Prevents final attack markers from going off repeatedly
    private bool markerHasNotGoneOff=true;
    private bool playerDeathStarted = false;
    

    private void Start()
    {
        currentHealth = maxHealth;
        currentCharge = 0;
        resourceBar.SetEnergy(currentCharge);
        finalUnlockedMarkers.SetActive(false);
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damageAmount)
    {
        audio.PlayOneShot(playerHurtSound);
        currentHealth -= damageAmount;
        resourceBar.SetHealth(currentHealth / 100);
    }

    public void AddEnergy(float chargeAdded)
    {
        currentCharge += chargeAdded;
        //Prevents player going over max charge
        if (currentCharge>=100)
        {
            //AUDIO playe final attack ready sound cue
            currentCharge = 100;
            gameObject.GetComponent<PlayerAttack>().UnlockFinalAttack();
            if(markerHasNotGoneOff)
            {
                finalUnlockedMarkers.SetActive(true);
                markerHasNotGoneOff = false;
                StartCoroutine(GetRidOfFinalMarkers());
            }
        }
        resourceBar.SetEnergy(currentCharge/100);
    }


    private void Update()
    {
        if(currentHealth<=0)
        {
            if (!playerDeathStarted)
                StartCoroutine(defeatCoroutine());
        }
    }

    private IEnumerator GetRidOfFinalMarkers()
    {
        yield return new WaitForSeconds(fadeOutTime);
        finalUnlockedMarkers.SetActive(false);
    }

    private IEnumerator defeatCoroutine()
    {
        playerDeathStarted = true;
        audio.PlayOneShot(defeatSound, defeatVolume);

        yield return new WaitForSeconds(.5f);//wait for the arrow to hit it

        for (float f = 1f; f > 0; f -= .1f)
        {
            renderer.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(.1f);
        }

        SceneManager.LoadScene("DefeatScene");
    }
}
