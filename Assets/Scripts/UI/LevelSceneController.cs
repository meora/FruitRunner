using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]

public class LevelSceneController : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _loadMenuButton;
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private CanvasGroup _levelCompletedGroup;

    [SerializeField] private Text _resultScorePointText;
    [SerializeField] private Text _endGameText;
    [SerializeField] private Text _resultFruitsText;
    [SerializeField] private int _scorePointsToWin;

    [SerializeField] private ParticleSystem _victoryFireWorkEffect;

    private void Awake()
    {
        Time.timeScale = 1;
        _levelCompletedGroup.alpha = 0;
    }

    private void Update()
    {
        if (DisplayedPoints.ScorePoints >= _scorePointsToWin)
        {
            WinGame(true);
        }
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _loadMenuButton.onClick.AddListener(OnLoadMenuButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _loadMenuButton.onClick.RemoveListener(OnLoadMenuButtonClick);
    }

    public void WinGame(bool win)
    {
        _animator.enabled = true;
        _levelCompletedGroup.alpha = 1;
        Instantiate(_victoryFireWorkEffect, _player.transform.position + Vector3.one, _victoryFireWorkEffect.transform.rotation);
        Time.timeScale = 0;
        _resultScorePointText.text = "your score " + DisplayedPoints.ScorePoints.ToString();
        _resultFruitsText.text = "your fruits " + _player.CollectedFruits.ToString();
        _endGameText.text = win == true ? "You win!" : "You Lost! Try Again!";
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
}