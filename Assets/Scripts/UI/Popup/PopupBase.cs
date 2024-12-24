using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    public Define.PopupType PopupType;
    
    public virtual void Init(PopupArg arg)
    {

    }

    public virtual void OnClosePopupBox()
    {
        Managers.Popup.RemoveFocuse(gameObject.GetInstanceID());
    }

    private void OnEnable()
    {
        Managers.Popup.AddFocuse(gameObject.GetInstanceID());
    }

    public virtual void PressBackButton()
    {
        Managers.Popup.ClosePopupBox(gameObject);
    }
}
