using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private Image _expBar;
    [SerializeField] private TextMeshProUGUI _levelText;

    public void UpdateGold(string gold)
    {
        _goldText.text = gold;
    }

    public void UpdateLevel(string level)
    {
        _levelText.text = $"{level}레벨";  
    }

    public void UpdateExpBar(float percentage)
    {
        _expBar.fillAmount = percentage;
    }
}
