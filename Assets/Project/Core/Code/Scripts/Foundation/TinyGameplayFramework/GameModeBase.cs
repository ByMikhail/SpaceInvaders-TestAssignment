using System;
using UnityEngine;

namespace SpaceInvaders.Foundation.TinyGameplayFramework
{
    public abstract class GameModeBase : MonoBehaviour
    {
        public GameModePhase Phase
        {
            get => _phase;
            set
            {
                if (_phase == value) return;

                _phase = value;
                OnPhaseChanged?.Invoke(_phase);
            }
        }

        public event Action<GameModePhase> OnPhaseChanged;

        private GameModePhase _phase = GameModePhase.MatchIsWaitingToStart;

        protected void Start()
        {
            HandleMatchIsWaitingToStart();
        }

        public void StartMatch()
        {
            if (Phase != GameModePhase.MatchIsWaitingToStart)
            {
                LogInvalidPhaseTransition(nameof(StartMatch));
                return;
            }

            Phase = GameModePhase.MatchIsInProgress;
            HandleMatchHasStarted();
        }

        public void EndMatch()
        {
            if (Phase != GameModePhase.MatchIsInProgress)
            {
                LogInvalidPhaseTransition(nameof(EndMatch));
                return;
            }

            Phase = GameModePhase.MatchHasEnded;
            HandleMatchHasEnded();
        }


        protected virtual void HandleMatchIsWaitingToStart() { }
        protected virtual void HandleMatchHasStarted() { }
        protected virtual void HandleMatchHasEnded() { }

        private void LogInvalidPhaseTransition(string transitionTriggerMethodName)
        {
            Debug.LogWarning($"[{nameof(GameModeBase)}] Attempt to call {transitionTriggerMethodName} when {nameof(Phase)} is {Phase}");
        }
    }
}