using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public enum UIEventType { UpdateWaveCounter, UpdateTimeToTheNextWaveLeft, UpdateMoneyCounter, UpdateMoneyToUpgrade, UpdateDamageValue, UpdateReload, UpdateReloadTimeValue };

    [SerializeField]
    private TextMeshProUGUI waveCounter;
    [SerializeField]
    private TextMeshProUGUI waveTimeLeft;
    [SerializeField]
    private TextMeshProUGUI moneyCounter;
    [SerializeField]
    private TextMeshProUGUI moneyToUpgrade;
    [SerializeField]
    private TextMeshProUGUI damageValue;
    [SerializeField]
    private TextMeshProUGUI reloadTimeValue;
    [SerializeField]
    private TextMeshProUGUI deathText;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Image reloadImage;
    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    private void OnEnable()
    {
        Events.UpdateUI += OnUpdateUI;
        Events.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        Events.UpdateUI -= OnUpdateUI;
        Events.GameOver -= OnGameOver;
    }

    private void OnUpdateUI(UIEventType type, int data)
    {
        switch(type)
        {
            case UIEventType.UpdateWaveCounter:
                UpdateWaveCounter(data);
                break;
            case UIEventType.UpdateTimeToTheNextWaveLeft:
                UpdateTimeToTheNextWaveLeft(data);
                break;
            case UIEventType.UpdateMoneyCounter:
                UpdateMoneyCounter(data);
                break;
            case UIEventType.UpdateMoneyToUpgrade:
                UpdateMoneyToUpgrade(data);
                break;
            case UIEventType.UpdateDamageValue:
                UpdateDamageValue(data);
                break;
            case UIEventType.UpdateReload:
                UpdateReload(data);
                break;
            case UIEventType.UpdateReloadTimeValue:
                UpdateReloadTimeValue(data);
                break;
        }
    }

    private void OnGameOver()
    {
        ActivateGameoverScreen();
    }

    private void UpdateWaveCounter(int waveCount)
    {
        waveCounter.text = "Wave: " + waveCount.ToString();
        deathText.text = "You died!\nYou have reached wave " + waveCount.ToString() + "!";
    }

    private void UpdateTimeToTheNextWaveLeft(int timeLeft)
    {
        waveTimeLeft.text = "Time to the next wave: " + timeLeft.ToString() + "s";
    }

    private void UpdateMoneyCounter(int moneyCount)
    {
        moneyCounter.text = "x " + moneyCount.ToString();
    }

    private void UpdateMoneyToUpgrade(int moneyToUp)
    {
        moneyToUpgrade.text = "Next: " + moneyToUp.ToString();
    }

    private void UpdateDamageValue(int damage)
    {
        damageValue.text = "Damage: " + damage.ToString();
    }
    private void UpdateReloadTimeValue(int reloadTime)
    {
        reloadTimeValue.text = "Reload: " + reloadTime.ToString();
    }
    private void UpdateReload(int realoadValue)
    {
        reloadImage.fillAmount = realoadValue / 10.0f;
    }
    private void ActivateGameoverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
