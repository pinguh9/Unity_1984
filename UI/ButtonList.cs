using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonList : MonoBehaviour
{
    [Serializable]
    public class Button
    {
        public ButtonObj _button;
    }

    public List<Button> _buttonList = new List<Button>();
    public GameObject _target;
    public string _funcToCall;

    private void Start()
    {
        foreach(Button button in _buttonList)
        {
           if(button._button != null)
            {
                button._button.Init(this);
            }
        }
    }

    public void InitTarget(GameObject target, string funcToCall)
    {
        this._target = target;
        this._funcToCall = funcToCall;
    }

}
