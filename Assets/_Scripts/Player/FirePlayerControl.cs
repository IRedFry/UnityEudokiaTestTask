using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirePlayerControl : MonoBehaviour
{
    public float forceAmount = 1f;
    public ForceMode forceMode;
    public AudioSource audioSource;

    [SerializeField]
    private GameObject canonModel;
    [SerializeField]
    private Vector3 canonballSpawnPointOffset;
    [SerializeField]
    private Vector3 lookVector;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private float minimumReloadTime;

    private bool isReloading;

    private void Start()
    {
        isReloading = false;
        Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateReloadTimeValue, (int)reloadTime);
    }

    private void OnEnable()
    {
        Events.UseBonus += OnUseBonus;
        Events.Upgrade += OnUpgrade;
    }

    private void OnDisable()
    {
        Events.UseBonus -= OnUseBonus;
        Events.Upgrade -= OnUpgrade;
    }

    private void OnUseBonus(BonusSpawner.BonusType type)
    {
        if (type == BonusSpawner.BonusType.FastReload)
        {
            StartCoroutine(UseFastReload());
        }
    }

    private void OnUpgrade(PlayerUpgrade.UpgradeType type, float data)
    {
        if (type == PlayerUpgrade.UpgradeType.ReloadUp)
        {
            reloadTime /= data;
            minimumReloadTime /= data;
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateReloadTimeValue, (int)reloadTime);
        }
    }

    void Update()
    {
        if (GameController.SharedInstance.isGameActive)
        {
            LookAtMouseDirection();
            FireCanonball();
        }
    }

    void LookAtMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(transform.position, hit.point);
            canonModel.transform.LookAt(hit.point);
            lookVector = hit.point - transform.position;
        }
    }
    
    void FireCanonball()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            GameObject newCanonball = CanonballPool.SharedInstance.GetPooledObject();
            if (newCanonball != null)
            {
                audioSource.Play();
                newCanonball.transform.position = (canonModel.transform.forward * 1.75f) + canonballSpawnPointOffset;
                newCanonball.SetActive(true);
                newCanonball.GetComponent<Rigidbody>().AddForce(lookVector * forceAmount, forceMode);
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        float timer = 0.0f;
        while (timer <= reloadTime)
        {
            timer += Time.deltaTime;
            yield return null;
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateReload, (int)((timer / reloadTime) * 10));
        }
        isReloading = false;

    }

    private IEnumerator UseFastReload()
    {
        if (reloadTime >= minimumReloadTime)
        {
            reloadTime /= 2.0f;
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateReloadTimeValue, (int)reloadTime);
            yield return new WaitForSeconds(5);
            reloadTime *= 2.0f;
            Events.UpdateUI?.Invoke(UIController.UIEventType.UpdateReloadTimeValue, (int)reloadTime);
        }
    }

}
