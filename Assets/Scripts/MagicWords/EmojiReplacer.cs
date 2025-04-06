using System.Collections.Generic;
using MagicWords.DTO;

namespace MagicWords
{
    public class EmojiReplacer
    {
        private readonly Dictionary<string, Emojy> emojiDict;

        public EmojiReplacer(Dictionary<string, Emojy> emojiDict)
        {
            this.emojiDict = emojiDict;
        }

        public string ProcessText(string text)
        {
            foreach (var emoji in emojiDict)
            {
                text = text.Replace("{" + emoji.Key + "}", $"<sprite name=\"{emoji.Key}\">");
            }
            return text;
        }
    }
}