using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI arrowText;
    public static ArrowManager instance;
    private void Awake() {
        instance = this;
    }
    int arrow = 15;
    public int Arrow{
        get{return arrow;}
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RemainArrow(){
        arrowText.text = arrow.ToString() + "발 남음";
    }
    // Update is called once per frame
    void Update()
    {
        RemainArrow();
    }
}
