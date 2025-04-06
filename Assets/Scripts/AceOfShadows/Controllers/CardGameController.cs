using System.Collections;
using AceOfShadows.Interfaces;
using AceOfShadows.Managers;
using DG.Tweening;
using UnityEngine;

namespace AceOfShadows.Controllers
{
    public class CardGameController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int totalCards;
    [SerializeField] private float moveDuration;
    [SerializeField] private float rotationInterval;
    
    [Header("Animation Curves")]
    [SerializeField] private AnimationCurve moveEaseCurve;
    [SerializeField] private AnimationCurve jumpEaseCurve;
    [SerializeField] private AnimationCurve rotationEaseCurve;
    
    [SerializeField] private GameManager gameManager;
    
    private IStackManager stackManager;
    private CardStack[] stacks;
    
    private const int MOVING_CARD_ORDER = 200;
    
    public void Initialize(IStackManager manager)
    {
        stackManager = manager;
    }
    
    private void Start()
    {
        Vector3[] stackPositions = 
        {
            new Vector3(-4f, 0, 0),
            new Vector3(4f, 0, 0)
        };
    
        stacks = stackManager.InitializeStacks(stackPositions, totalCards);
    
        if(stacks.Length > 0 && stacks[0] != null)
        {
            stackManager.CreateCards(stacks[0], totalCards);
            StartCoroutine(CardRotationRoutine());
        }
    }

    private IEnumerator CardRotationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(rotationInterval);
            
            if (stacks[0].CardCount == 0)
            {
                gameManager.ShowGameOverDialog();
                yield break;
            }

            if (stacks[0].CardCount > 0 && stacks[1] != null)
            {
                Card topCard = stacks[0].GetTopCard();
                Vector3 targetPosition = stacks[1].GetNextCardPosition();

                StartCoroutine(MoveCardWithoutWaiting(topCard, targetPosition));
            }
        }
    }
    
    private IEnumerator MoveCardWithoutWaiting(Card card, Vector3 targetPosition)
    {
        Vector3 startPos = card.transform.position;
        
        stacks[0].RemoveCard(card);
        stacks[1].AddCard(card, stacks[1].CardCount);
        
        card.SetSortingOrder(MOVING_CARD_ORDER);
        
        card.transform.position = startPos;
        Vector3 originalScale = card.transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;
        float scaleDuration = moveDuration * 0.5f;
        
        Sequence moveSequence = DOTween.Sequence();
        
        moveSequence.Append(card.transform.DOMove(targetPosition, moveDuration)
            .SetEase(moveEaseCurve));
        moveSequence.Join(card.transform.DOJump(targetPosition, 1f, 1, moveDuration)
            .SetEase(jumpEaseCurve));
        moveSequence.Join(card.transform.DORotate(new Vector3(0, 0, 180), moveDuration)
            .SetLoops(1, LoopType.Yoyo)
            .SetEase(rotationEaseCurve));
        moveSequence.Join(card.transform.DOScale(targetScale, scaleDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                card.transform.DOScale(originalScale, scaleDuration)
                    .SetEase(Ease.InQuad);
            }));
        
        moveSequence.OnComplete(() => {
            card.SetSortingOrder(stacks[1].CardCount);
            
            if(stacks[0].CardCount == 0)
            {
                gameManager.ShowGameOverDialog();
            }
        });
        
        yield return null;
    }
}
}