using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;     // �O�i�E��ނ̑��x
    [SerializeField] private float rotateSpeed = 90f;   // ���񑬓x�i�x/�b�j

    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation;
    }

    void Update()
    {
        // --- ��]���� ---
        float yaw = 0f;
        float pitch = 0f;

        // ���E�L�[�Ń��[��]
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) yaw = rotateSpeed * Time.deltaTime;

        // �㉺�L�[�Ńs�b�`��]
        if (Input.GetKey(KeyCode.UpArrow)) pitch = -rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) pitch = rotateSpeed * Time.deltaTime;

        rotation *= Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = rotation;

        // --- �ړ����� ---
        float move = 0f;
        if (Input.GetKey(KeyCode.W)) move = 1f;   // �O�i
        if (Input.GetKey(KeyCode.S)) move = -1f;  // ���

        transform.position += transform.forward * move * moveSpeed * Time.deltaTime;
    }
}