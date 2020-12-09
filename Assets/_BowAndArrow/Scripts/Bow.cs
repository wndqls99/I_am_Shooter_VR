using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject m_ArrowPrefab = null; // 화살 프리팹객체

    [Header("Bow")]
    public float m_GrabThreshold = 0.15f; // 잡는 위치 역치 값 (수치가 낮을수록 가하는 에너지에 비해 반응이 쉽게 일어난다.) -> 잡는위치의 강도값
    public Transform m_Start = null; // 화살 당기는 시작점 (idle 위치)
    public Transform m_End = null; // 화살이 끝까지 당겨진 위치
    public Transform m_Socket = null; // 화살촉이 닿는 활시위(활의현) 위치

    private Transform m_PullingHand = null; // 당기는 손 위치
    private Arrow m_CurrentArrow = null; // 화살
    private Animator m_Animator = null; // 애니메이션

    private float m_PullValue = 0.0f; // 당기는 값(당기는 힘의 값)

    private void Awake() // 항상 Start 전에 호출되며 프리팹이 인스턴스화 된 직후에 호출된다 (단, 게임오브젝트가 시작동안 비활성화인 경우 호출되지않는다)
    {
        m_Animator = GetComponent<Animator>(); // 애니메이터 초기화
    }

    private void Start() // 스크립트 인스턴스가 활성화 된 경우 첫 번째 프레임 업데이트함수 호출 전 에 호출 된다. (다른 스크립트의 Awake가 실행 된 이후에 실행된다.)
    {
        StartCoroutine(CreateArrow(0.0f)); // 코루틴 실행, 0초마다 하살 생성
    }

    private void Update() // 매 프레임마다 호출되는 함수 (스크립트가 활성화 되어있어야 사용이 가능하다.)
    {
        if (!m_PullingHand || !m_CurrentArrow) // 손을 당기고 있거나 화살을 가지고 있을경우
            return; // 아무것도 하지 않는다.

        m_PullValue = CalculatePull(m_PullingHand); // 당기는 값을 계산
        m_PullValue = Mathf.Clamp(m_PullValue, 0.0f, 1.0f); // 클램프를 통해 float값을 0~1값을 넘지 않게 설정해줌

        m_Animator.SetFloat("Blend", m_PullValue); // 활 애니메이션의 블랜드 트리의 '블랜드'파라미터에 값을 넣어준다. 
    }

    private float CalculatePull(Transform pullHand) // 활시위 당기는것 계산
    {
        Vector3 direction = m_End.position - m_Start.position; // 끝위치에서 시작위치를 뺀것으로 계산
        float magnitude = direction.magnitude; // 해당백터의 길이(크기)를 계산

        direction.Normalize(); // 해당 벡터가 1의 magnitude(규모)를 갖도록 설정
        Vector3 difference = pullHand.position - m_Start.position; // 당기는 손 위치와 시작 위치와의 차이 계산

        return Vector3.Dot(difference, direction) / magnitude; // Dot 메소드를 통해 두 백터간의 '내적'을 구하고 magnitude의 스칼라로 나눠서 얼마나 활을 당기고 있나 계산.
    }

    private IEnumerator CreateArrow(float waitTime) // IEnumerator(열거자) -> 코루틴을 위한 용도(기기별 프레임드랍을 고려한 반복행동) 즉, 성능에 상관없이 foreach식으로 하나씩 돌아가며 필요할때마다 하나씩 다음 개체순번으로 넘어감
    {
        // Wait
        yield return new WaitForSeconds(waitTime); // yield(양보) -> update에서 1초가 지났는지 확인후 MoveNext를 호출한다.(다음 프레임까지 대기) 그러면 코드의 다음 부분이 실행된다.

        // Create, child
        GameObject arrowObject = Instantiate(m_ArrowPrefab, m_Socket); // Instantiate을 통해 Prefab에 있는 화살을 m_Socket위치에 생성 -> 이후 arrowObject에 인스턴트화

        // Orient
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f); // 화살을 현 위로 위치(로컬좌표)시킨다. 월드좌표로 하면 활 밑에 자식으로 생성된 화살일경우 좌표가 다를수 있다. 0.425f는 이 활모델기준 가장 이상적인 좌표
        arrowObject.transform.localEulerAngles = Vector3.zero; // 부모의 상대적인 회전량 Degree값을 Vector3로 접근하는 프로퍼티로 0(영점)으로 잡아준다.

        // Set
        m_CurrentArrow = arrowObject.GetComponent<Arrow>(); // 이렇게 생성한 화살을 현제화살에 담아준다.
    }

    public void Pull(Transform hand) // 당기기
    {
        float distance = Vector3.Distance(hand.position, m_Start.position); // 손과 활시위의 거리를 계산

        if (distance > m_GrabThreshold) // 활시위소켓으로 부터 너무 먼 거리에서 당기려고하면 당기는걸로 인지하지 않는다.
            return;

        m_PullingHand = hand; // 현재 손을 당기는 손으로 만든다(인지한다).
    }

    public void Release() // 놓기
    {
        if (m_PullValue > 0.25f) // 당기는 값(거리)이(가) 0.25f보다 크면 
            FireArrow(); // 화살을 쏜다.

        m_PullingHand = null; // 당기고 있는 손 초기화

        m_PullValue = 0.0f; // 당기는 값 초기화
        m_Animator.SetFloat("Blend", 0); // 당기는 애니메이션도 0으로 초기화 하여 당기지 않는 모션으로 만들어줌.

        if (!m_CurrentArrow) // 활을 충분히 당겨서 화살을 쏘지 않은경우가 있을수 있으니 '완전히 화살이 활에 없는경우'
            StartCoroutine(CreateArrow(0.25f)); // 처음에만 0초로 하였고 이제 0.25초 마다 화살을 생성해서 날릴수 있게 코루틴 설정.
    }

    private void FireArrow()
    {
        m_CurrentArrow.Fire(m_PullValue); // 당기는 값에 맞춰서 화살을 쏜다.
        m_CurrentArrow = null; // 발사한뒤 현재 가지고 있는 화살을 null로 없게끔 한다.
    }
}
