using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitApplicationButton;
    [SerializeField] private Button _loadMenuButton;
    [SerializeField] private Player _player;

    private CanvasGroup _gameOverGroup;

    private void Awake()
    {
        _gameOverGroup = GetComponent<CanvasGroup>();
        _gameOverGroup.alpha = 0;
    }

    private void Update()
    {
        if (Time.time >= 5)
        {
            Debug.Log(Time.time);
            _gameOverGroup.alpha = 1;
            Time.timeScale = 0;
        }
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _loadMenuButton.onClick.AddListener(OnLoadMenuButtonClick);
        _exitApplicationButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _loadMenuButton.onClick.RemoveListener(OnLoadMenuButtonClick);
        _exitApplicationButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnLostLevel()
    {
        
    }

    private void OnAllCoinsCollected()
    {
        _gameOverGroup.alpha = 1;
        Time.timeScale = 0;
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(StaticSceneNames.LevelSceneName);
    }

    private void OnLoadMenuButtonClick()
    {
        SceneManager.LoadScene(StaticSceneNames.MenuSceneName);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}