using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour
{
    public enum State { 
        Idle,
        On, 
        Off,
    }
    public State state;
    public void SetButton(State state)
    {
        this.state = state;
    }
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
