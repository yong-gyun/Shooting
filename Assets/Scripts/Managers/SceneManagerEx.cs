using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get; set; }
    public Define.SceneType CurrentSceneType { get; set; }

    public void LoadScene(Define.SceneType sceneType)
    {
        SceneManager.LoadScene(GetSceneName(sceneType));
    }

    public string GetSceneName(Define.SceneType sceneType)
    {
        return sceneType.ToString();
    }
}
