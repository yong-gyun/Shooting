using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return Object.FindObjectOfType<BaseScene>(); } }
    public Define.SceneType CurrentSceneType 
    { 
        get 
        {
            if (_currentSceneType == Define.SceneType.None)
                return CurrentScene.SceneType;

            return _currentSceneType; 
        } 
        set 
        { 
            _currentSceneType = value; 
        } 
    }
    
    private Define.SceneType _currentSceneType;

    public void LoadScene(Define.SceneType sceneType)
    {
        CurrentSceneType = sceneType;
        SceneManager.LoadScene(GetSceneName(sceneType));
    }

    public string GetSceneName(Define.SceneType sceneType)
    {
        return sceneType.ToString();
    }
}
