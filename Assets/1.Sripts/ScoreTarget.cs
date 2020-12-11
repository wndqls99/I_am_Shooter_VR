using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTarget : MonoBehaviour
{
    public int point;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name.Contains("Arrow")){
            print("화살 맞음");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
