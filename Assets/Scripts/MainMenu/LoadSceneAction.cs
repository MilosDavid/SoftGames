using MainMenu.Interfaces;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LoadSceneAction : IButtonAction
    {
        private string sceneName;

        public LoadSceneAction(string sceneName)
        {
            this.sceneName = sceneName;
        }

        public void Execute()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}