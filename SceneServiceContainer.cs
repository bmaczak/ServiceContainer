using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SceneServiceContainer : MonoServiceContainer
{
    private void Awake()
    {
        Install();
    }

    private void OnDestroy()
    {
        Uninstall();
    }
}
