using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressBackButton : MonoBehaviour
{
    private PopupBase _popup;

    private void Start()
    {
        _popup = GetComponent<PopupBase>();
    }

    private void Update()
    {
        if (Managers.Popup.IsFocuse(_popup.GetInstanceID()) == true)
        {
            _popup.PressBackButton();
        }
    }
}
