using SpaceInvaders.Foundation.ServiceLocator;
using SpaceInvaders.Gameplay.GameModes;
using SpaceInvaders.GUI.Code.Scripts.Systems;
using SpaceInvaders.GUI.Code.Scripts.Views;
using UnityEngine;

namespace SpaceInvaders.GUI.Code.Scripts.Screens
{
    public class MatchScreen : UIScreen
    {
        [SerializeField] private PropertyFieldInt _scoreField;
        [SerializeField] private PropertyFieldInt _roundField;

        private ScoreGameMode _gameMode;

        #region Unity lifecycle

        private void Awake()
        {
            _gameMode = ServiceLocator.Default.GetService<ScoreGameMode>();
        }

        private void OnEnable()
        {
            _gameMode.OnScoreChanged += GameMode_OnScoreChanged;
            _gameMode.OnRoundChanged += GameMode_OnRoundChanged;

            GameMode_OnScoreChanged();
            GameMode_OnRoundChanged();
        }

        private void OnDisable()
        {
            _gameMode.OnScoreChanged -= GameMode_OnScoreChanged;
            _gameMode.OnRoundChanged -= GameMode_OnRoundChanged;
        }

        #endregion

        #region Event listeners

        private void GameMode_OnScoreChanged()
        {
            _scoreField.Value = _gameMode.Score;
        }

        private void GameMode_OnRoundChanged()
        {
            _roundField.Value = _gameMode.Round;
        }

        #endregion
    }
}