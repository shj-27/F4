using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // ���� �� ����
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� �Ǵ� �����̽��� �Է� ����
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // ���� ȸ��
        // ���� ���� ���� �ӵ��� ���� ���Ʒ��� ȸ���ϵ��� ����
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 3f);
    }

    void Jump()
    {
        // ���� �ӵ� ���� �� ���� �������� �� ���ϱ�
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
