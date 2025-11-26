using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    private Image _hpBar;
    private Health _health;

    private void Awake()
    {
        _hpBar = GetComponent<Image>();
        _health = GetComponentInParent<Health>();
    }

    private void Update()
    {
        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        // HpBar 업데이트
        int curHealth = _health.CurHealth;
        int maxHealth = _health.MaxHealth;  // MaxHealth도 런타임 중 자주 변경 될 수 있음

        _hpBar.fillAmount = (curHealth / (float)maxHealth);
    }
}
