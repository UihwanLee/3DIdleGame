using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    Atk,
    Exp,
    MaxExp,
    Gold,
    Hp,
    MaxHp,
}

public class ConditionHandler : MonoBehaviour
{
    // Player 스탯 정보 등 모두 관리한다.

    protected Condition _maxExp;
    protected Condition _maxHp;

    protected Condition _atk;
    protected Condition _exp;
    protected Condition _gold;
    protected Condition _hp;
    protected Condition _lv;

    [SerializeField] protected PlayerConditionUI _conditionUI;

    protected FloatingTextPoolManager _floatingTextPoolManager;

    [SerializeField] protected ConditionInfoSO _conditionInfo;

    protected Transform _damageTransform;  // 데미지를 표시할 Transform

    public bool IsDead { get; set; }

    protected virtual void Awake()
    {
        _atk = new Condition(_conditionInfo.BaseAtk);
        _maxExp = new Condition(_conditionInfo.BaseMaxExp);
        _maxHp = new Condition(_conditionInfo.BaseMaxHp);

        _exp = new Condition(0f);
        _hp = new Condition(_maxHp.Value);
        _lv = new Condition(1f);
        _gold = new Condition(0f);

        if(this.transform.gameObject.tag == "Player" && _conditionInfo != null)
        {
            _conditionUI.UpdateExpBar(0f);
            _conditionUI.UpdateGold(_gold.Value.ToString("F0"));
            _conditionUI.UpdateLevel(_lv.Value.ToString());
        }
    }

    protected virtual void Start()
    {
        IsDead = false;
        _floatingTextPoolManager = FloatingTextPoolManager.Instance;
    }

    public void SetDamageTransform(Transform target)
    {
        // 데미지 표시 위치 초기화
        this._damageTransform = target;
    }

    public void AddCondition(ConditionType tpye, float amount)
    {
        switch(tpye)
        {
            case ConditionType.Atk:
                _atk.AddValue(amount);
                break;
            case ConditionType.Exp:
                _exp.AddValue(amount);
                {
                    if(_exp.Value >= _maxExp.Value)
                    {
                        // 경험치 통 늘리기
                        _maxExp.SetValue(_maxExp.Value * _conditionInfo.ExpIncreaseScaling);

                        // 경험치 초기화
                        _exp.SetValue(0f);

                        // 기본 공격력 증가
                        _atk.SetValue(_atk.Value * _conditionInfo.AtkIncreasScaling);

                        // 레벨 올리기
                        _lv.AddValue(1f);
                    }

                    // UI 전달
                    string lv = _lv.Value.ToString("F0");
                    _conditionUI.UpdateLevel(lv);
                    _conditionUI.UpdateExpBar(_exp.Value / _maxExp.Value);
                }
                break;
            case ConditionType.Gold:
                _gold.AddValue(amount);
                // UI 전달
                string gold = _gold.Value.ToString("F0");
                _conditionUI.UpdateGold(gold);
                break;
            case ConditionType.Hp:
                _hp.AddValue(amount);
                {
                    if (_hp.Value >= _maxHp.Value)
                    {
                        // 체력 통 늘리기
                        _maxHp.SetValue(_maxHp.Value * _conditionInfo.HpIncreaseScaling);

                        // 체력 만땅으로 늘리기
                        _hp.SetValue(_maxHp.Value);
                    }
                }
                break;
            default:
                break;
        }
    }

    public void SubCondition(ConditionType tpye, float amount)
    {
        switch (tpye)
        {
            case ConditionType.Atk:
                _atk.SubVale(amount);
                break;
            case ConditionType.Exp:
                _exp.SubVale(amount);
                break;
            case ConditionType.MaxExp:
                _maxExp.SubVale(amount);
                break;
            case ConditionType.Gold:
                _gold.SubVale(amount);
                break;
            case ConditionType.Hp:
                _hp.SetValue(Mathf.Max(0f, _hp.Value - amount));
                {
                    // damageTransform가 초기화되어 있을 경우 사용
                    Transform damageTransform = (this._damageTransform != null) ? this._damageTransform : this.transform;

                    // Damage Text 표시
                    _floatingTextPoolManager.SpawnText(TextType.Damage, amount.ToString(), damageTransform);

                    if (_hp.Value == 0)
                    {
                        IsDead = true;
                        OnDie();
                    }
                }
                break;
            case ConditionType.MaxHp:
                _maxHp.SubVale(amount);
                break;
            default:
                break;
        }
    }

    public virtual void OnDie()
    {
        
    }

    public float Atk { get { return _atk.Value; } }
    public float Hp { get { return _hp.Value; } }
    public float MaxHp { get { return _maxHp.Value; } }
}
