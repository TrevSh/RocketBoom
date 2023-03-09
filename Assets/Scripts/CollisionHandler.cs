using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip victoryAudio;
    [SerializeField] AudioClip fuelAudio;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] TMP_Text fuelGuage;

    public bool cheatsEnabled = true;
    bool toggleCollision = false;
    AudioSource audioSource;
    public float baseFuelAmount = 10;
    public float currentFuelAmount = 0f;
    public float gainFuelAmount = 5f;

    bool isTransitioning = false;
    void Start()
    {
        currentFuelAmount = baseFuelAmount;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log(currentFuelAmount);
        useCheat();
        fuelGuage.text = currentFuelAmount.ToString();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || toggleCollision) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Welcome Home!");
                break;
            case "Finish":
                StartSuccessSequence();
                //Debug.Log("Course Complete!");
                break;
            default:
                Debug.Log("Crashed!");
                StartCrashSequence();
                break;
        }
    }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Fuel"))
            {
                //Destroy(other.gameObject);
                GainFuel(gainFuelAmount);
               other.GetComponent<AudioSource>().Play();
                
            }
        }

   

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        Debug.Log("Playing Victory Scream.");
        audioSource.PlayOneShot(victoryAudio);
        successParticle.Play();
        Invoke("LoadNextLevel", delayTime);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
        
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void useCheat()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log(toggleCollision);
            toggleCollision = !toggleCollision;
        }
    }

    void GainFuel(float fuel)
    {
        gainFuelAmount = fuel;
        currentFuelAmount += fuel;
    }

    void DestroyFuel(GameObject fuel)
    {
        Destroy(fuel);
    }

}
