using UnityEngine;

namespace AceOfShadows.Interfaces
{
    public interface IStackManager
    {
        CardStack[] InitializeStacks(Vector3[] positions, int totalCards);
        void CreateCards(CardStack stack, int cardCount);
    }
}