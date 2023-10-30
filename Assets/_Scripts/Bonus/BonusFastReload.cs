using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFastReload : BonusBase
{
    protected override void OnBonusUse()
    {
        Events.UseBonus(BonusSpawner.BonusType.FastReload);
    }
}
