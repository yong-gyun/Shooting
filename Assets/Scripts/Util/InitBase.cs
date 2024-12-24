using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBase : MonoBehaviour
{
    private bool _init;

    public virtual bool Init()
    {
        if (_init == true)
            return false;

        return true;
    }

    public virtual bool Clear()
    {
        if (_init == false)
            return false;

        return true;
    }
}