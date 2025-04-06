using UnityEngine;
using UnityEngine.UI;

namespace PhoenixFlame
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Button buttonToggle;
    
        private bool isOn;

        void OnEnable()
        {
            buttonToggle.onClick.AddListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            isOn = !isOn;
            animator.SetBool("IsOn", isOn);
        }

        private void OnDisable()
        {
            buttonToggle.onClick.RemoveListener(OnButtonClick);
        }
    }
}
