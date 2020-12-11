using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    //public Text text;
    LineRenderer line;
    RaycastHit hitInfo;
    Ray ray;
    float raycastDistance = 10.0f;
    ButtonState btnState;

    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        btnState = FindObjectOfType<ButtonState>();
    }

    // Update is called once per frame
    void Update()
    {
        //int layer = 1 << LayerMask.NameToLayer("Ignore Raycast");
        line.SetPosition(0, transform.position);
        ray = new Ray(transform.position, transform.forward * 10);
        //if (Physics.Raycast(ray, out hitInfo, 100, ~layer)
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            line.enabled = true;
            line.SetPosition(1, hitInfo.point);


            if (hitInfo.collider.gameObject.CompareTag("Button"))
            {
                hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);
                temp = hitInfo.collider.gameObject;
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))//오른손 검지 트리거
            {
                if (hitInfo.collider.gameObject.CompareTag("Button"))
                {
                    //버튼 사운드를 재생한다
                    SoundManager.instance.PlayBtnSound();
                    hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    //temp = hitInfo.collider.gameObject;
                    hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);

                }
                else
                {
                }
            }
            
        }

        else
        {
            line.SetPosition(1, transform.position + (transform.forward * raycastDistance));
            if (temp != null)
            {
                /*btnState.SetStateIdle();
                arrowState.SetStateIdle();*/
                switch (temp.gameObject.tag)
                {
                    case "Button":
                        temp.GetComponent<ButtonState>().SetButton(ButtonState.State.Idle);
                        break;
                }
                temp = null;

            }



        }
    }
}

