using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePairPrefab;     // PipePair 프리팹
    public int poolSize = 5;              // 풀 크기
    public float spawnRate = 2f;          // 생성 간격
    public float spawnX = 10f;            // 생성 위치 X
    public float minY = -1f;              // 중심 Y 최소값
    public float maxY = 2f;               // 중심 Y 최대값
    public float gapSize = 2.5f;          // 상/하 파이프 간격

    private GameObject[] pipePool; // 파이프 풀
    private int currentIndex = 0; // 파이프 인덱스
    private float timeSinceLastSpawn; // 생성 간격

    void Start()
    {
        pipePool = new GameObject[poolSize]; // 파이프 풀 생성
        for (int i = 0; i < poolSize; i++)
        {
            pipePool[i] = Instantiate(pipePairPrefab, new Vector2(-20f, -20f), Quaternion.identity); // 파이프 풀 초기화
        }
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime; // 생성 간격 계산

        if (timeSinceLastSpawn >= spawnRate)
        {
            timeSinceLastSpawn = 0f; // 생성 간격 초기화

            float centerY = Random.Range(minY, maxY); // 중심 Y 랜덤 값 계산
            Vector2 spawnPosition = new Vector2(spawnX, centerY);

            pipePool[currentIndex].transform.position = spawnPosition; // 파이프 위치 설정
            pipePool[currentIndex].SetActive(true);

            currentIndex++; // 파이프 인덱스 증가
            if (currentIndex >= poolSize)
                currentIndex = 0; // 파이프 인덱스 초기화
        }
    }
}