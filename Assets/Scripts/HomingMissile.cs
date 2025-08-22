using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 15f;          // 移動スピード（少し下げる）
    public float rotateSpeed = 10f;    // 旋回速度を上げる
    private Transform target;
    private float hitRange = 1.5f;     // 当たり判定を少し広げる

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // ターゲット方向を計算
        Vector3 dir = (target.position - transform.position).normalized;

        // 旋回処理（Slerp）
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        // 前進
        transform.position += transform.forward * speed * Time.deltaTime;

        // 命中判定（コライダー不要の簡易判定）
        if (Vector3.Distance(transform.position, target.position) <= hitRange)
        {
            var enemy = target.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                enemy.OnHit();
            }
            Destroy(gameObject);
        }
    }
}