using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType;

    private void Awake()
    {
        Init();
    }

    public abstract void Init();
}
