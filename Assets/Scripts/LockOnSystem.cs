using System.Collections.Generic;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public GameObject bulletPrefab;    // 追尾弾プレハブ
    public float lockOnRange = 50f;    // ロックオンできる距離
    public int maxLockOns = 8;         // 最大ロックオン数

    private List<Transform> lockOnTargets = new List<Transform>();
    private bool isLockingOn = false;

    void Update()
    {
        // Zキー押しっぱなしでロックオン開始
        if (Input.GetKey(KeyCode.Z))
        {
            isLockingOn = true;
            UpdateLockOnTargets();
        }

        // Zキー離したときに弾発射
        if (Input.GetKeyUp(KeyCode.Z))
        {
            FireBullets();
            lockOnTargets.Clear();
            isLockingOn = false;
        }
    }

    // ロックオン対象を更新
    void UpdateLockOnTargets()
    {
        lockOnTargets.Clear();

        // 前方にいる全敵を取得
        var enemies = Object.FindObjectsByType<EnemyMove>(FindObjectsSortMode.None);
        foreach (var e in enemies)
        {
            Vector3 dir = e.transform.position - transform.position;
            if (Vector3.Dot(transform.forward, dir.normalized) > 0.3f && dir.magnitude < lockOnRange)
            {
                lockOnTargets.Add(e.transform);
                if (lockOnTargets.Count >= maxLockOns) break;
            }
        }

        // ロックオンされた敵を赤くする
        foreach (var e in enemies)
        {
            if (lockOnTargets.Contains(e.transform))
                e.SetLockOn(true);
            else
                e.SetLockOn(false);
        }
    }

    // 追尾弾を発射
    void FireBullets()
    {
        foreach (var enemy in lockOnTargets)
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
            b.GetComponent<HomingMissile>().SetTarget(enemy);
        }
    }
}