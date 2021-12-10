using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ButtonObj : UIButtonMessage
{
    #region SERIALIZEFIELD
    [SerializeField]
    private int _id;
    #endregion

    public CommonEnums.ePlaneType _planeType;
    private ButtonList _buttons;

    public void Init(ButtonList _list)
    {
        _buttons = _list;
        target = gameObject;
        functionName = StringDefines.Strings.ButtonMsg;
    }

    private void OnButtonMsg()
    {
        if(_buttons != null)
        {
            if(_buttons._target == null)
            {
                _buttons.gameObject.SendMessage(StringDefines.Strings.ButtonMsg, _planeType);
            }
            else
            {
                _buttons._target.SendMessage(_buttons._funcToCall, _planeType);
            }
        }
    }
}
