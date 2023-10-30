using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballBase : ScriptableObject
{
    public int damage { get; private set; }

    public void IncreaseDamageByAmount(int newDamage)
    {
        damage += newDamage;
        Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateDamageValue, damage);
    }

    public void IncreaseDamageByCoefficient(float coef)
    {
        damage = (int)(damage * coef);
        Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateDamageValue, damage);
    }
}
