using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float m_Speed = 2000.0f; // 날아가는 속도 값
    public Transform m_Tip = null; // 히트 감지를 위한 화살촉

    private Rigidbody m_Rigidbody = null; // 물리 제어를 위한 강체
    private bool m_IsStopped = true; // 공기중을 날아가다 가고 있는지 확인 (비행 여부 값)
    private Vector3 m_LastPosition = Vector3.zero; // 마지막 위치를 0 으로 한다.

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); // 리지드 바디 가져오기
    }

    private void Start()
    {
        m_LastPosition = transform.position; // 마지막 위치를 현재 위치(활 위에 있는 위치)로 지정
    }

    private void FixedUpdate()
    {
        // 뭔가 맞추어서 멈춘상태라면 업데이트 하지 않는다 (If we've hit something, don't update)
        if (m_IsStopped)
            return;

        // 물리에 따른 회전(Rotate with physics)
        m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up)); // 강체를 회전시킨다. 쿼터니온의 회전을 통제하여, 강체의 벡터(바라보는 방향)을 상향 하게 한다)

        // 충돌 처리(Collision check)
        RaycastHit hit; // 충동에 대한 정보
        if(Physics.Linecast(m_LastPosition, m_Tip.position, out hit)) // Linecast을 통해 직선상으로 충돌체 여부확인. (시작점이 마지막 위치, 끝점이 현재위치 ※라인케스트를 사용한 이유는 물체가 빠르게 움직일경우 물리처리(대표적으로 충돌)이 제대로 이루어지지 않을수있기 때문.)
        { // 이 두 점 사이에 뭐가 있다면
            Stop(hit.collider.gameObject); // 화살을 멈춘다. (충돌체 정보 전달)
        }

        // (다음 프레임을 위한 위치 업데이트)Store position for next frame
        m_LastPosition = m_Tip.position; // 현제 화살촉 위치를 마지막 위치로 설정
    }

    private void Stop(GameObject hitObject) // 이동을 멈추게 한다. (충돌체에 맞은경우)
    {
        // Flag
        m_IsStopped = true; // 멈춘거로 처리

        // Parent
        transform.parent = hitObject.transform; // 부모 위치를 충돌체 위치로 잡아준다.

        // Disable Physics
        m_Rigidbody.isKinematic = true; // 외부의 힘(물리력)이 가해지지 않게 한다.
        m_Rigidbody.useGravity = false; // 중력을 받지 않게 한다.

        // Damage
        CheckForDamage(hitObject); // 충돌체에 대한 데미지 계산
    }

    private void CheckForDamage(GameObject hitObject) // 데미지 계산
    {
        MonoBehaviour[] behaviours = hitObject.GetComponents<MonoBehaviour>(); // 모든 충돌체에 대한 정보를 가져온다.

        foreach (MonoBehaviour behaviour in behaviours) // 충돌체 정보를 배열로 하여 foreach 실행
        {
            if(behaviour is IDamageable) // 만일 데미지를 입을수 있다면
            {
                IDamageable damageable = (IDamageable)behaviour; // 데미지 가능 객채생성하여 담아둔다.
                damageable.Damage(5);  // 5만큼 데미지를 준다.

                break; // 빠져나옴으로써 다른 충돌체에 대미지를 안주게끔 한다.
            }
        }
    }

    public void Fire(float pullValue) // 화살을 쏠때 (활에서 호출)
    {
        m_LastPosition = transform.position; // 마지막 위치를 현재 위치로 한다. (쏘여진 위치, 즉 부모로부터 분리된 위치)

        // Flag
        m_IsStopped = false; // 날아가고 있는거로 처리

        // 부모로부터 분리(Unparent)
        transform.parent = null; // 활에 더이상 부착이 안되게 자신을 생성한 부모(활)를 null로 한다.

        // 물리적용(Physics)
        m_Rigidbody.isKinematic = false; // 외부의 힘(물리력)이 가해지게 한다.
        m_Rigidbody.useGravity = true; // 중력에 영향을 받게 한다.
        m_Rigidbody.AddForce(transform.forward * (pullValue * m_Speed)); // 강체에 물리힘을 적용하여 이동하게끔한다 (윌드 좌표상 앞으로 보는 방향에 * (당기는 힘 * 속도))

        Destroy(gameObject, 5.0f); // 5초뒤에 삭제 ---> 아직 날아가고 있어도 삭제가 된다.
    }
}
