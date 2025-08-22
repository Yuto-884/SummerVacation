using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;  // 敵プレハブ
    [SerializeField] private int enemyCount = 24;     // 生成数
    [SerializeField] private float spawnDistance = 20f; // プレイヤーからの距離
    [SerializeField] private float spread = 10f;        // 左右/上下に散らす幅

    [SerializeField] private Transform player;  // プレイヤーをInspectorで指定

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // プレイヤーの前方ベクトル
            Vector3 forward = player.forward;

            // プレイヤーの右方向と上方向を使って散らす
            Vector3 right = player.right;
            Vector3 up = player.up;

            // 前方に配置 + ランダムで横・縦に散らす
            Vector3 pos = player.position +
                          forward * spawnDistance +
                          right * Random.Range(-spread, spread) +
                          up * Random.Range(-spread * 0.5f, spread * 0.5f);

            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
