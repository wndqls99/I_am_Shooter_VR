using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI arrowText;
    public static ArrowManager instance;
    private void Awake() {
        instance = this;
    }
    int arrow = 14;
    public int Arrow{
        get{return arrow;}
        set{arrow = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("Main1")){
            arrow = 5;
        }        
        if(SceneManager.GetActiveScene().name.Equals("Main2")){
            arrow = 4;
        } 
        if(SceneManager.GetActiveScene().name.Equals("Main3")){
            arrow = 14;
        }
        RemainArrow();
    }
    public void RemainArrow(){
        if(arrow <= 0){
            arrowText.text = "화살 없음";
        }else{
            arrowText.text = Arrow.ToString() + "발 남음";
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(arrow == 0){
            GameManager.instance.GameClear();
        }
    }
}
