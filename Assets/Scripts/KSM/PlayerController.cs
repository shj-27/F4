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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Pipe"���� Ȯ��
        if (collision.gameObject.tag == "Pipe")
        {
            Die(); // "Pipe" �±׿� �浹�ϸ� ��� ó�� �Լ� ȣ��
        }
    }

    void Die()
    {
        Debug.Log("���� ����! �������� �浹�߽��ϴ�.");

        // 1. �÷��̾� ������ ���� ���� ���� ��ũ��Ʈ ��Ȱ��ȭ
        enabled = false;

        // 2. ���� ������ ���߱� ���� Rigidbody2D ��Ȱ��ȭ
        rb.simulated = false;

        // 3. ���� ���� ó���� ���� ���� �ð� ��ü�� ����
        Time.timeScale = 0;

    }
}
