using MainMenu.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class ButtonHandler : MonoBehaviour
    {
        private IButtonAction action;

        public void Initialize(IButtonAction buttonAction, Button button)
        {
            action = buttonAction;
            button.onClick.AddListener(() => action.Execute());
        }
    }
}