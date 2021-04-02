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
    [SerializeField] AudioClip arrowSound;
    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = intro;

        PlayIntroSound();
    }
    public void PlayIntroSound()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(intro);
    }
    public void PlayBtnSound()
    {
        audioSource.PlayOneShot(btnSound);
    }
    public void FireArrow(){
        audioSource.PlayOneShot(arrowSound);
    }
    public void PlayShot(){
        audioSource.PlayOneShot(shotSound);
    }
    public void Explosion(){
        audioSource.PlayOneShot(explosionSound);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
