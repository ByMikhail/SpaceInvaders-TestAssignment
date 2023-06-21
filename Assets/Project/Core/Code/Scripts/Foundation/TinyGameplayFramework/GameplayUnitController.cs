using UnityEngine;

namespace SpaceInvaders.Foundation.TinyGameplayFramework
{
    public class GameplayUnitController<T> : MonoBehaviour
        where T : GameplayUnit
    {
        [SerializeField] private T _target;

        public T Target
        {
            get => _target;
            set => _target = value;
        }

        private bool TargetIsAssigned => _target != null;
        private bool TargetBasedUpdateHasToBeCalled => TargetIsAssigned && (!CallTargetBasedUpdatedOnlyWhenMatchIsInProgress || _gameMode.Phase == GameModePhase.MatchIsInProgress);

        protected virtual bool CallTargetBasedUpdatedOnlyWhenMatchIsInProgress => true;

        private GameModeBase _gameMode;

        #region Unity lifecycle

        protected void Awake()
        {
            _gameMode = ServiceLocator.ServiceLocator.Default.GetService<GameModeBase>();

            _gameMode.OnPhaseChanged += GameMode_OnPhaseChanged;

            if (_gameMode.Phase == GameModePhase.MatchIsInProgress)
            {
                HandleOnMatchHasStarted();
            }
        }

        protected virtual void Update()
        {
            if (TargetBasedUpdateHasToBeCalled)
            {
                TargetBasedUpdate();
            }
        }

        protected void FixedUpdate()
        {
            if (TargetBasedUpdateHasToBeCalled)
            {
                TargetBasedFixedUpdate();
            }
        }

        #endregion

        #region Event listeners

        protected virtual void HandleOnMatchHasStarted() { }
        protected virtual void HandleOnMatchHasEnded() { }
        protected virtual void TargetBasedUpdate() { }
        protected virtual void TargetBasedFixedUpdate() { }

        private void GameMode_OnPhaseChanged(GameModePhase phase)
        {
            switch (phase)
            {
                case GameModePhase.MatchIsInProgress:
                    HandleOnMatchHasStarted();
                    break;
                case GameModePhase.MatchHasEnded:
                    HandleOnMatchHasEnded();
                    break;
            }
        }

        #endregion
    }
}