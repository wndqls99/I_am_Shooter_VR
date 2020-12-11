using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private void Awake() {
        instance = this;
    }
    int score = 0;
    public int Score{
        get{return score;}
        set{score = value;}
    }
    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update

    public void SetScore(){
        scoreText.text = score.ToString() + "점";
    }
    // Update is called once per frame
    void Update()
    {
        SetScore();
    }
}
