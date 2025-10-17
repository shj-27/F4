using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;




public class PlayerController : MonoBehaviour
{

    // 점프 힘 (Inspector에서 조절)
    public float jumpForce = 5f;

    // 폭발 프리팹 (Inspector에서 할당)
    public GameObject explosionPrefab;

    private Rigidbody2D rb;

    // 게임 오버를 외부에 알리는 이벤트
    public static event System.Action OnDied;

    // ------------------
    // 1. 초기화 (Start)
    // ------------------
    void Start()
    {
        // Rigidbody2D 컴포넌트 참조
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    // ------------------
    // 2. 입력 처리 (Update)
    // ------------------
    void Update()
    {
        // 마우스 클릭 또는 스페이스바 입력 감지
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // 새가 살아있을 때만 점프 가능하도록 체크
            if (rb != null && !rb.isKinematic)
            {
                Flap();
            }
        }
    }

    // ------------------
    // 3. 점프 로직 (Flap)
    // ------------------
    void Flap()
    {
        // 현재 속도 초기화 (이전 관성 제거)
        rb.velocity = Vector2.zero;

        // 위쪽으로 순간적인 힘 가하기
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // ------------------
    // 4. 충돌 처리 (OnCollisionEnter2D)
    // ------------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 파이프, 바닥 등 충돌 시 '죽음' 함수 호출
        Die();
    }

    // ------------------
    // 5. 죽음 / 게임 오버 로직 (Die)
    // ------------------
    void Die()
    {
        // 이미 죽은 상태가 아닌지 확인
        if (rb != null && !rb.isKinematic)
        {
            // (a) 물리 엔진 비활성화 및 움직임 멈춤
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;

            // (b) 폭발 효과 생성
            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                // 폭발 애니메이션 재생 시간만큼 후 오브젝트 파괴
                Destroy(explosion, 2.0f);
            }

            // (c) 새 오브젝트 숨기기
            gameObject.SetActive(false);

            // (d) 게임 매니저에 게임 오버 알림
            OnDied?.Invoke();
        }
    }
}
