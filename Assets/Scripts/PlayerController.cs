using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;     // 前進・後退の速度
    [SerializeField] private float rotateSpeed = 90f;   // 旋回速度（度/秒）

    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation;
    }

    void Update()
    {
        // --- 回転処理 ---
        float yaw = 0f;
        float pitch = 0f;

        // 左右キーでヨー回転
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) yaw = rotateSpeed * Time.deltaTime;

        // 上下キーでピッチ回転
        if (Input.GetKey(KeyCode.UpArrow)) pitch = -rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) pitch = rotateSpeed * Time.deltaTime;

        rotation *= Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = rotation;

        // --- 移動処理 ---
        float move = 0f;
        if (Input.GetKey(KeyCode.W)) move = 1f;   // 前進
        if (Input.GetKey(KeyCode.S)) move = -1f;  // 後退

        transform.position += transform.forward * move * moveSpeed * Time.deltaTime;
    }
}