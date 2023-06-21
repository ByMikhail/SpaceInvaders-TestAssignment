using SpaceInvaders.Foundation.TinyGameplayFramework;
using UnityEngine;

namespace SpaceInvaders.GUI.Code.Scripts.Systems
{
    public class UIScreenManager : MonoBehaviour
    {
        [SerializeField] private GameModeBase _gameMode;
        [SerializeField] private UIScreen _prePlayScreen;
        [SerializeField] private UIScreen _playScreen;
        [SerializeField] private UIScreen _postPlayScreen;

        private UIScreen _currentScreen;

        #region Unity lifecycle

        private void Awake()
        {
            _prePlayScreen.Show();
            _playScreen.Hide();
            _postPlayScreen.Hide();

            _currentScreen = _prePlayScreen;
        }

        private void OnEnable()
        {
            _gameMode.OnPhaseChanged += GameMode_OnPhaseChanged;
            GameMode_OnPhaseChanged(_gameMode.Phase);
        }

        private void OnDisable()
        {
            _gameMode.OnPhaseChanged -= GameMode_OnPhaseChanged;
        }

        #endregion

        #region Event listeners

        private void GameMode_OnPhaseChanged(GameModePhase phase)
        {
            _currentScreen.Hide();

            var nextScreen = phase switch
            {
                GameModePhase.MatchIsWaitingToStart => _prePlayScreen,
                GameModePhase.MatchIsInProgress => _playScreen,
                GameModePhase.MatchHasEnded => _postPlayScreen,
                _ => null
            };

            if (nextScreen)
            {
                nextScreen.Show();
            }

            _currentScreen = nextScreen;
        }

        #endregion
    }
}