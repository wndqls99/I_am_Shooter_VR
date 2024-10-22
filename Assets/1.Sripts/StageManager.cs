﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instane;
    private void Awake() {
        instane = this;
    }
    int myStage;
    public int MyStage{
        get{return myStage;}
        set{myStage = value;}
    }
    [SerializeField] TextMeshProUGUI stageTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] GameObject soundManager;
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        SetStageNum(MyStage);//레벨 1

    }
    public void SetStageNum(int num){
        stageTxt.text = "스테이지 " + DB.instance.stageList[MyStage].num.ToString();//입력받은 값의 스테이지 정보 출력
        scoreTxt.text = DB.instance.stageList[MyStage].score.ToString() + " 점";
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickNext(){
        if(MyStage < 2){
            MyStage++;
            SetStageNum(MyStage);
        }
    }
    public void ClickPre(){
        if(MyStage > 0){
            MyStage--;
            SetStageNum(MyStage);    
        }
    }
    public void ClickStart(){
        // switch(myStage){
        //     case 1 :
        //         SceneManager.LoadScene("Main1");
        //         DontDestroyOnLoad(soundManager);
        //     break;
        //     case 2 :
        //         SceneManager.LoadScene("Main2");
        //         DontDestroyOnLoad(soundManager);
        //     break;
        //     case 3 :
        //         SceneManager.LoadScene("Main3");
        //         DontDestroyOnLoad(soundManager);
        //     break;
        //     default:
        //     break;
        // }
        SceneManager.LoadScene("Main3");
        DontDestroyOnLoad(soundManager);
    }
}
