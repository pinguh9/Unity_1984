using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemBase
{
    public override void Get()
    {
        base.Get();
        InGameManager.Game.Coin += AMOUNT_PER_ONCE;
    }
}
