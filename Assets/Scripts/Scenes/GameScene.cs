using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        Managers.Stage.StartStage(1);
        Managers.Object.SpawnPlayer();
    }

    private void Update()
    {
        Managers.Stage.OnUpdate();
    }
}
