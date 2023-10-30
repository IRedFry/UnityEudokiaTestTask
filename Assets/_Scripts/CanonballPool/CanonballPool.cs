using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CanonballPool : MonoBehaviour
{
    public static CanonballPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private CanonballBase canonballBase;
    void Awake()
    {
        SharedInstance = this;
        canonballBase = ScriptableObject.CreateInstance<CanonballBase>();
        canonballBase.IncreaseDamageByAmount(20);
    }

    private void OnEnable()
    {
        Events.Upgrade += OnUpgrade;
    }

    private void OnDisable()
    {
        Events.Upgrade -= OnUpgrade;
    }

    public void OnUpgrade(PlayerUpgrade.UpgradeType type, float data)
    {
        if (type == PlayerUpgrade.UpgradeType.DamageUp)
        {
            IncreaseDamageByCoefficient(data);
        }
    }

    public void IncreaseDamageByAmount(int newDamage)
    {
        canonballBase.IncreaseDamageByAmount(newDamage);
    }

    public void IncreaseDamageByCoefficient(float coef)
    {
        canonballBase.IncreaseDamageByCoefficient(coef);
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.GetComponent<Canonball>().SetCanonballBase(canonballBase);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
