using System;
using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Controllers;
using SpaceInvaders.Gameplay.Units;
using SpaceInvaders.Tools.Extensions;
using UnityEngine;

namespace SpaceInvaders.Gameplay.GameModes
{
    public class ScoreGameMode : GameModeBase
    {
        [SerializeField] private Transform _spaceshipSpawnPoint;
        [SerializeField] private Transform _invadersGroupSpawnPoint;
        [SerializeField] private Spaceship _spaceshipPrefab;
        [SerializeField] private PlayerSpaceshipController _playerControllerPrefab;
        [SerializeField] private InvadersGroup _invadersGroupPrefab;

        public int Score
        {
            get => _score;
            private set => value.AssignTo(ref _score, OnScoreChanged);
        }

        public int Round
        {
            get => _round;
            private set => value.AssignTo(ref _round, OnRoundChanged);
        }

        public event Action OnScoreChanged;
        public event Action OnRoundChanged;

        private int _score = 0;
        private int _round = 1;
        private Spaceship _spaceship;
        private PlayerSpaceshipController _playerController;
        private InvadersGroup _invadersGroup;

        protected override void HandleMatchIsWaitingToStart()
        {
            SpawnPlayerSpaceship();
            SpawnInvadersGroup();
        }

        private void SpawnPlayerSpaceship()
        {
            _spaceship = Instantiate(_spaceshipPrefab, _spaceshipSpawnPoint.position, _spaceshipSpawnPoint.rotation);
            _spaceship.OnKilled += PlayerSpaceship_OnKilled;

            _playerController = Instantiate(_playerControllerPrefab);
            _playerController.Target = _spaceship;
        }

        private void SpawnInvadersGroup()
        {
            _invadersGroup = Instantiate(_invadersGroupPrefab, _invadersGroupSpawnPoint.position, _invadersGroupSpawnPoint.rotation);
            _invadersGroup.OnDestroyed += InvadersGroup_OnDestroyed;

            foreach (var invader in _invadersGroup)
            {
                invader.OnKilled += Invader_OnKilled;
            }
        }

        #region Event listeners

        private void Invader_OnKilled(object _, EventArgs __)
        {
            Score++;
        }

        private void PlayerSpaceship_OnKilled(object _, EventArgs __)
        {
            EndMatch();
        }

        private void InvadersGroup_OnDestroyed(object _, EventArgs __)
        {
            Round++;
            SpawnInvadersGroup();
        }

        #endregion
    }
}