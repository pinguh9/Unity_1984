using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : ItemBase
{
    public override void Get()
    {
        base.Get();
        GameManager.Object.Player.PowerLevel += AMOUNT_PER_ONCE;
    }
}
