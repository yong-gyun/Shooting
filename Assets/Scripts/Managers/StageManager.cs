using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public float PlayTime { get; set; }
    public int Score { get; set; }
    public bool IsStageStarted { get; set; } = false;
    public int CurrentStage { get; private set; } = 0;
    public int LastStage = 2;
    public Action<int> OnStageStartEvent = null;
    public Action<bool> OnStageEndEvent = null;

    public void StartStage(int stage)
    {
        CurrentStage = stage;
        IsStageStarted = true;
        OnStageStartEvent?.Invoke(stage);
    }

    public void EndStage(bool stageClear)
    {
        if (stageClear == true)
        {

        }
        else
        {

        }

        if (CurrentStage == LastStage)
        {
            Clear();
        }
    }

    public void OnUpdate()
    {

    }

    public void Clear()
    {
        CurrentStage = 0;
        IsStageStarted = false;
        OnStageStartEvent = null;
    }
}
