using SpaceInvaders.Foundation.ServiceLocator;
using SpaceInvaders.Gameplay.GameModes;
using SpaceInvaders.GUI.Code.Scripts.Systems;
using SpaceInvaders.GUI.Code.Scripts.Views;
using SpaceInvaders.Infrastructure.Systems.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.GUI.Code.Scripts.Screens
{
    public class ResultScreen : UIScreen
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private PropertyFieldInt _scoreField;
        [SerializeField] private PropertyFieldInt _roundField;

        private SceneManager _sceneManager;
        private ScoreGameMode _gameMode;

        #region Unity lifecycle

        private void Awake()
        {
            _sceneManager = ServiceLocator.Default.GetService<SceneManager>();
            _gameMode = ServiceLocator.Default.GetService<ScoreGameMode>();
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartButton_OnClick);
            _scoreField.Value = _gameMode.Score;
            _roundField.Value = _gameMode.Round;
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartButton_OnClick);
        }

        #endregion

        #region Event listeners

        private void RestartButton_OnClick()
        {
            _sceneManager.ReloadCurrentScene();
        }

        #endregion
    }
}