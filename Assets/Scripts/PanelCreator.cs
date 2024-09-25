using System.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PanelCreator : MonoBehaviour {
    [SerializeField]
    private RectTransform canvasTransform; // Assign your Canvas' RectTransform in the Inspector
    [SerializeField]
    private GameObject UIPanel;
    [SerializeField]
    private GameObject selectionPanel;
    [SerializeField]
    private GameObject startPanelRef;
    
    public static PanelCreator instance;
    //private int tileCount = 16;
    public static GameObject panelObject;
    public static GameObject rightPanel;
    public static GameObject leftPanel;


    private GameObject startPanel;
    private GridLayoutGroup gridLayoutGroup;
    private VerticalLayoutGroup verticalLayoutGroup;
    private Color panelcolor = new Color(0f,0f,0f,0f);
    private Color leftPanelColor = new Color(19.0f / 255.0f, 19.0f / 255.0f, 19.0f / 255.0f, 233.0f / 255.0f);

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start() {
        CreateleftPanel();
        PanelCreators();
        CreateRightPanel();
        Tiles.instance.startbuttoncoroutine(panelObject);
    }

    

    private void PanelCreators() {
        // Create a new GameObject with a RectTransform and Image component
        panelObject = new GameObject("SquarePanel", typeof(RectTransform), typeof(Image));

        // Optional: Add color to the panel
        panelObject.GetComponent<Image>().color = panelcolor;

        Vector2 anchorpointmin = new Vector2(0.24f, 0.08f);
        Vector2 anchorpointmax = new Vector2(0.76f, 0.92f);
        Vector2 pivotPoint = new Vector2(0.5f, 0.5f);

        gridLayoutGroup = panelObject.AddComponent<GridLayoutGroup>();

        // Set the panel's parent to be the Canvas
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.SetParent(canvasTransform, false);
        startPanel = Instantiate(startPanelRef, canvasTransform);
        startPanel.transform.SetAsLastSibling();
        // Set the size of the panel
        panelRect.sizeDelta = Vector2.zero;

        // Center the panel and make it maintain a square aspect ratio
        panelRect.anchorMin = anchorpointmin;
        panelRect.anchorMax = anchorpointmax;
        panelRect.pivot = pivotPoint;
        panelRect.anchoredPosition = Vector2.zero;

        configuregridlayoutgroup();
    }
    private void CreateRightPanel() {
        // Create the new panel to the right of the center panel
        rightPanel = new GameObject("RightPanel", typeof(RectTransform), typeof(Image));
        rightPanel.GetComponent<Image>().color = panelcolor;

        //verticalLayoutGroup = rightPanel.AddComponent<VerticalLayoutGroup>();

        // Set the panel's parent to be the Canvas
        RectTransform rightPanelRect = rightPanel.GetComponent<RectTransform>();
        rightPanelRect.SetParent(canvasTransform, false);

        Instantiate(UIPanel, rightPanelRect);
        // Set the size of the right panel (same as the original panel)
        rightPanelRect.sizeDelta = Vector2.zero;

        // Position the right panel based on the size of the original panel
        rightPanelRect.anchorMin = new Vector2(0.78f, 0f); // Anchored at the center
        rightPanelRect.anchorMax = new Vector2(0.98f, 1f);
        rightPanelRect.pivot = new Vector2(0.5f, 0.5f);

        // Offset the position to move it to the right of the original panel
        rightPanelRect.anchoredPosition = Vector2.zero;

        //configVerticalLayoutGroup();
    }
    private void CreateleftPanel() {
        // Create the new panel to the right of the center panel
        leftPanel = new GameObject("leftPanel", typeof(RectTransform), typeof(Image));
        leftPanel.GetComponent<Image>().color = leftPanelColor;

        //verticalLayoutGroup = rightPanel.AddComponent<VerticalLayoutGroup>();

        // Set the panel's parent to be the Canvas
        RectTransform leftPanelRect = leftPanel.GetComponent<RectTransform>();
        leftPanelRect.SetParent(canvasTransform, false);

        Instantiate(selectionPanel, leftPanelRect);
        // Set the size of the right panel (same as the original panel)
        leftPanelRect.sizeDelta = Vector2.zero;

        // Position the right panel based on the size of the original panel
        leftPanelRect.anchorMin = new Vector2(0.026f, 0f); // Anchored at the center
        leftPanelRect.anchorMax = new Vector2(0.222f, 1f);
        leftPanelRect.pivot = new Vector2(0.5f, 0.5f);

        // Offset the position to move it to the right of the original panel
        leftPanelRect.anchoredPosition = Vector2.zero;

        //configVerticalLayoutGroup();
    }
    #region LayoutConfigurations
    private void configuregridlayoutgroup() {

        Vector2 cellSize = GetCellSize(SelectionUIController.instance.SelectedGridSize);
        Vector2 spaceing = GetSpacing(SelectionUIController.instance.SelectedGridSize);

        gridLayoutGroup.padding = new RectOffset(0,0,0,0);

        // Set cell size and spacing
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.spacing = spaceing;

        // Set alignment and layout constraints
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;

    }

    public void OnGridSizeChanged() {
        configuregridlayoutgroup();
    }


    //private Vector2 getcellSize(Vector2Int gridS) {

    //    Vector2 selected = new Vector2(180,180);

    //    if (gridS == new Vector2(3, 3)) {
    //        selected = new Vector2(250, 250);
    //    }
    //    else if (gridS == new Vector2(5, 5)) {
    //        selected = new Vector2(150, 150);
    //    }
    //    else if (gridS == new Vector2(7, 7)) {
    //        selected = new Vector2(105, 105);
    //    }
    //    else if (gridS == new Vector2(9, 9)) {
    //        selected = new Vector2(84, 84);
    //    }

    //    return selected;
    //}

    //private Vector2 getpacing(Vector2Int gridS) {

    //    Vector2 selected = new Vector2(30, 30);

    //    if (gridS == new Vector2(3, 3)) {
    //        return selected;
    //    }
    //    else if (gridS == new Vector2(5, 5)) {
    //        return selected;
    //    }
    //    else if (gridS == new Vector2(7, 7)) {
    //        selected = new Vector2(25, 25);
    //    }
    //    else if (gridS == new Vector2(9, 9)) {
    //        selected = new Vector2(18, 18);
    //    }

    //    return selected;
    //}
    // Optimized version of getcellSize

    private Vector2 GetCellSize(Vector2Int gridSize) {

        Dictionary<Vector2Int, Vector2> cellSizes = new Dictionary<Vector2Int, Vector2>() {
        { new Vector2Int(3, 3), new Vector2(250, 250) },
        { new Vector2Int(5, 5), new Vector2(150, 150) },
        { new Vector2Int(7, 7), new Vector2(105, 105) },
        { new Vector2Int(9, 9), new Vector2(84, 84) }
        };

        return cellSizes.TryGetValue(gridSize, out Vector2 cellSize) ? cellSize : new Vector2(180, 180);
    }

    private Vector2 GetSpacing(Vector2Int gridSize) {

        Dictionary<Vector2Int, Vector2> spacingSizes = new Dictionary<Vector2Int, Vector2>() {
        { new Vector2Int(3, 3), new Vector2(30, 30) },
        { new Vector2Int(5, 5), new Vector2(30, 30) },
        { new Vector2Int(7, 7), new Vector2(25, 25) },
        { new Vector2Int(9, 9), new Vector2(18, 18) }
        };

        return spacingSizes.TryGetValue(gridSize, out Vector2 spacingSize) ? spacingSize : new Vector2(30, 30);
    }



    //void configVerticalLayoutGroup() {
    //    verticalLayoutGroup.padding = new RectOffset(0,0,0,0);

    //    verticalLayoutGroup.spacing = -88f;
    //    verticalLayoutGroup.childAlignment= TextAnchor.MiddleCenter;
    //    verticalLayoutGroup.childControlHeight = false;
    //    verticalLayoutGroup.childControlWidth = false;
    //    verticalLayoutGroup.childScaleHeight = true;
    //    verticalLayoutGroup.childScaleWidth = true;
    //}

    #endregion
    private void Update() {
    }
    public GameObject getUiPanel() {
        return UIPanel;
    }
    public GameObject getStartPanel() {
        return startPanel;
    }
    public GameObject getpanelobject() {
        return panelObject;
    }
}

