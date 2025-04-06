using UnityEngine;

namespace AceOfShadows.Interfaces
{
    public interface ICardContainer
    {
        void AddCard(Card card, int index);
        void RemoveCard(Card card);
        int CardCount { get; }
        Card GetTopCard();
        Vector3 GetNextCardPosition();
    }
}