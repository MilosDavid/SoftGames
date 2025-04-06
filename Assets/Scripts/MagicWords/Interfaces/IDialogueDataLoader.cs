using System;
using System.Collections;
using MagicWords.DTO;

namespace MagicWords.Interfaces
{
    public interface IDialogueDataLoader
    {
        IEnumerator LoadDialogueData(Action<DialogData> onDataLoaded);
    }
}