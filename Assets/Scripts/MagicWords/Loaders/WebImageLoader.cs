using System;
using System.Collections;
using MagicWords.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace MagicWords.Loaders
{
    public class WebImageLoader : IImageLoader
    {
        public IEnumerator LoadSprite(string url, Action<Sprite> onSpriteLoaded)
        {
            url = TempFixForBadUrlInJson(url);
            
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading image: " + www.error);
                yield break;
            }

            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            onSpriteLoaded?.Invoke(sprite);
        }

        private string TempFixForBadUrlInJson(string url)
        {
            if (url == "https://api.dicebear.com:81/9.x/fun-emoji/png?seed=Sad")
            {
                return "https://api.dicebear.com/9.x/fun-emoji/png?seed=Sad";
            }

            return url;
        }
    }
}