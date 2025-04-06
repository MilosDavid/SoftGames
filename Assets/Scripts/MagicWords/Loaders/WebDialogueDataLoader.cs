using System;
using System.Collections;
using MagicWords.DTO;
using MagicWords.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MagicWords.Loaders
{
    public class WebDialogueDataLoader : IDialogueDataLoader
    {
        private string url;

        public WebDialogueDataLoader(string jsonUrl)
        {
            url = jsonUrl;
        }
        
        public IEnumerator LoadDialogueData(Action<DialogData> onDataLoaded)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading JSON: " + www.error);
                yield break;
            }

            var data = JsonConvert.DeserializeObject<DialogData>(www.downloadHandler.text);
            onDataLoaded?.Invoke(data);
        }
    }
}