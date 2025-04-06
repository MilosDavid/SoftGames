using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AceOfShadows.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonOk;
        [SerializeField] private Button buttonYes;
        [SerializeField] private Button buttonNo;
        [SerializeField] private RectTransform rectOfDialog;
        [SerializeField] private RectTransform rectOfGameOverButton;
        [SerializeField] private RectTransform rectOfExitDialogButtons;
        
        [SerializeField] private TextMeshProUGUI textDialogDescription;
        
        private void OnEnable()
        {
            buttonBack.onClick.AddListener(ShowExitDialog);
            buttonOk.onClick.AddListener(GoToMainMenu);
            buttonYes.onClick.AddListener(GoToMainMenu);
            buttonNo.onClick.AddListener(HideExitDialog);
        }

        private void ShowExitDialog()
        {
            PauseGame();
            textDialogDescription.SetText("Are you sure you want to quit?");
            rectOfExitDialogButtons.gameObject.SetActive(true);
            rectOfDialog.gameObject.SetActive(true);
        }

        private void HideExitDialog()
        {
            ResumeGame();
            rectOfExitDialogButtons.gameObject.SetActive(false);
            rectOfDialog.gameObject.SetActive(false);
        }

        public void ShowGameOverDialog()
        {
            PauseGame();
            textDialogDescription.SetText("Game Over");
            rectOfDialog.gameObject.SetActive(true);
            rectOfGameOverButton.gameObject.SetActive(true);
        }

        private void GoToMainMenu()
        {
            rectOfDialog.gameObject.SetActive(false);
            rectOfGameOverButton.gameObject.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }
        
    
        void PauseGame()
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
    
        void ResumeGame()
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }

        private void OnDisable()
        {
            buttonBack.onClick.RemoveListener(ShowExitDialog);
            buttonOk.onClick.RemoveListener(GoToMainMenu);
            buttonYes.onClick.RemoveListener(GoToMainMenu);
            buttonNo.onClick.RemoveListener(HideExitDialog);
        }
    }
}