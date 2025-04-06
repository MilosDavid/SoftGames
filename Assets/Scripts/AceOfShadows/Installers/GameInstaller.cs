using AceOfShadows.Controllers;
using AceOfShadows.Managers;
using UnityEngine;

namespace AceOfShadows.Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private StackManager stackManagerPrefab;
        [SerializeField] private CardGameController gameController;
    
        private void Awake()
        {
            StackManager managerInstance = Instantiate(stackManagerPrefab);
            gameController.Initialize(managerInstance);
        }
    }
}