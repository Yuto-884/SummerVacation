using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;  // CubeのPrefab
    [SerializeField] private int enemyCount = 24;     // 生成する数
    [SerializeField] private float spawnRadius = 30f; // 生成する範囲の半径

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // ランダムな位置を決める（XZ平面に広げる）
            Vector3 pos = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                Random.Range(0f, 5f),   // 高さは適当（0〜5の間とか）
                Random.Range(-spawnRadius, spawnRadius)
            );

            // 生成
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
