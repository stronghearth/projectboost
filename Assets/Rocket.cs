using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    
    //switches for the designer to manipulate
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 20f;
    [SerializeField] private static float levelLoadDelay = 2.0f;

    [SerializeField] AudioClip GemTrekRocketEngine;
    [SerializeField] AudioClip GemTrekExpl;
    [SerializeField] AudioClip GemTrekVictoryD;
    [SerializeField] AudioClip GemTrekVictoryE;
    [SerializeField] AudioClip GemTrekVictoryF;
    [SerializeField] AudioClip GemTrekVictoryG;
    [SerializeField] AudioClip GemTrekVictoryA;

    [SerializeField] ParticleSystem exhaustParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;

    // declare Rigid Body instance and audio source instance
    Rigidbody rigidBody;
    public AudioSource audioSource;
    public Audio a;
    

    enum State { Alive, Dying, LevelUp }
    State state = State.Alive;

    bool collisionsDisabled = false;
   

    //declare level scene names 
    private readonly string Level1 = "Level1";
    private readonly string Level2 = "Level2";
    private readonly string Level3 = "Level3";
    private readonly string Level4 = "Level4";
    private readonly string Level5 = "Level5";

    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != 0) { 
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (state == State.Alive && currentSceneIndex != 0)
        {
            RespondToRotationInput();
            RespondToThrustInput();
        }


        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        } else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled; //toggle collision
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled) //ignore collisions once a single collision is detected
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

     private void StartSuccessSequence()
    {
        state = State.LevelUp;
        audioSource.Stop();
        audioSource.volume = 1f;

        if (Level1 == SceneManager.GetActiveScene().name)
        {
            audioSource.PlayOneShot(GemTrekVictoryD, 1f);
        }
        else if (Level2 == SceneManager.GetActiveScene().name)
        {
            audioSource.PlayOneShot(GemTrekVictoryE, 1f);
        }
        else if (Level3 == SceneManager.GetActiveScene().name)
        {
            audioSource.PlayOneShot(GemTrekVictoryF, 1f);
        }
        else if (Level4 == SceneManager.GetActiveScene().name)
        {
            audioSource.PlayOneShot(GemTrekVictoryG, 1f);
        }
        else if (Level5 == SceneManager.GetActiveScene().name)
        {
            audioSource.PlayOneShot(GemTrekVictoryA, 1f);
        }
        successParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay); // make time a parameter
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        a.StopMusic();
        audioSource.volume = 1f;
        audioSource.PlayOneShot(GemTrekExpl);
        explosionParticles.Play();
        Invoke("RestartGameOnDeath", levelLoadDelay);
    }

    private void RestartGameOnDeath()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            
        } else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }  
    }

    public static class FadeAudioSource
    {

        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void RespondToRotationInput()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.volume = 1f;
            audioSource.clip = GemTrekRocketEngine;
            audioSource.Play();
            audioSource.loop = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(FadeAudioSource.StartFade(audioSource, 0.01f, 0f));
            exhaustParticles.Stop();
            audioSource.loop = false;
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        exhaustParticles.Play();
    }        
}
