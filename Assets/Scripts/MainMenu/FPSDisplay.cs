using UnityEngine;

namespace MainMenu
{
    public class FPSDisplay : MonoBehaviour
    {
        private float deltaTime = 0.0f;

        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int fps = Mathf.CeilToInt(1.0f / deltaTime);
            GUI.color = Color.white;
            GUI.skin.label.fontSize = 20;
            GUI.Label(new Rect(10, 10, 100, 50), $"FPS: {fps}");
        }
    }
}