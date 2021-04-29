using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource main;
    private AudioSource menu;

    public AudioClip pistolClip;
    public AudioClip shotgunClip;
    public AudioClip menuClip;
    public AudioClip musicClip;
    public AudioClip meleeClip;
    public AudioClip enemyDeathClip;
    public AudioClip playerDeathClip;

    public bool pistolPlay = false;
    public bool shotgunPlay = false;
    public bool inMenu = false;
    public bool inGame = false;
    public bool meleeHit = false;
    public bool enemyKill = false;
    public bool playerKill = false;

    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        main.pitch = Time.timeScale;

        while (inMenu)
        {
            menu.Play();
        }

        while (inGame)
        {
            main.Play();
        }

        if (pistolPlay)
        {
            main.PlayOneShot(pistolClip, 0.5f);//Play pistol sound after firing
            pistolPlay = false;
        }

        if (shotgunPlay)
        {
            main.PlayOneShot(shotgunClip, 0.5f); //Play shotgun sound after firing
            shotgunPlay = false;
        }

        if (meleeHit)
        {
            main.PlayOneShot(meleeClip, 0.5f); //Play shotgun sound after firing
            meleeHit = false;
        }

        if (enemyKill)
        {
            main.PlayOneShot(enemyDeathClip, 0.5f); //Play shotgun sound after firing
            enemyKill = false;
        }

        if (playerKill)
        {
            main.PlayOneShot(playerDeathClip, 0.5f); //Play shotgun sound after firing
            playerKill = false;
        }
    }
}