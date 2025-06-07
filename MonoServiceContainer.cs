using UnityEngine;

public class MonoServiceContainer : MonoBehaviour
{
    private ServiceContainer container = new();

    protected void Install() => container.InstallServices(GetComponents<IServiceInstaller>());

    protected void Uninstall() => container.UninstallServices();
}
