using AceOfShadows.Interfaces;
using UnityEngine;

namespace AceOfShadows.Managers
{
    public class StackManager : MonoBehaviour, IStackManager
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject stackPrefab;
        [SerializeField] private GameObject cardPrefab;
        
        [Header("Stack Settings")]
        [SerializeField] private float maxVerticalOffset;
        [SerializeField] private float maxHorizontalOffset;
        [SerializeField] private float horizontalPeek;
    
        public CardStack[] InitializeStacks(Vector3[] positions, int totalCards)
        {
            float singleCardVerticalOffset = maxVerticalOffset / totalCards;
            float singleCardHorizontalOffset = maxHorizontalOffset / totalCards;
            CardStack[] stacks = new CardStack[positions.Length];
        
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject stackObj = Instantiate(stackPrefab, positions[i], Quaternion.identity);
                stacks[i] = stackObj.GetComponent<CardStack>();
                stacks[i].Initialize(horizontalPeek, singleCardVerticalOffset, singleCardHorizontalOffset);
            }
        
            return stacks;
        }

        public void CreateCards(CardStack stack, int cardCount)
        {
            for (int i = 0; i < cardCount; i++)
            {
                GameObject cardObj = Instantiate(cardPrefab, stack.transform);
                stack.AddCard(cardObj.GetComponent<Card>(), i);
            }
        }
    }
}