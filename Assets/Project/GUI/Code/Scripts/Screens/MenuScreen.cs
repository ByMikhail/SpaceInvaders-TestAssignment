using SpaceInvaders.Foundation.ServiceLocator;
using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.GUI.Code.Scripts.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.GUI.Code.Scripts.Screens
{
    public class MenuScreen : UIScreen
    {
        [SerializeField] private Button _startMatchButton;

        private GameModeBase _gameMode;

        #region Unity lifecycle

        private void Awake()
        {
            _gameMode = ServiceLocator.Default.GetService<GameModeBase>();
        }

        private void OnEnable()
        {
            _startMatchButton.onClick.AddListener(StartMatchButton_OnClick);
        }

        private void OnDisable()
        {
            _startMatchButton.onClick.RemoveListener(StartMatchButton_OnClick);
        }

        #endregion

        #region Event listeners

        private void StartMatchButton_OnClick()
        {
            _gameMode.StartMatch();
        }

        #endregion
    }
}