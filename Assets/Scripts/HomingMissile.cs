using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 15f;          // �ړ��X�s�[�h�i����������j
    public float rotateSpeed = 10f;    // ���񑬓x���グ��
    private Transform target;
    private float hitRange = 1.5f;     // �����蔻��������L����

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

        // �^�[�Q�b�g�������v�Z
        Vector3 dir = (target.position - transform.position).normalized;

        // ���񏈗��iSlerp�j
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        // �O�i
        transform.position += transform.forward * speed * Time.deltaTime;

        // ��������i�R���C�_�[�s�v�̊ȈՔ���j
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