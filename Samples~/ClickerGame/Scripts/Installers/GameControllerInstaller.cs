using UnityEngine;

public class GameControllerInstaller : MonoBehaviour, IServiceInstaller
{
    [SerializeField] private GameController gameController;
    
    public void InstallServices(ServiceContainer container)
    {
        container.Install(gameController);
    }
}
