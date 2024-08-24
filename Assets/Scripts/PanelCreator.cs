using System.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SquarePanelCreator : MonoBehaviour {
    [SerializeField]
    private RectTransform canvasTransform; // Assign your Canvas' RectTransform in the Inspector
    [SerializeField]
    private float size; // Desired size of the square panel
    [SerializeField]
    private GameObject buttonTile;

    private int tileCount = 16;
    private GameObject panelObject;
    private GridLayoutGroup gridLayoutGroup;

    void Start() {
        PanelCreator();
        configuregridlayoutgroup();
        StartCoroutine(buttontilecreator());
    }

    IEnumerator buttontilecreator() {
        for (int i = 0; i < tileCount; i++) {
            Instantiate(buttonTile, panelObject.transform);
            yield return null;
        }
    }

    void PanelCreator() {
        // Create a new GameObject with a RectTransform and Image component
        panelObject = new GameObject("SquarePanel", typeof(RectTransform), typeof(Image));
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

        // Optional: Add color to the panel
        Image panelImage = panelObject.GetComponent<Image>();
        panelImage.color = new Color(255f, 255f, 255f, 100f); // Change to any color you like

    }

    void configuregridlayoutgroup() {
        gridLayoutGroup.padding = new RectOffset(8, 8, 9, 8);

        // Set cell size and spacing
        gridLayoutGroup.cellSize = new Vector2(180, 180);
        gridLayoutGroup.spacing = new Vector2(30, 30);

        // Set alignment and layout constraints
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;
    }


}

