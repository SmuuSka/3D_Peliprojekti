
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    public Slider volumeSlider; // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        volumeSlider.value = 0.5f;
    }
    private void Update()
    {
        {
            //Säädetään volume sliderin arvon mukaan vain jos ollaan main menussa
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            {
                //Haetaan slideria vain jos ollaan main menussa:
                GetComponent<AudioSource>().volume = volumeSlider.value;
                //print(volumeSlider.value);
            }
        }
    }
}

