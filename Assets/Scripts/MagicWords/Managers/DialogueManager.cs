using System.Collections;
using System.Collections.Generic;
using MagicWords.DTO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Avatar = MagicWords.DTO.Avatar;

namespace MagicWords.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Transform dialogueContainer;
        [SerializeField] private GameObject messagePrefab;
        [SerializeField] private GameObject leftMessagePrefab;
        [SerializeField] private GameObject rightMessagePrefab;
        [SerializeField] private string jsonUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";
        [SerializeField] private float delayBetweenMessages = 3f;

        private DialogData dialogueData;
        private Dictionary<string, Avatar> avatarDictionary = new Dictionary<string, Avatar>();
        private Dictionary<string, Emojy> emojiDictionary = new Dictionary<string, Emojy>();
        private Dictionary<string, Sprite> emojiSprites = new Dictionary<string, Sprite>();
        private Dictionary<string, Sprite> avatarSprites = new Dictionary<string, Sprite>();
        
        private MessagePool messagePool;
        private float yOffset = 0f;
        
        private void Start()
        {
            StartCoroutine(LoadData());
        }

        private IEnumerator LoadData()
        {
            UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading JSON: " + www.error);
                yield break;
            }

            messagePool = new MessagePool(messagePrefab, dialogueContainer, 10);
            
            dialogueData = JsonConvert.DeserializeObject<DialogData>(www.downloadHandler.text);
            
            foreach (var avatar in dialogueData.Avatars)
            {
                avatarDictionary[avatar.Name] = avatar;
                StartCoroutine(LoadAvatarSprite(avatar.Url, avatar.Name));
            }

            foreach (var emoji in dialogueData.Emojies)
            {
                emojiDictionary[emoji.Name] = emoji;
                StartCoroutine(LoadEmojiSprite(emoji.Url, emoji.Name));
            }

            StartCoroutine(DisplayDialogueWithDelay());
        }

        private IEnumerator LoadEmojiSprite(string url, string emojiName)
        {
            if (url == "https://api.dicebear.com:81/9.x/fun-emoji/png?seed=Sad")
            {
                url = "https://api.dicebear.com/9.x/fun-emoji/png?seed=Sad";
            }
            
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading emoji: " + www.error);
                yield break;
            }

            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            emojiSprites[emojiName] = sprite;
        }

        private IEnumerator LoadAvatarSprite(string url, string avatarName)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading avatar: " + www.error);
                yield break;
            }

            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            avatarSprites[avatarName] = sprite;
        }

        private IEnumerator DisplayDialogueWithDelay()
        {
            yield return new WaitForSeconds(1f);

            foreach (var line in dialogueData.Dialogue)
            {
                DisplayMessage(line);
                yield return new WaitForSeconds(delayBetweenMessages);
            }
        }
        
        private void DisplayMessage(Dialogue line)
        {
            GameObject messageObj = messagePool.Get();
            MessageUI messageUI = messageObj.GetComponent<MessageUI>();

            Avatar avatar = avatarDictionary.ContainsKey(line.Name) ? avatarDictionary[line.Name] : null;
            if (avatarSprites.ContainsKey(line.Name))
            {
                messageUI.SetAvatar(avatarSprites[line.Name]);
            }

            messageUI.SetName(line.Name);
            messageUI.SetText(ProcessText(line.Text), emojiSprites);

            RectTransform messageRect = messageObj.GetComponent<RectTransform>();
            messageRect.anchoredPosition = new Vector2(0, yOffset);
            yOffset -= messageRect.rect.height;
        }

        private string ProcessText(string text)
        {
            foreach (var emoji in emojiDictionary)
            {
                text = text.Replace("{" + emoji.Key + "}", $"<sprite name=\"{emoji.Key}\">");
            }
            return text;
        }
    }
}