using System.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SquarePanelCreator : MonoBehaviour {
    [SerializeField]
    private RectTransform canvasTransform; // Assign your Canvas' RectTransform in the Inspector
    [SerializeField]
    private float size; // Desired size of the square panel
    
    
    public static SquarePanelCreator instance;

    

    //private int tileCount = 16;
    public static GameObject panelObject;
    private GridLayoutGroup gridLayoutGroup;
    Color panelcolor = new Color(0f,156f,137f);

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start() {
        
        PanelCreator();
        configuregridlayoutgroup();

        Tiles.instance.panelObject = panelObject;
        Tiles.instance.startbuttoncoroutine();
    }

    

    void PanelCreator() {
        // Create a new GameObject with a RectTransform and Image component
        panelObject = new GameObject("SquarePanel", typeof(RectTransform), typeof(Image));

        // Optional: Add color to the panel
        panelObject.GetComponent<Image>().color = panelcolor;

        Vector2 anchorpoint = new Vector2(0.5f, 0.5f);

        gridLayoutGroup = panelObject.AddComponent<GridLayoutGroup>();

        // Set the panel's parent to be the Canvas
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.SetParent(canvasTransform, false);

        // Set the size of the panel
        panelRect.sizeDelta = new Vector2(size, size);

        // Center the panel and make it maintain a square aspect ratio
        panelRect.anchorMin = anchorpoint;
        panelRect.anchorMax = anchorpoint;
        panelRect.pivot = anchorpoint;
        panelRect.anchoredPosition = Vector2.zero;

    }

    void configuregridlayoutgroup() {
        
        gridLayoutGroup.padding = new RectOffset(88, 8, 9, 8);

        // Set cell size and spacing
        gridLayoutGroup.cellSize = new Vector2(180, 180);
        gridLayoutGroup.spacing = new Vector2(30, 30);

        // Set alignment and layout constraints
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;

    }

    private void Update() {

    }
}

