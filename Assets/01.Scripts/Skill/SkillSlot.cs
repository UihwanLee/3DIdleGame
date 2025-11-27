using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SkillSO _data;

    [Header("Component")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _remainTimeTxt;
    [SerializeField] private Image _coolTimeBG;

    private float _skillCoolTime;
    private bool _canExcute;
    private ISkillCaster _skillCaster;

    private Coroutine _skillCoolTimeCoroutine;

    private void Reset()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        _icon = transform.FindChild<Image>("SkillIcon");
        _title = transform.FindChild<TextMeshProUGUI>("SkillTitle");
        _remainTimeTxt = transform.FindChild<TextMeshProUGUI>("SkillRemainTime");
        _coolTimeBG = transform.FindChild<Image>("SkillCoolTimeBG");
    }

    public void Initialize()
    {
        // Component 초기화가 안되어 있을 경우에는 초기화
        if (_icon == null || _title == null || _remainTimeTxt == null || _coolTimeBG == null)
            InitializeComponent();

        ResetComponent();

        SetButtonClickEvent();
    }

    private void SetButtonClickEvent()
    {
        if(this.transform.TryGetComponent(out Button button))
        {
            button.onClick.AddListener(OnClickSkill);
        }
    }

    public void SetSkillCaster(ISkillCaster skillCaster)
    {
        this._skillCaster = skillCaster;
    }


    public void ResetComponent()
    {
        // coolTimeBg 리셋
        _coolTimeBG.fillAmount = 1f;
        _coolTimeBG.gameObject.SetActive(false);

        // remianTime 리셋
        _remainTimeTxt.text = string.Empty;

        // 동작 가능 여부 초기화
        _canExcute = true;

        // 코루틴 중지
        if (_skillCoolTimeCoroutine != null) StopCoroutine(_skillCoolTimeCoroutine);
    }

    public void SetSkill(SkillSO skill)
    {
        this._data = skill;

        _icon.sprite = skill.SkillIcon;
        _title.text = skill.SkillName;

        _skillCoolTime = skill.SkillCoolTime;

        ResetComponent();
    }

    public void OnClickSkill()
    {
        // 스킬을 눌렀을 때 실행 여부
        if (!TryExcuteSkill()) return;

        // 스킬 실행
        _data.Excute(_skillCaster);

        // 실행 가능 여부 변경
        _canExcute = false;

        // 스킬 실행 후 스킬 쿨타임 코루틴 실행
        StartCoroutine(SkillCoolTimeCoroutine());
    }

    private bool TryExcuteSkill()
    {
        if (!_canExcute) { Debug.Log("[스킬 실행 불가]: canExcute false임"); return false; }

        if (_skillCaster == null) { Debug.Log("[스킬 실행 불가]: SkillCaster가 없음!"); return false; }

        if(_data == null) { Debug.Log("[스킬 실행 불가]: skillSO 초기화 안됨"); return false; }

        return true;
    }

    private IEnumerator SkillCoolTimeCoroutine()
    {
        float curDuration = 0f;

        _coolTimeBG.gameObject.SetActive(true);

        while (curDuration < _skillCoolTime)
        {
            curDuration += Time.deltaTime;

            float percentage = _skillCoolTime - curDuration;

            // _coolTimeBG amount 조정 : 1 -> 0
            _coolTimeBG.fillAmount = (percentage / _skillCoolTime);

            // _remainTimeTxt 조정
            float remianTime = _skillCoolTime - curDuration;
            _remainTimeTxt.text = remianTime.ToString("F1");

            yield return null;
        }

        ResetComponent();
    }
}