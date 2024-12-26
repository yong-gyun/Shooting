using System;
using UnityEngine;

[Serializable]
public class StatData
{
    public float maxHp;
    public float hp;
    public float attack;
    public float moveSpeed;
    public float attackSpeed;

    public void CopyData(StatData copyData)
    {
        maxHp = copyData.maxHp;
        hp = copyData.hp;
        attack = copyData.attack;
        moveSpeed = copyData.moveSpeed;
        attackSpeed = copyData.attackSpeed;
    }
}

public abstract class BaseStatus : MonoBehaviour
{
    #region 스탯 프로퍼티
    public float Hp
    {
        get
        {
            return _currentStat.hp;
        }
        set
        {
            float calculator = Mathf.Min(value, _currentStat.hp);
            _currentStat.hp = calculator;
        }
    }

    public float AttackSpeed { get { return _currentStat.attackSpeed; } set { _currentStat.attackSpeed = value; } }

    public float Attack { get { return _currentStat.attack; } set { _currentStat.attack = value; } }

    public float MoveSpeed { get { return _currentStat.moveSpeed; } set { _currentStat.moveSpeed = value; } }
    #endregion

    public bool Damageable { get; set; }
    public Action OnDeadEvent = null;
    public Action<float> OnDamagedEvent = null;

    [SerializeField] protected StatData _orignStatData;       //원본 스탯 데이터
    [SerializeField] protected StatData _currentStat;         //현재 사용중인 복사용 데이터

    public void OnDamaged(float damage)
    {
        if (Hp <= 0 || Damageable == false)
            return;
        
        if (Hp <= 0)
        {
            OnDead();
            return;
        }

        Hp -= damage;
        OnDamagedEvent?.Invoke(damage);
    }

    public void OnDead()
    {
        Damageable = false;
        OnDeadEvent?.Invoke();
    }
}