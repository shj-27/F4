using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 2f;
    public float maxSpeed = 5f; //최대 속도
    public float destroyX = -15f;

    private PipeSpawnerVer_2 spawner;

    void Start()
    {
        spawner = FindObjectOfType<PipeSpawnerVer_2>();
    }

    void Update()
    {
        float difficulty = GameManager.Instance.GetDifficultyMultiplier(); 
        float currentSpeed = Mathf.Min(maxSpeed, speed * difficulty); //난이도 조절(파이프 속도)

        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            spawner.ReturnToPool(spawner.pipePool, gameObject);
            gameObject.SetActive(false); // 화면 밖으로 나가면 비활성화
        }
    }
}