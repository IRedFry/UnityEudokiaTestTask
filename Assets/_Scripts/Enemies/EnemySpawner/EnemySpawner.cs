using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner SharedInstance;
    [SerializeField]
    private List<GameObject> pooledObjects;
    private EnemyBase enemyBase;
    [SerializeField]
    private bool isSpawnFreeze;
    public int waveCount { get; private set; }

    public int startMaxHealth = 20;
    public int maxHealthStep = 20;
    public float spawnRate = 5.0f;
    public float upgradeRate = 10.0f;
    public GameObject objectToPool;
    public int amountToPool;
    public float spawnRadius = 10.0f;


    private void Awake()
    {
        waveCount = 1;
        SharedInstance = this;
        isSpawnFreeze = false;
    }

    private void OnEnable()
    {
        Events.UseBonus += OnUseBonus;
    }

    private void OnDisable()
    {
        Events.UseBonus -= OnUseBonus;
    }

    void OnUseBonus(BonusSpawner.BonusType type)
    {
        if (type == BonusSpawner.BonusType.FreezeSpawn)
        {
            StartCoroutine(FreezeSpawn());
        }
    }

    void Start()
    {
        enemyBase = ScriptableObject.CreateInstance<EnemyBase>();
        enemyBase.SetMaxHealth(startMaxHealth);
        enemyBase.SetMoneyToGet(1);

        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.GetComponent<Enemy>().SetEnemyBase(enemyBase);
            pooledObjects.Add(tmp);
        }

        StartCoroutine(SpawnEnemy());
        StartCoroutine(UpgradeEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (GameController.SharedInstance.isGameActive)
        {
            if (!isSpawnFreeze)
            {
                GameObject newEnemy = GetPooledObject();
                if (newEnemy != null)
                {
                    Vector3 enemyPosition = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), -0.5f, Random.Range(-spawnRadius, spawnRadius));
                    newEnemy.transform.position = enemyPosition;
                    newEnemy.GetComponent<Enemy>().Restart();
                    newEnemy.SetActive(true);
                }
                else
                {
                    Events.GameOver?.Invoke();
                    yield return null;
                }

                yield return new WaitForSeconds(spawnRate);
            }
            else
                yield return null;
        }
    }
    private IEnumerator UpgradeEnemy()
    {
        float timer = upgradeRate;
        while (GameController.SharedInstance.isGameActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                waveCount++;
                upgradeRate *= 1.2f;
                Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateWaveCounter, waveCount);
                enemyBase.SetMaxHealth(enemyBase.maxHealth + maxHealthStep);
                if (waveCount % 2 == 0)
                    enemyBase.SetMoneyToGet(waveCount / 2 + 1);
                spawnRate *= 0.5f;
                timer = upgradeRate;
            }
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateTimeToTheNextWaveLeft, (int)timer);
            yield return null;
        }
    }

    private IEnumerator FreezeSpawn()
    {
        isSpawnFreeze = true;
        yield return new WaitForSeconds(5);
        isSpawnFreeze = false;
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
