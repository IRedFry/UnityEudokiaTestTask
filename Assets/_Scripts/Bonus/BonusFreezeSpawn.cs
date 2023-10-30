using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFreezeSpawn : BonusBase
{
    protected override void OnBonusUse()
    {
        Events.UseBonus?.Invoke(BonusSpawner.BonusType.FreezeSpawn);
    }
}
