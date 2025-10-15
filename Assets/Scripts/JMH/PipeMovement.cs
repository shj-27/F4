using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 2f;
    public float destroyX = -15f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            gameObject.SetActive(false); // 화면 밖으로 나가면 비활성화
        }
    }
}