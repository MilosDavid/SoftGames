using System;
using System.Collections;
using UnityEngine;

namespace MagicWords.Interfaces
{
    public interface IImageLoader
    {
        IEnumerator LoadSprite(string url, Action<Sprite> onSpriteLoaded);
    }
}