using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridSizeListener {
    void OnGridSizeChanged(Vector2Int gridSize);
}
public interface INoOfBombsListener {
    void NoOfBombsChanged(int noOfBombs);
}
public class GridSizeNotifier : MonoBehaviour
{
    public static GridSizeNotifier Instance { get; private set; }
    private List<IGridSizeListener> listeners = new List<IGridSizeListener>();
    private List<INoOfBombsListener> BListeners = new List<INoOfBombsListener> ();

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
    public void RegisterBlisteners(INoOfBombsListener listener) {
        if (!BListeners.Contains(listener)) {
            BListeners.Add(listener);
        }
        else {
            Debug.LogWarning("Listener already registered: " + listener);
        }
    }
    public void UnregisterBlisteners(INoOfBombsListener listener) {
        if (BListeners.Contains(listener)) {
            BListeners.Remove(listener);
        }
    }

    public void NotifyNoOfBombsChanged(int noofBombs) {
        foreach (var listener in BListeners) {
            listener.NoOfBombsChanged(noofBombs);
        }
    }

    public void NotifyGridSizeChanged(Vector2Int gridSize) {
        foreach (var listener in listeners) { 
            listener.OnGridSizeChanged(gridSize);
        }
    }    
}
