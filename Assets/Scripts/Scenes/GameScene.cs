using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        Managers.Object.SpawnPlayer();
    }

    private void Update()
    {
        Managers.Stage.OnUpdate();
    }

    public void ChangeStage()
    {
        Managers.Object.Player.Init();
    }
}
