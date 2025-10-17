using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;




public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;
    public bool itsReset;
    // ���� �� (Inspector���� ����)
    public float jumpForce = 5f;

    // ���� ������ (Inspector���� �Ҵ�)
    public GameObject explosionPrefab;

    private Rigidbody2D rb;
    

    // ���� ������ �ܺο� �˸��� �̺�Ʈ
    public static event System.Action OnDied;

   
    void Start()
    {
        
        itsReset = false;
        // Rigidbody2D ������Ʈ ����
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        // ���콺 Ŭ�� �Ǵ� �����̽��� �Է� ����
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // ���� ������� ���� ���� �����ϵ��� üũ
            if (rb != null && !rb.isKinematic)
            {
                Flap();
            }

        

        }
        if ((itsReset == true) && Input.GetKeyDown(KeyCode.R)) 
        {
            Reset();
        }

        
    }

    
    
    private void Reset()
    {
            SceneManager.LoadScene("Change");
        
    }
    
    void Flap()
    {
        // ���� �ӵ� �ʱ�ȭ (���� ���� ����)
        rb.velocity = Vector2.zero;

        // �������� �������� �� ���ϱ�
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

  
    // �浹 ó�� (OnCollisionEnter2D)
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ������, �ٴ� �� �浹 �� '����' �Լ� ȣ��
        Die();
        GameManager.itsReset = true;
        if (collision.collider.CompareTag("Pipe"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.RestartGame();
            Debug.Log("�ߵ���");
        }
    }

    
    // ���� / ���� ���� ���� (Die)
    
    void Die()
    {
        // �̹� ���� ���°� �ƴ��� Ȯ��
        if (rb != null && !rb.isKinematic)
        {
            // (a) ���� ���� ��Ȱ��ȭ �� ������ ����
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;

            // (b) ���� ȿ�� ����
            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                // ���� �ִϸ��̼� ��� �ð���ŭ �� ������Ʈ �ı�
                Destroy(explosion, 2.0f);
            }


            // (c) �� ������Ʈ �����
            gameObject.SetActive(false);

            // (d) ���� �Ŵ����� ���� ���� �˸�
            OnDied?.Invoke();

        }
    }
}
