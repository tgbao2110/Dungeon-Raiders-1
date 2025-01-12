using UnityEngine;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    private List<IPausable> pausables = new List<IPausable>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPausable(IPausable pausable)
    {
        if (!pausables.Contains(pausable))
        {
            pausables.Add(pausable);
            Debug.Log($"Registered pausable: {pausable}");
        }
    }

    public void UnregisterPausable(IPausable pausable)
    {
        if (pausables.Contains(pausable))
        {
            pausables.Remove(pausable);
            Debug.Log($"Unregistered pausable: {pausable}");
        }
    }

    public void SetPaused(bool isPaused)
    {
        foreach (var pausable in pausables)
        {
            pausable.SetPaused(isPaused);
            Debug.Log($"SetPaused called on {pausable}, isPaused: {isPaused}");
        }
    }
}

public interface IPausable
{
    void SetPaused(bool isPaused);
}
