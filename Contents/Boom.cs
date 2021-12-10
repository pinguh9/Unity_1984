using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : ItemBase
{
    public override void Get()
    {
        base.Get();
        InGameManager.Game.BoomCount += AMOUNT_PER_ONCE;
    }
}
