using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    private bool _isArrive;

    protected override void Start()
    {
        base.Start();
        TargetTag = "Player";
    }

    public virtual void SetInfo(Transform spawnPos)
    {

    }
}
