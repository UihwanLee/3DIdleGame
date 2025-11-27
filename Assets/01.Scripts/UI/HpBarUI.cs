using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    private Image _hpBar;
    private ConditionHandler _condition;

    private void Start()
    {
        _hpBar = GetComponent<Image>();
        _condition = GetComponentInParent<ConditionHandler>();
    }

    private void Update()
    {
        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        _condition = GetComponentInParent<ConditionHandler>();
        if (_condition == null) Debug.Log("Codition이 없음");

        // HpBar 업데이트
        _hpBar.fillAmount = (_condition.Hp /_condition.MaxHp);
    }
}
