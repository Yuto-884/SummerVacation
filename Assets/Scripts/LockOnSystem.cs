using System.Collections.Generic;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public GameObject bulletPrefab;    // �ǔ��e�v���n�u
    public float lockOnRange = 50f;    // ���b�N�I���ł��鋗��
    public int maxLockOns = 8;         // �ő働�b�N�I����

    private List<Transform> lockOnTargets = new List<Transform>();
    private bool isLockingOn = false;

    void Update()
    {
        // Z�L�[�������ςȂ��Ń��b�N�I���J�n
        if (Input.GetKey(KeyCode.Z))
        {
            isLockingOn = true;
            UpdateLockOnTargets();
        }

        // Z�L�[�������Ƃ��ɒe����
        if (Input.GetKeyUp(KeyCode.Z))
        {
            FireBullets();
            lockOnTargets.Clear();
            isLockingOn = false;
        }
    }

    // ���b�N�I���Ώۂ��X�V
    void UpdateLockOnTargets()
    {
        lockOnTargets.Clear();

        // �O���ɂ���S�G���擾
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

        // ���b�N�I�����ꂽ�G��Ԃ�����
        foreach (var e in enemies)
        {
            if (lockOnTargets.Contains(e.transform))
                e.SetLockOn(true);
            else
                e.SetLockOn(false);
        }
    }

    // �ǔ��e�𔭎�
    void FireBullets()
    {
        foreach (var enemy in lockOnTargets)
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
            b.GetComponent<HomingMissile>().SetTarget(enemy);
        }
    }
}