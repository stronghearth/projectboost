using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioClip GemTrekLevel1;
    [SerializeField] AudioClip GemTrekLevel2;
    [SerializeField] AudioClip GemTrekLevel3;
    [SerializeField] AudioClip GemTrekLevel4;
    [SerializeField] AudioClip GemTrekLevel5;

    public AudioSource Music;
    

    //declare level scene names 
    private readonly string Level1 = "Level1";
    private readonly string Level2 = "Level2";
    private readonly string Level3 = "Level3";
    private readonly string Level4 = "Level4";
    private readonly string Level5 = "Level5";


    // Start is called before the first frame update
    void Start()
    {
        Music = GetComponent<AudioSource>();
        PlayMusic();
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void PlayMusic()
    {
        if (Level1 == SceneManager.GetActiveScene().name)
        {
            Music.volume = 0.2f;
            Music.clip = GemTrekLevel1;
            Music.Play();
            Music.loop = true;
        }

        else if (Level2 == SceneManager.GetActiveScene().name)
        {
            Music.volume = 0.2f;
            Music.clip = GemTrekLevel2;
            Music.Play();
            Music.loop = true;
        }

        else if (Level3 == SceneManager.GetActiveScene().name)
        {
            Music.volume = 0.2f;
            Music.clip = GemTrekLevel3;
            Music.Play();
            Music.loop = true;
        }

        else if (Level4 == SceneManager.GetActiveScene().name)
        {
            Music.volume = 0.2f;
            Music.clip = GemTrekLevel4;
            Music.Play();
            Music.loop = true;
        }

        else if (Level5 == SceneManager.GetActiveScene().name)
        {
            Music.volume = 0.2f;
            Music.clip = GemTrekLevel5;
            Music.Play();
            Music.loop = true;
        }
    }

    public void StopMusic()
        {
            Music.loop = false;
            Music.Stop();
        }
}