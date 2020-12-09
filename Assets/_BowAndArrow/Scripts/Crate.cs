using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Crate : MonoBehaviour, IDamageable // IDamageable 를 상속 받아야만 데미지를 받을수 있다.
{
    public void Damage(int amount)
    {
        TurnRed(); // 데미지 받으면 빨간색으로 됨.
    }

    private void TurnRed() // 이 경우 뭐로 치던 빨간색이 됨.
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();  // MeshRenderer 정보를 가져오고
        meshRenderer.material.color = Color.red; // MeshRenderer를 통해 재질을 빨간색으로 바꿈.
    }

    private void Start() // Oculus Mirror 실행해서 모니터로도 보게끔
    {
        try
        {
            Process.Start("C:/Program Files/Oculus/Support/oculus-diagnostics/OculusMirror.exe");
        }
        catch(Exception ex)
        {
            // MessageBox.Show(Ex.Message);
            print("OculusMirror를 실행하지 못했습니다");
        }
    }
}
