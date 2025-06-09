using UnityEngine;

public class ProgressManagerInstaller : MonoBehaviour, IServiceInstaller
{
    public void InstallServices(ServiceContainer container)
    {
       container.Install(new ProgressManager());
    }
}
