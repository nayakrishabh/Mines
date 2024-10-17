using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridSizeListener {
    void OnGridSizeChanged(Vector2Int gridSize);
}

public class GridSizeNotifier : MonoBehaviour
{
    public static GridSizeNotifier Instance { get; private set; }
    private List<IGridSizeListener> listeners = new List<IGridSizeListener>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void RegisterListener(IGridSizeListener listener) {
        if (!listeners.Contains(listener)) {
            listeners.Add(listener);
        }
        else {
            Debug.LogWarning("Listener already registered: " + listener);
        }
    }

    public void UnregisterListener(IGridSizeListener listener) {
        if (listeners.Contains(listener)) {
            listeners.Remove(listener);
        }
    }

    public void NotifyGridSizeChanged(Vector2Int gridSize) {
        foreach (var listener in listeners) { 
            listener.OnGridSizeChanged(gridSize);
        }
    }    
}
