using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;
    public event Action OnDie;

    private Transform _damageTransform;  // 데미지를 표시할 Transform

    public bool IsDead { get; set; }

    private FloatingTextPoolManager _floatingTextPoolManager;


    private void Start()
    {
        IsDead = false;
        _health = _maxHealth;
        _floatingTextPoolManager = FloatingTextPoolManager.Instance;
    }

    public void SetDamageTransform(Transform target)
    {
        // 데미지 표시 위치 초기화
        this._damageTransform = target;
    }

    public void TakeDamage(int damage)
    {
        if (_health == 0) return;

        _health = Mathf.Max(_health - damage, 0);

        // damageTransform가 초기화되어 있을 경우 사용
        Transform damageTransform = (this._damageTransform != null) ? this._damageTransform : this.transform;

        // Damage Text 표시
        _floatingTextPoolManager.SpawnText(TextType.Damage, damage.ToString(), damageTransform);

        if (_health == 0)
        {
            IsDead = true;
            OnDie?.Invoke();
        }
    }

    #region 프로퍼티

    public int MaxHealth { get { return _maxHealth; } }
    public int CurHealth { get { return _health; } }

    #endregion
}
