using System.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PanelCreator : MonoBehaviour, IGridSizeListener {

    [SerializeField]
    private RectTransform canvasTransform;

    [SerializeField]
    private GameObject tilePanel3x3;
    [SerializeField] 
    private GameObject tilePanel5x5;
    [SerializeField] 
    private GameObject tilePanel7x7;
    [SerializeField] 
    private GameObject tilePanel9x9;

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
    public GameObject selectedTile { get { return _selectedTile; } }

    private GameObject _selectedTile;
    private GameObject startPanel;
    private GridLayoutGroup gridLayoutGroup;
    private VerticalLayoutGroup verticalLayoutGroup;
    private Color panelcolor = new Color(0f,0f,0f,0f);
    private Color leftPanelColor = new Color(19.0f / 255.0f, 19.0f / 255.0f, 19.0f / 255.0f, 233.0f / 255.0f);

    private GameObject tile3x3, tile5x5, tile7x7, tile9x9;

    private List<GameObject> _tilelist3x3 = new List<GameObject>(), _tilelist5x5 = new List<GameObject>(), _tilelist7x7 = new List<GameObject>(), _tilelist9x9 = new List<GameObject>();

    public List<GameObject> tilelist3x3 { get { return _tilelist3x3; } set { if (value != null) { _tilelist3x3.AddRange(value); }  } }
    public List<GameObject> tilelist5x5 { get { return _tilelist5x5; } set { if (value != null) { _tilelist5x5.AddRange(value); } } }
    public List<GameObject> tilelist7x7 { get { return _tilelist7x7; } set { if (value != null) { _tilelist7x7.AddRange(value); } } }
    public List<GameObject> tilelist9x9 { get { return _tilelist9x9; } set { if (value != null) { _tilelist9x9.AddRange(value); } } }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void OnEnable() {
        
    }
    private void OnDisable() {
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.UnregisterListener(this);
        }
    }
    void Start() {
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.RegisterListener(this);
        }
        CreateleftPanel();
        PanelCreators();
        CreateRightPanel();
        //Tiles.instance.startbuttoncoroutine(panelObject);
    }
    public void OnGridSizeChanged(Vector2Int gridSize) {
        TileGridSetup(gridSize);
    }
    private void PanelCreators() {

        tile3x3 =  Instantiate(tilePanel3x3, canvasTransform);
        tile5x5 =  Instantiate(tilePanel5x5, canvasTransform);
        tile7x7 =  Instantiate(tilePanel7x7, canvasTransform);
        tile9x9 =  Instantiate(tilePanel9x9, canvasTransform);


        for (int i = 0; i < tile3x3.transform.childCount; i++) {
            if (tile3x3.transform.GetChild(i) != null) {
                GameObject childObj = tile3x3.transform.GetChild(i).gameObject;
                Debug.Log(childObj.name);
                _tilelist3x3.Add(childObj);
            }
        }

        for (int i = 0; i < tile5x5.transform.childCount; i++) {
            _tilelist5x5.Add(tile5x5.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < tile7x7.transform.childCount; i++) {
            _tilelist7x7.Add(tile7x7.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < tile9x9.transform.childCount; i++) {
            _tilelist9x9.Add(tile9x9.transform.GetChild(i).gameObject);
        }

        //foreach (Transform child in tile3x3.transform) {
        //    _tilelist3x3.Add(child.gameObject);
        //}
        //foreach (Transform child in tile5x5.transform) {
        //    _tilelist5x5.Add(child.gameObject);
        //}
        //foreach (Transform child in tile7x7.transform) {
        //    _tilelist7x7.Add(child.gameObject);
        //}
        //foreach (Transform child in tile9x9.transform) {
        //    _tilelist9x9.Add(child.gameObject);
        //}

        TileGridSetup(new Vector2Int(5, 5));
        startPanel = Instantiate(startPanelRef, canvasTransform);
        startPanel.transform.SetAsLastSibling();
        //IGridSizeListener listener = gameObject.GetComponent<IGridSizeListener>();
        //if (listener != null) {
        //    GridSizeNotifier notifier = GridSizeNotifier.Instance;  // Ensure you're getting the correct notifier
        //    notifier.RegisterListener(listener);
        //}
        //else {
        //    Debug.LogError("Listener not found in prefab!");
        //}
        //// Create a new GameObject with a RectTransform and Image component
        //panelObject = new GameObject("SquarePanel", typeof(RectTransform), typeof(Image));

        //// Optional: Add color to the panel
        //panelObject.GetComponent<Image>().color = panelcolor;

        //Vector2 anchorpointmin = new Vector2(0.24f, 0.08f);
        //Vector2 anchorpointmax = new Vector2(0.76f, 0.92f);
        //Vector2 pivotPoint = new Vector2(0.5f, 0.5f);

        //gridLayoutGroup = panelObject.AddComponent<GridLayoutGroup>();

        //// Set the panel's parent to be the Canvas
        //RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        //panelRect.SetParent(canvasTransform, false);

        //// Set the size of the panel
        //panelRect.sizeDelta = Vector2.zero;

        //// Center the panel and make it maintain a square aspect ratio
        //panelRect.anchorMin = anchorpointmin;
        //panelRect.anchorMax = anchorpointmax;
        //panelRect.pivot = pivotPoint;
        //panelRect.anchoredPosition = Vector2.zero;

        //configuregridlayoutgroup();
    }
    private void TileGridSetup(Vector2Int gridS) {
        tileGridSelection(gridS);
    }

    private void tileGridSelection(Vector2Int gridS) {

        if (gridS == new Vector2Int(3, 3)) {
            tile3x3.SetActive(true);
            tile5x5.SetActive(false);
            tile7x7.SetActive(false);
            tile9x9.SetActive(false);
            _selectedTile = tile3x3;
        }
        else if (gridS == new Vector2Int(5, 5)) {
            tile3x3.SetActive(false);
            tile5x5.SetActive(true);
            tile7x7.SetActive(false);
            tile9x9.SetActive(false);
            _selectedTile = tile5x5;
        }
        else if (gridS == new Vector2Int(7, 7)) {
            tile3x3.SetActive(false);
            tile5x5.SetActive(false);
            tile7x7.SetActive(true);
            tile9x9.SetActive(false);
            _selectedTile = tile7x7;
        }
        else if (gridS == new Vector2Int(9, 9)) {
            tile3x3.SetActive(false);
            tile5x5.SetActive(false);
            tile7x7.SetActive(false);
            tile9x9.SetActive(true);
            _selectedTile = tile9x9;
        }
        else {
            Debug.LogError("Unsupported grid size: " + gridS);
        }
    }

    private void CreateRightPanel() {

        rightPanel = new GameObject("RightPanel", typeof(RectTransform), typeof(Image));
        rightPanel.GetComponent<Image>().color = panelcolor;

        
        
        RectTransform rightPanelRect = rightPanel.GetComponent<RectTransform>();
        rightPanelRect.SetParent(canvasTransform, false);

        Instantiate(UIPanel, rightPanelRect);
        
        rightPanelRect.sizeDelta = Vector2.zero;

        
        rightPanelRect.anchorMin = new Vector2(0.78f, 0f); 
        rightPanelRect.anchorMax = new Vector2(0.98f, 1f);
        rightPanelRect.pivot = new Vector2(0.5f, 0.5f);

        rightPanelRect.anchoredPosition = Vector2.zero;

        //configVerticalLayoutGroup();
    }
    private void CreateleftPanel() {

        leftPanel = new GameObject("leftPanel", typeof(RectTransform), typeof(Image));
        leftPanel.GetComponent<Image>().color = leftPanelColor;

        
        RectTransform leftPanelRect = leftPanel.GetComponent<RectTransform>();
        leftPanelRect.SetParent(canvasTransform, false);

        Instantiate(selectionPanel, leftPanelRect);
        
        leftPanelRect.sizeDelta = Vector2.zero;

        
        leftPanelRect.anchorMin = new Vector2(0.026f, 0f);
        leftPanelRect.anchorMax = new Vector2(0.222f, 1f);
        leftPanelRect.pivot = new Vector2(0.5f, 0.5f);

        
        leftPanelRect.anchoredPosition = Vector2.zero;

    }

    //#region LayoutConfigurations
    //private void configuregridlayoutgroup() {

    //    Vector2 cellSize = GetCellSize(SelectionUIController.instance.SelectedGridSize);
    //    Vector2 spaceing = GetSpacing(SelectionUIController.instance.SelectedGridSize);

    //    gridLayoutGroup.padding = new RectOffset(0,0,0,0);

    //    // Set cell size and spacing
    //    gridLayoutGroup.cellSize = cellSize;
    //    gridLayoutGroup.spacing = spaceing;

    //    // Set alignment and layout constraints
    //    gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
    //    gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
    //    gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
    //    gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;

    //}

    //public void OnGridSizeChanged() {
    //    configuregridlayoutgroup();
    //}


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

//#endregion
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

