using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class OculusInput : MonoBehaviour
{
    public Bow m_Bow = null; // 활 객체 받는곳
    public GameObject m_OppositeController = null; // 반대손(오른손) 컨트롤러 넣는곳
    public OVRInput.Controller m_Controller = OVRInput.Controller.None; // R Thouch 넣는곳

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_Controller)) // 버튼을 누르면
            m_Bow.Pull(m_OppositeController.transform); // 활을 당긴다

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_Controller)) // 버튼을 놓으면
            m_Bow.Release(); // 활시위를 놓는다
            
    }
}
