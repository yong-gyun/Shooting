using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : BaseStatus
{
    public int Score { get; private set; }

    [SerializeField] private List<StatData> _originStatDatas = new List<StatData>();
    [SerializeField] private List<int> _scores = new List<int>();

    public void SetInfo(int stage)
    {
        int idx = stage - 1;
        _currentStat.CopyData(_originStatDatas[idx]);
        Score = _scores[idx];
    }
}
