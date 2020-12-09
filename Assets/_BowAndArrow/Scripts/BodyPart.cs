using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable // IDamageable 를 상속 받아야만 데미지를 받을수 있다.
{
    protected Target owner = null; // 자신을 소유하는 '타켓' 소유자를 만든다.

    public void Setup(Target newOwner)
    {
        owner = newOwner; // 생성시 소유자 설정
    }

    public void Damage(int amount)
    {
        owner.TakeDamage(amount);  // 소유자에게 대미지를 준다.
    }
}
