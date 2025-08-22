using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Renderer rend;
    private Color defaultColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

    void Update()
    {
        // ふわふわ移動
        transform.position += new Vector3(
            Mathf.Sin(Time.time * 1.2f) * 0.001f,
            Mathf.Cos(Time.time * 1.5f) * 0.001f,
            0f
        );
    }

    // ロックオン状態の色変更
    public void SetLockOn(bool isLocked)
    {
        rend.material.color = isLocked ? Color.red : defaultColor;
    }

    // 被弾処理
    public void OnHit()
    {
        StartCoroutine(DamageReaction());
    }

    private System.Collections.IEnumerator DamageReaction()
    {
        rend.material.color = Color.yellow;
        float shakeTime = 0.5f;
        float timer = 0f;

        while (timer < shakeTime)
        {
            transform.position += Random.insideUnitSphere * 0.05f;
            timer += Time.deltaTime;
            yield return null;
        }

        rend.material.color = defaultColor;
    }
}
