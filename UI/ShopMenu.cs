using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    #region SERIALIZEFIELD
    [SerializeField]
    private UILabel _infoText;
    #endregion
    private const float TEXT_SHOWING_TIME = 2.0f;

    private bool isBuyable (int coin) => GameManager.Data.Money >= coin;

    private void Start()
    {
        setInfoText();
        Transform _buttonGroup = transform.Find(StringDefines.Strings.ButtonGruop);
        ButtonList _buttonList = _buttonGroup.GetComponent<ButtonList>();
        _buttonList.InitTarget(gameObject, StringDefines.Strings.CallBackFunctionName);
    }
    public void OnClickBackButton()
    {
        GameManager.Scene.LoadScene(StringDefines.Strings.MainMenuScene);
    }

    public void OnMsgButtonList(CommonEnums.ePlaneType type)
    {
        Debug.Log(type);
        int price = GameManager.Data.PlanePrice[type];

        if (isBuyable(GameManager.Data.PlanePrice[type]))
        {
            GameManager.Data.PlaneType = type;
            GameManager.Data.Money -= price;
        }
        else
        {
            _infoText.text = StringDefines.Strings.NotEnoughMoney;
            Invoke("setInfoText", TEXT_SHOWING_TIME);
        }
    }

    public void setInfoText()
    {
        _infoText.text = $"{StringDefines.Strings.MoneyAmount}{GameManager.Data.Money}";
    }

}
