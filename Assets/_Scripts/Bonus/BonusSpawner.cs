using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public enum BonusType { FreezeSpawn = 0, FastReload };

    [SerializeField]
    private List<GameObject> bonuses;

    public float spawnRate = 5.0f;
    public float spawnRadius = 10.0f;
    private void Start()
    {
        StartCoroutine(SpawnBonus());
    }

    private IEnumerator SpawnBonus()
    {
        while (GameController.SharedInstance.isGameActive)
        {
            GameObject newBonus = Instantiate(bonuses[Random.Range(0, bonuses.Count)]);
            Vector3 bonusPosition = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0.1f, Random.Range(-spawnRadius, spawnRadius));
            newBonus.transform.position = bonusPosition;
            yield return new WaitForSeconds(spawnRate);
        }
    }

}
