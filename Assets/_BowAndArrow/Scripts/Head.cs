using UnityEngine;

public class Head : BodyPart, IDamageable // 머리는 몸에 상속되어 있다. 또한 데미지도 받는다.
{
    public new void Damage(int amount)
    {
        owner.TakeDamage(amount * 2); // 소유자(몸)에 대미지를 받는다 (그런데 2배 만큼 받는다)
    }
}
