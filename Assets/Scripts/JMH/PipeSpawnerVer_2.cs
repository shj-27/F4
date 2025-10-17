using UnityEngine;
using System.Collections.Generic;

public class PipeSpawnerVer_2 : MonoBehaviour
{
    [Header("파이프 생성 설정")]
    public GameObject pipePairPrefab;     // PipePair 프리팹
    public int poolSize = 5;              // 풀 크기
    public float spawnRate = 2f;          // 생성 간격
    public float minSpawnRate = 1f;       // 최소 생성 간격
    public float spawnX = 10f;            // 생성 위치 X
    public float minY = -1f;              // 중심 Y 최소값
    public float maxY = 2f;               // 중심 Y 최대값
    public float gapSize = 2.5f;          // 상/하 파이프 간격

    private float timeSinceLastSpawn; // 생성 간격
    public Queue<GameObject> pipePool = new Queue<GameObject>();

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime; // 생성 간격 계산

        float difficulty = GameManager.Instance.GetDifficultyMultiplier();
        float currentSpawnRate = Mathf.Max(minSpawnRate, spawnRate / difficulty); //난이도 조절(생성주기)

        if (timeSinceLastSpawn >= currentSpawnRate)
        {
            SpawnPipe(currentSpawnRate);
            timeSinceLastSpawn = 0f; // 생성 간격 초기화
        }
    }

    void SpawnPipe(float currentSpawnRate)
    {
        GameObject pipe = GetFromPool(pipePool, pipePairPrefab);
        float centerY = Random.Range(minY, maxY); // 중심 Y 랜덤 값 계산
        Vector2 spawnPosition = new Vector2(spawnX, centerY);
        pipe.transform.position = spawnPosition; // Y축 랜덤
        pipe.SetActive(true);
    }

    GameObject GetFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            return obj;
        }
        else
        {
            return Instantiate(prefab);
        }
    }

    public void ReturnToPool(Queue<GameObject> pool, GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}