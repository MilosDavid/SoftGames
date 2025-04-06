using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MagicWords
{
    public class BackButtonController : MonoBehaviour
    {
        [SerializeField] private Button buttonBack;
        private void OnEnable()
        {
            buttonBack.onClick.AddListener(BackToMainMenu);
        }

        private void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnDisable()
        {
            buttonBack.onClick.RemoveListener(BackToMainMenu);
        }
    }
}