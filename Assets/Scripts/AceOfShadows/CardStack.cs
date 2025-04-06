using System.Collections.Generic;
using AceOfShadows.Interfaces;
using UnityEngine;

namespace AceOfShadows
{
    public class CardStack : MonoBehaviour, ICardContainer
    {
        [SerializeField] private float horizontalPeek;
        [SerializeField] private float verticalOffset;
        [SerializeField] private float horizontalOffset;
    
        private List<Card> cards = new List<Card>();
        public int CardCount => cards.Count;
        
        public void Initialize(float hPeek, float singleCardVerticalOffset, float singleCardHorizontalOffset)
        {
            horizontalPeek = hPeek;
            verticalOffset = singleCardVerticalOffset;
            horizontalOffset = singleCardHorizontalOffset;
        }

        public void AddCard(Card card, int index)
        {
            float yPos = Mathf.Sqrt(index) * verticalOffset;
            float xPos = Mathf.Sqrt(index) * horizontalOffset;
            float randomOffset = Random.Range(-0.005f, 0.005f);
            card.transform.SetParent(transform);
            card.transform.localPosition = new Vector3(
                xPos,
                yPos,
                -index * 0.01f
            );
            card.SetSortingOrder(index);
            cards.Add(card);
        }

        public void RemoveCard(Card card) => cards.Remove(card);
        public Card GetTopCard() => cards[CardCount - 1];
    
        public Vector3 GetNextCardPosition()
        {
            return transform.position + new Vector3(
                 horizontalPeek,
                CardCount * verticalOffset,
                -CardCount * 0.01f
            );
        }
    }
}