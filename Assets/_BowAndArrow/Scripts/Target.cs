using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Health { private set; get; } = 10; // 체력
    public bool Alive { private set; get; } = true; // 죽었나 살았나 확인

    private Animator animator = null; // 애니메이션

    private void Awake() // 인스턴트 생성시
    {
        animator = GetComponent<Animator>(); // 애니메이션 컴포넌트 가져옴
        SetupBodyParts(); // SetupBodyParts 실행
    }

    private void SetupBodyParts() // 부위(자식)부분 상속(소유자) 지정
    {
        BodyPart[] bodyParts = GetComponentsInChildren<BodyPart>(); // BodyPart내의 모든 자식 컴포넌트를 가져온다 (팔,다리,머리 등등)

        foreach (BodyPart bodyPart in bodyParts) // 가져온 bodyParts를 배열로 돌린다.
            bodyPart.Setup(this); // 모두 Target을 소유자로 해준다.
    }

    public void TakeDamage(int damage) // 데미지를 받는다.
    {
            if (!Alive) // 살아 있지 않다면
                return; // 데미지 입지 않는다.
            
            Health -= damage; // 체력을 데미지만큼 차감

            if(Health <= 0) // 체력이 0 이하가되면
                Kill(); // 죽는다.
    }

    public void Kill() // 죽으면
    {
        Alive = false; // 죽었다고 처리
        animator.SetBool("PickUp", true); // Pickup 애니메이션 실행
    }
}
