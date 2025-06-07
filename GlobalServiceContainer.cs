using UnityEngine;

public class GlobalServiceContainer : MonoServiceContainer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        var projectServices = Resources.Load<GlobalServiceContainer>("GlobalServices");
        projectServices.OnAppStart();
    }

    private void OnAppStart()
    {
        Install();
        Application.quitting += OnAppQuit;
    }
    
    private void OnAppQuit()
    {
        Uninstall();
    }
}
