using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerStatData : StatData
{
    public float maxFuel;
    public float fuel;

    public void CopyData(PlayerStatData copyData)
    {
        hp = copyData.hp;
        fuel = copyData.fuel;
        attack = copyData.attack;
        moveSpeed = copyData.moveSpeed;
        attackSpeed = copyData.attackSpeed;
        defense = copyData.defense;
    }
}


public class PlayerStatus : BaseStatus
{
    public float MaxFuel { get { return _currentStat.maxFuel; } }

    public float Fuel
    {
        get
        {
            return _currentStat.fuel;
        }
        set
        {
            float calculator = Mathf.Min(value, _currentStat.fuel);
            _currentStat.fuel = calculator;
        }
    }

    [SerializeField] private PlayerStatData _originStatData;
    [SerializeField] private PlayerStatData _currentStat;

    public void SetInfo()
    {
        _currentStat.CopyData(_originStatData);
    }
}
