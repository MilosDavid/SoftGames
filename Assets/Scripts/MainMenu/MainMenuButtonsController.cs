using MainMenu.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuButtonsController : MonoBehaviour
    {
        [SerializeField] private Button[] buttonsMainMenu;
        
        private void Start()
        {
            var actions = new IButtonAction[]
            {
                new LoadSceneAction("AceOfShadows"),
                new LoadSceneAction("MagicWords"),
                new LoadSceneAction("PhoenixFlame")
            };

            for (int i = 0; i < buttonsMainMenu.Length; i++)
            {
                var handler = buttonsMainMenu[i].gameObject.AddComponent<ButtonHandler>();
                handler.Initialize(actions[i], buttonsMainMenu[i]);
            }
        }
    }
}
