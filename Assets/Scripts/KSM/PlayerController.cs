using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // 점프 힘 설정
    private Rigidbody2D rb;

    [Header("Pause Settings")]
    // 환경 설정(Pause) 패널 UI를 연결할 변수입니다.
    public GameObject pausePanel;
    private bool isPaused = false; // 현재 게임이 일시 정지 상태인지 확인

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 또는 스페이스바 입력 감지
        if (!isPaused && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }

        // 새의 회전
        // 새의 현재 수직 속도에 따라 위아래로 회전하도록 설정
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 3f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause() // UI 버튼에서 호출할 수 있도록 public으로 변경
    {
        isPaused = !isPaused; // 상태 반전

        if (isPaused)
        {
            // 일시 정지: 시간 흐름을 멈추고 패널을 표시합니다.
            Time.timeScale = 0f;
            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
            Debug.Log("게임 일시 정지 (ESC)");
        }
        else
        {
            // 재개: 시간 흐름을 다시 시작하고 패널을 숨깁니다.
            Time.timeScale = 1f;
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
            Debug.Log("게임 재개 (ESC)");
        }
    }

    void Jump()
    {
        // 기존 속도 제거 후 위쪽 방향으로 힘 가하기
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Pipe"인지 확인
        if (collision.gameObject.tag == "Pipe")
        {
            Die(); // "Pipe" 태그와 충돌하면 사망 처리 함수 호출
        }
    }

    void Die()
    {
        Debug.Log("게임 오버! 파이프와 충돌했습니다.");

        // 1. 플레이어 조작을 막기 위해 현재 스크립트 비활성화
        enabled = false;

        // 2. 물리 동작을 멈추기 위해 Rigidbody2D 비활성화
        rb.simulated = false;

        // 3. 게임 오버 처리를 위해 게임 시간 전체를 멈춤
        Time.timeScale = 0;

    }
}
