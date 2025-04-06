using UnityEngine;

namespace AceOfShadows
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public void SetSortingOrder(int order) 
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = order;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer not found on card!");
            }
        }
    }
}