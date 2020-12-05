using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    AudioSource audioSource;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip btnSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = intro;
        if(SceneManager.GetActiveScene().name == "Intro" || SceneManager.GetActiveScene().name == "Stage")
        {
            PlayIntroSound();
        }
    }
    public void PlayIntroSound()
    {
        audioSource.PlayOneShot(intro);
    }
    public void PlayBtnSound()
    {
        audioSource.PlayOneShot(btnSound);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
