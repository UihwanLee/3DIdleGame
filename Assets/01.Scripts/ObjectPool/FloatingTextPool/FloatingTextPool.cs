using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damage를 표시하기 위해 Damage Prefab을 저장한 Pool
/// </summary>
public class FloatingTextPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _maxCount;
    [SerializeField] private Transform _canvasParent;
    [SerializeField] private FloatingText data;

    private PoolManager _poolManager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        Initialize();
    }

    private void Initialize()
    {
        // Damage 생성
        _poolManager.CreatePool("데미지", _prefab, _maxCount, _canvasParent);
    }

    #region 프로퍼티

    public FloatingText Data { get { return data; } }

    #endregion
}
