using System;
using System.Collections.Generic;

public class ServiceContainer
{
    private List<object> installedServices = new();

    public void Install(object service)
    {
        ServiceLocator.Register(service);
        installedServices.Add(service);
    }

    public void InstallServices(IEnumerable<IServiceInstaller> installers)
    {
        foreach (var installer in installers)
        {
            installer.InstallServices(this);
        }
        foreach (var installedService in installedServices)
        {
            if (installedService is IInitializable initializable)
                initializable.Initialize();
        }
    }

    public void UninstallServices()
    {
        foreach (var installedService in installedServices)
        {
            if (installedService is IDisposable disposable)
                disposable.Dispose();
        }
        foreach (var installedService in installedServices)
        {
            ServiceLocator.Unregister(installedService);
        }
    }
}
