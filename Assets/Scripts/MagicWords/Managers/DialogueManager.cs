using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MagicWords.DTO;
using MagicWords.Interfaces;
using MagicWords.Loaders;
using UnityEngine;
using Avatar = MagicWords.DTO.Avatar;

namespace MagicWords.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Transform dialogueContainer;

        [SerializeField] private GameObject leftMessagePrefab;
        [SerializeField] private GameObject rightMessagePrefab;
        
        [SerializeField] private string jsonUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";
        [SerializeField] private float delayBetweenMessages = 3f;

        private IDialogueDataLoader dataLoader;
        private IImageLoader imageLoader;
        
        private MessagePool messagePool;
        private EmojiReplacer emojiReplacer;
        
        private DialogData dialogueData;
        private Dictionary<string, Avatar> avatars = new Dictionary<string, Avatar>();
        private Dictionary<string, Emojy> emojis = new Dictionary<string, Emojy>();
        private Dictionary<string, Sprite> emojiSprites = new Dictionary<string, Sprite>();
        private Dictionary<string, Sprite> avatarSprites = new Dictionary<string, Sprite>();
        
        private void Awake()
        {
            dataLoader = new WebDialogueDataLoader(jsonUrl);
            imageLoader = new WebImageLoader();
            messagePool = new MessagePool(leftMessagePrefab, rightMessagePrefab, dialogueContainer, 10);
        }
        
        private void Start()
        {
            StartCoroutine(dataLoader.LoadDialogueData(OnDialogueDataLoaded));
        }
        
        private void OnDialogueDataLoaded(DialogData data)
        {
            emojiReplacer = new EmojiReplacer(data.Emojies.ToDictionary(e => e.Name, e => e));
            avatars = data.Avatars.ToDictionary(a => a.Name, a => a);
            emojis = data.Emojies.ToDictionary(e => e.Name, e => e);

            foreach (var avatar in data.Avatars)
            {
                StartCoroutine(imageLoader.LoadSprite(avatar.Url, sprite => avatarSprites[avatar.Name] = sprite));
            }

            foreach (var emoji in data.Emojies)
            {
                StartCoroutine(imageLoader.LoadSprite(emoji.Url, sprite => emojiSprites[emoji.Name] = sprite));
            }

            StartCoroutine(DisplayDialogueWithDelay(data.Dialogue));
        }
        
        private IEnumerator DisplayDialogueWithDelay(List<Dialogue> lines)
        {
            yield return new WaitForSeconds(1f);
            foreach (var line in lines)
            {
                DisplayMessage(line);
                yield return new WaitForSeconds(delayBetweenMessages);
            }
        }
        
        private void DisplayMessage(Dialogue line)
        {
            string position = avatars.TryGetValue(line.Name, out var avatar) ? avatar.Position : "left";
            GameObject messageObj = messagePool.Get(position);

            MessageUI ui = messageObj.GetComponent<MessageUI>();
            ui.SetName(line.Name);

            if (avatarSprites.TryGetValue(line.Name, out var sprite))
            {
                ui.SetAvatar(sprite);
            }

            ui.SetText(emojiReplacer.ProcessText(line.Text), emojiSprites);
        }
    }
}