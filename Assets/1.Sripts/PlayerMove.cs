using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject pos1;
    [SerializeField] GameObject pos2;
    bool pos = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.X)){
            print("x눌림");
            if(pos){
                gameObject.transform.position = pos2.transform.position;
                pos = false;
            }else{
                gameObject.transform.position = pos1.transform.position;
                pos = true;
            }
        }
    }
}
