using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace SpaceInvaders.Infrastructure.Systems.SceneManagement
{
    public class SceneManager
    {
        public void ReloadCurrentScene()
        {
            var currentScene = UnitySceneManager.GetActiveScene();
            UnitySceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}