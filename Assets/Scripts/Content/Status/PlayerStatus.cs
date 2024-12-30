using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : BaseStatus
{
    public float MaxFuel { get { return _maxFuel; } }

    public float Fuel
    {
        get
        {
            return _fuel;
        }
        set
        {
            if (value >= MaxFuel)
            {
                _fuel = MaxFuel;
            }
            else
            {
                float calculator = Mathf.Max(value, 0f);
                _fuel = calculator;
            }
        }
    }

    public int FireIndex { get; set; } = 2;
    public int UpgradeAttackCount { get; set; }

    [SerializeField] private float _maxFuel;
    [SerializeField] private float _fuel;
    
    public void SetFullCondition()
    {
        Hp = MaxHp;
        Fuel = MaxFuel;
    }

    public void SetInfo()
    {
        _currentStat.CopyData(_orignStatData);
        _fuel = _maxFuel;
        FireIndex = 2;
    }
}
