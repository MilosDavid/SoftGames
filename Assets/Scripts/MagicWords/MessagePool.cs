using System.Collections.Generic;
using UnityEngine;

namespace MagicWords
{
    public class MessagePool 
    {
        private readonly GameObject leftPrefab;
        private readonly GameObject rightPrefab;
        private readonly Transform parent;
        private readonly Queue<GameObject> pool = new Queue<GameObject>();
        
        public MessagePool(GameObject leftPrefab, GameObject rightPrefab, Transform parent, int initialSize = 10)
        {
            this.leftPrefab = leftPrefab;
            this.rightPrefab = rightPrefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                GameObject leftObj = GameObject.Instantiate(leftPrefab, parent);
                leftObj.SetActive(false);
                pool.Enqueue(leftObj);

                GameObject rightObj = GameObject.Instantiate(rightPrefab, parent);
                rightObj.SetActive(false);
                pool.Enqueue(rightObj);
            }
        }

        public GameObject Get(string position)
        {
            foreach (var obj in pool)
            {
                if (!obj.activeInHierarchy && 
                    ((position == "left" && obj.CompareTag("LeftMessage")) || 
                     (position == "right" && obj.CompareTag("RightMessage"))))
                {
                    obj.SetActive(true);
                    int insertIndex = GetLastActiveSiblingIndex() + 1;
                    obj.transform.SetSiblingIndex(insertIndex);
                    return obj;
                }
            }
            
            GameObject newObj = position == "left" 
                ? GameObject.Instantiate(leftPrefab, parent) 
                : GameObject.Instantiate(rightPrefab, parent);
        
            newObj.transform.SetSiblingIndex(GetLastActiveSiblingIndex() + 1);
            return newObj;
        }
        
        private int GetLastActiveSiblingIndex()
        {
            int lastActiveIndex = -1;

            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).gameObject.activeSelf)
                {
                    lastActiveIndex = i;
                }
            }

            return lastActiveIndex;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}