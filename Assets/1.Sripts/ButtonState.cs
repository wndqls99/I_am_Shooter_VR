using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour
{
    Image btnImg;
    Sprite idleImg;
    Sprite onImg;

    public enum State { 
        Idle,
        On, 
    }
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        btnImg = GetComponent<Image>();
        idleImg = Resources.Load<Sprite>("Btn/Btn");
        onImg = Resources.Load<Sprite>("Btn/Btn_1");
        SetButton(State.Idle);
    }
    public void SetButton(State state)
    {
        this.state = state;
        switch (this.state)
        {
            case State.Idle:
                btnImg.sprite = idleImg;
                break;
            case State.On:
                btnImg.sprite = onImg;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
