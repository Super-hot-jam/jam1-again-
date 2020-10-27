using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource pistol;
    private AudioSource shotgun;
    private AudioSource menu;
    private AudioSource music;
    private AudioSource melee;
    private AudioSource enemyDeath;
    private AudioSource playerDeath;

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
        pistol = GetComponent<AudioSource>();
        shotgun = GetComponent<AudioSource>();
        menu = GetComponent<AudioSource>();
        music = GetComponent<AudioSource>();
        melee = GetComponent<AudioSource>();
        enemyDeath = GetComponent<AudioSource>();
        playerDeath = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        pistol.pitch = Time.timeScale;
        shotgun.pitch = Time.timeScale;
        music.pitch = Time.timeScale;
        melee.pitch = Time.timeScale;
        enemyDeath.pitch = Time.timeScale;

        while (inMenu)
        {
            menu.Play();
        }

        while (inGame)
        {
            music.Play();
        }

        if (pistolPlay)
        {
            pistol.PlayOneShot(pistolClip, 0.5f);//Play pistol sound after firing
            pistolPlay = false;
        }

        if (shotgunPlay)
        {
            shotgun.PlayOneShot(shotgunClip, 0.5f); //Play shotgun sound after firing
            shotgunPlay = false;
        }

        if (meleeHit)
        {
            melee.PlayOneShot(meleeClip, 0.5f); //Play shotgun sound after firing
            meleeHit = false;
        }

        if (enemyKill)
        {
            enemyDeath.PlayOneShot(enemyDeathClip, 0.5f); //Play shotgun sound after firing
            enemyKill = false;
        }

        if (playerKill)
        {
            playerDeath.PlayOneShot(playerDeathClip, 0.5f); //Play shotgun sound after firing
            playerKill = false;
        }
    }
}