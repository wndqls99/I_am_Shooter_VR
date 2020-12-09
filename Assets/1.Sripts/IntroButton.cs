using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroButton : MonoBehaviour
{
    [SerializeField] GameObject soundManager;
    public void StartBtn()
    {
        SceneManager.LoadScene("Stage");
        DontDestroyOnLoad(soundManager);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
