using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // 점프 힘 설정
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 또는 스페이스바 입력 감지
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 새의 회전
        // 새의 현재 수직 속도에 따라 위아래로 회전하도록 설정
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 3f);
    }

    void Jump()
    {
        // 기존 속도 제거 후 위쪽 방향으로 힘 가하기
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
