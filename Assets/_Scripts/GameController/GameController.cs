using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController SharedInstance;
    public bool isGameActive { get; private set; }
    void Awake()
    {
        isGameActive = true;
        SharedInstance = this;
    }

    private void OnEnable()
    {
        Events.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        Events.GameOver -= OnGameOver;
    }

    public void OnGameOver()
    {
        isGameActive = false;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
