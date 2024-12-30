using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public float PlayTime { get; set; }
    public int Score { get; set; }
    public bool IsStageStarted { get; set; } = false;
    public int CurrentStage { get; private set; } = 0;

    public void StartStage(int stage)
    {
        CurrentStage = stage;
        IsStageStarted = true;
    }

    public void OnUpdate()
    {

    }

    public void Clear()
    {
        CurrentStage = 0;
        IsStageStarted = false;
    }
}
