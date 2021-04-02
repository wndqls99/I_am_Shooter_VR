using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        instance = this;
        SetTargets();
    }
    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] GameObject ray;
    //public GameObject[] targets;
    int targets = 0;
    public void SetTargets(){
        switch(SceneManager.GetActiveScene().name){
            case "Main1":
                targets = 3;
                break;
            case "Main2":
                targets = 4;
                break;                
            case "Main3":
                targets = 12;
                break;    
        }

    }
    public int Targets{
        get{return targets;}
        set{targets = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        resultUI.SetActive(false);
        ray.SetActive(false);
    }
    public void GameClear(){
        resultUI.SetActive(true);
        resultText.text = ScoreManager.instance.Score.ToString() + "점";
        if(targets == 0){
            gameText.text = "게임클리어";
            DB.instance.UpdateScore(ScoreManager.instance.Score);
        }else{
            gameText.text = "게임오버";
        }
        ray.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(targets == 0){
            Invoke("GameClear", 2.0f);
            //GameClear();
        }
    }
}
