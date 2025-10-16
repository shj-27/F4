using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // ���� �� ����
    private Rigidbody2D rb;

    [Header("Pause Settings")]
    // ȯ�� ����(Pause) �г� UI�� ������ �����Դϴ�.
    public GameObject pausePanel;
    private bool isPaused = false; // ���� ������ �Ͻ� ���� �������� Ȯ��

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
        // ���콺 ���� ��ư Ŭ�� �Ǵ� �����̽��� �Է� ����
        if (!isPaused && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }

        // ���� ȸ��
        // ���� ���� ���� �ӵ��� ���� ���Ʒ��� ȸ���ϵ��� ����
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 3f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause() // UI ��ư���� ȣ���� �� �ֵ��� public���� ����
    {
        isPaused = !isPaused; // ���� ����

        if (isPaused)
        {
            // �Ͻ� ����: �ð� �帧�� ���߰� �г��� ǥ���մϴ�.
            Time.timeScale = 0f;
            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
            Debug.Log("���� �Ͻ� ���� (ESC)");
        }
        else
        {
            // �簳: �ð� �帧�� �ٽ� �����ϰ� �г��� ����ϴ�.
            Time.timeScale = 1f;
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
            Debug.Log("���� �簳 (ESC)");
        }
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
