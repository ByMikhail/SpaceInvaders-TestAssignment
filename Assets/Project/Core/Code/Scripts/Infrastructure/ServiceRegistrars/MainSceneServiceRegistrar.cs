using SpaceInvaders.Foundation.ServiceLocator;
using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.GameModes;
using SpaceInvaders.Infrastructure.Systems.SceneManagement;
using UnityEngine;

namespace SpaceInvaders.Infrastructure.ServiceRegistrars
{
    public class MainSceneServiceRegistrar : SceneServiceRegistrar
    {
        [SerializeField] private ScoreGameMode _gameMode;

        protected override void RegisterServices()
        {
            Register<SceneManager>(new SceneManager());
            Register<GameModeBase>(_gameMode);
            Register<ScoreGameMode>(_gameMode);
        }
    }
}