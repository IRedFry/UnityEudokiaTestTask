using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public enum UpgradeType { DamageUp, ReloadUp};

    [SerializeField]
    private int moneyCount;
    private int moneyForUpgrade;

    public float damageIncreaseCoefficient = 2.0f;
    public float reloadLowerCoefficient = 1.5f;
    private void Start()
    {
        moneyCount = 0;
        moneyForUpgrade = 3;
        Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateMoneyToUpgrade, moneyForUpgrade);
        AddMoney(0);
    }

    private void OnEnable()
    {
        Events.EnemyDeath += OnEnemyDeath;
    }

    private void OnDisable()
    {
        Events.EnemyDeath -= OnEnemyDeath;
    }

    private void OnEnemyDeath(EnemyBase eBase)
    {
        AddMoney(eBase.moneyToGet);
    }

    public void AddMoney(int amount)
    {
        moneyCount += amount;
        Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateMoneyCounter, moneyCount);
    }

    public void UpgradeDamage()
    {
        if (moneyCount >= moneyForUpgrade)
        {
            moneyCount -= moneyForUpgrade;
            moneyForUpgrade += moneyForUpgrade / 2;
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateMoneyCounter, moneyCount);
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateMoneyToUpgrade, moneyForUpgrade);
            Events.Upgrade?.Invoke(UpgradeType.DamageUp, damageIncreaseCoefficient);
        }
    }

    public void UpgradeReload()
    {
        if (moneyCount >= moneyForUpgrade)
        {
            moneyCount -= moneyForUpgrade;
            moneyForUpgrade += moneyForUpgrade / 2;
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateMoneyCounter, moneyCount);
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateMoneyToUpgrade, moneyForUpgrade);
            Events.Upgrade?.Invoke(UpgradeType.ReloadUp, reloadLowerCoefficient);
        }
    }


}
