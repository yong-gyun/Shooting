using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers Instance { get { Init(); return s_instance; } }
    private static Managers s_instance;

    private DataManager _data = new DataManager();
    private PopupManager _popup = new PopupManager();
    private ObjectManager _object = new ObjectManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private StageManager _stage = new StageManager();

    public static DataManager Data { get { return Instance._data; } }
    public static PopupManager Popup { get { return Instance._popup; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static StageManager Stage { get { return Instance._stage; } }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
                
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._data.Init();
        }
    }

    private void Update()
    {
        Popup.OnUpdate();
    }

    public static void Clear()
    {
        Data.Clear();
    }

    private void OnApplicationQuit()
    {
        Data.SaveDatas();
    }
}