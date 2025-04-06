using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicWords
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI messageText;

        public void SetAvatar(Sprite avatar)
        {
            avatarImage.sprite = avatar;
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetText(string text, Dictionary<string, Sprite> emojiSprites)
        {
            messageText.text = text;
        }
    }
}