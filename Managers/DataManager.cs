using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    #region CONST_VALUES
    private const int MIN_AMOUNT = 0;
    private const int MAX_AMOUNT = 999999;
    private const int COMMON_PRICE = 0;
    private const int UNCOMMON_PRICE = 2;
    private const int RARE_PRICE = 10;
    private const int LEGENDARY_PRICE = 20;
    private const int EXOTIC_PRICE = 30;
    #endregion

    private int _bestScore;
    private int _money;
    public CommonEnums.ePlaneType PlaneType;
    public Dictionary<CommonEnums.ePlaneType, int> PlanePrice = new Dictionary<CommonEnums.ePlaneType, int>();

    public int BestScore
    {
        get { return _bestScore; }
        set { Mathf.Clamp(value, MIN_AMOUNT, MAX_AMOUNT); }
    }

    public int Money
    {
        get { return _money; }
        set { Mathf.Clamp(value, MIN_AMOUNT, MAX_AMOUNT); }
    }

    public void Init()
    {
         InitDict();
        _bestScore = MIN_AMOUNT;
        _money = MIN_AMOUNT;
        PlaneType = CommonEnums.ePlaneType.CommonPlane;
    }

    public void InitDict()
    {
        PlanePrice.Add(CommonEnums.ePlaneType.CommonPlane, COMMON_PRICE);
        PlanePrice.Add(CommonEnums.ePlaneType.UnCommonPlane, UNCOMMON_PRICE);
        PlanePrice.Add(CommonEnums.ePlaneType.RarePlane, RARE_PRICE);
        PlanePrice.Add(CommonEnums.ePlaneType.LegendaryPlane, LEGENDARY_PRICE);
        PlanePrice.Add(CommonEnums.ePlaneType.ExoticPlane, EXOTIC_PRICE);
    }
}
