using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ScriptableObject
{
    public int moneyToGet { get; private set; }
    public int maxHealth { get; private set; }
    public void SetMoneyToGet(int newMoney)
    {
        moneyToGet = newMoney;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }
}
