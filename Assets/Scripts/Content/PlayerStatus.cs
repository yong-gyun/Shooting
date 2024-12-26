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
            float calculator = Mathf.Min(value, _fuel);
            _fuel = calculator;
        }
    }

    [SerializeField] private float _maxFuel;
    [SerializeField] private float _fuel;

    public void SetInfo()
    {
        _currentStat.CopyData(_orignStatData);
        _fuel = _maxFuel;
    }
}
