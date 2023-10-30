using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static Action<BonusSpawner.BonusType> UseBonus;
    public static Action<UIController.UIEventType, int> UpdateUI;
    public static Action GameOver;
    public static Action<EnemyBase> EnemyDeath;
    public static Action<PlayerUpgrade.UpgradeType, float> Upgrade;
}
