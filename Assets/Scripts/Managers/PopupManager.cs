using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupWaitData
{
    public Define.PopupType popupType;
    public PopupArg arg;
}

public class PopupArg
{
    public static PopupArg Empty = new PopupArg();
}

public class PopupManager
{
    public Transform Root
    {
        get
        {
            if (_root == null)
            {
                GameObject go = new GameObject("@Popup_Root");
                _root = go.transform;
                Object.DontDestroyOnLoad(go);
            }

            return _root;
        }
    }

    private Transform _root;
    private List<PopupBase> _activePopups = new List<PopupBase>();

    private List<PopupWaitData> _waitActivePopups = new List<PopupWaitData>();
    private List<PopupBase> _waitClosePopups = new List<PopupBase>();
    private List<int> _focusStack = new List<int>();

    private int _order = 10;

    public void OnUpdate()
    {
        foreach (PopupWaitData waitData in _waitActivePopups)
        {
            ShowWaitPopupBox(waitData);
        }

        _waitActivePopups.Clear();

        foreach (PopupBase popup in _waitClosePopups)
        {
            CloseWaitPopupBox(popup);
        }

        _waitActivePopups.Clear();
    }

    public void ShowPopupBox(Define.PopupType popupType, PopupArg arg)
    {
        PopupWaitData waitData = new PopupWaitData()
        {
            popupType = popupType,
            arg = arg,
        };

        _waitActivePopups.Add(waitData);
    }

    public bool IsFocuse(int id)
    {
        if (id < 0 || _focusStack.Count == 0)
            return false;

        return _focusStack.Last() == id;
    }

    public void AddFocuse(int id)
    {
        if (_focusStack.Contains(id) == true)
            return;

        _focusStack.Add(id);
    }

    public void RemoveFocuse(int id)
    {
        _focusStack.RemoveAll(item => item == id);
    }

    private void ShowWaitPopupBox(PopupWaitData waitData)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefab/UI/Popup/{waitData.popupType}");
        if (prefab == null)
        {
            Debug.Log($"Not found prefab {waitData.popupType}");
            return;
        }

        GameObject go = Object.Instantiate(prefab, Root);
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.sortingOrder = _order++;

        PopupBase popup = go.GetComponent<PopupBase>();
        popup.PopupType = waitData.popupType;
        _activePopups.Add(popup);
    }

    public void ClosePopupBox(GameObject popup)
    {
        ClosePopupBox(popup.GetComponent<PopupBase>());
    }

    private void ClosePopupBox(PopupBase popup)
    {
        if (popup != null)
        {
            popup.OnClosePopupBox();
            _order--;
            _waitClosePopups.Add(popup);
        }
    }

    private void CloseWaitPopupBox(PopupBase popup)
    {
        _activePopups.Remove(popup);
        popup.gameObject.SetActive(false);
        Object.Destroy(popup.gameObject);
        popup = null;
    }
}