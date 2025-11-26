using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟를 바라보도록 업데이트하는 스크립트
/// </summary>
public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        Vector3 dir = _target.position - this.transform.position;
        dir.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
    }
}
