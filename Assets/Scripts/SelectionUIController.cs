using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUIController : MonoBehaviour
{
    public static SelectionUIController instance;

    [SerializeField] private SelectionUIConnector connector;

    private Dictionary<Vector2Int, List<int>> gridBombOptions;
    private Vector2Int _selectedGridSize;
    private int _selectedBombCount;
    private Color defaultButtonColor = Color.white;
    private Color selectedButtonColor = Color.cyan;
    private Button selectedGridSizeButton;
    private Button selectedBombButton;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        gridBombOption();
        addListenersAndSetText();
        NotifyGridSize(new Vector2Int(5, 5));
        //SetGridSize(new Vector2Int(5, 5));
    }
    private void gridBombOption() {
        gridBombOptions = new Dictionary<Vector2Int, List<int>>
        {
            { new Vector2Int(3, 3), new List<int> { 1, 2, 3, 4 } },
            { new Vector2Int(5, 5), new List<int> { 1, 3, 5, 7 } },
            { new Vector2Int(7, 7), new List<int> { 1, 5, 10, 15 } },
            { new Vector2Int(9, 9), new List<int> { 5, 10, 15, 20 } }
        };
    }
    //private void SetGridSize(Vector2Int gridSize) {

    //}
    private void NotifyGridSize(Vector2Int gridSize) {

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyGridSizeChanged(gridSize);
        }

        _selectedGridSize = gridSize;

        HighlightButton(getGridButton(_selectedGridSize), ref selectedGridSizeButton);

        selectedGridSizeButton = getGridButton(gridSize);

        Tiles.instance.setNoofTiles(gridSize.x);

        Debug.Log("Grid Size Selected: " + gridSize.x + "x" + gridSize.y);

        HighlightButton(connector.bombButtons[0], ref selectedBombButton);

        UpdateBombButtons(gridBombOptions[gridSize]);

        List<int> selectedBs = gridBombOptions[_selectedGridSize];
        _selectedBombCount = selectedBs[0];

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyNoOfBombsChanged(_selectedBombCount);
        }
    }
    private void UpdateBombButtons(List<int> bombOptions) {
        int optionsCount = Mathf.Min(bombOptions.Count, connector.bombButtons.Count);
        for (int i = 0; i < bombOptions.Count; i++) {
                int bombCount = bombOptions[i];
                connector.bombButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = bombCount.ToString();
                connector.bombButtons[i].onClick.RemoveAllListeners();
                int index = i;
                connector.bombButtons[i].onClick.AddListener(() => SetBombCount(bombCount, connector.bombButtons[index]));
        }
    }
    private void SetBombCount(int bombCount , Button bombButton) {
        _selectedBombCount = bombCount;
        Debug.Log("Bomb Count Selected: " + _selectedBombCount);

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyNoOfBombsChanged(_selectedBombCount);
        }

        HighlightButton(bombButton, ref selectedBombButton);
    }
    // Method to Highlight the selected button and reset previous one
    private void HighlightButton(Button newSelectedButton, ref Button previouslySelectedButton) {
        // Reset the color of the previously selected button
        if (previouslySelectedButton != null) {
            SetButtonColor(previouslySelectedButton, defaultButtonColor);
        }

        // Highlight the new selected button
        SetButtonColor(newSelectedButton, selectedButtonColor);

        // Update the reference to the currently selected button
        previouslySelectedButton = newSelectedButton;
    }

    // Method to Set the Button Color
    private void SetButtonColor(Button button, Color color) {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        colorBlock.highlightedColor = color;
        colorBlock.selectedColor = color;
        button.colors = colorBlock;
    }

    private void addListenersAndSetText() {
        connector.Button3x3.GetComponent<Button>().onClick.AddListener(() => NotifyGridSize(new Vector2Int(3, 3)));
        connector.Button5x5.GetComponent<Button>().onClick.AddListener(() => NotifyGridSize(new Vector2Int(5, 5)));
        connector.Button7x7.GetComponent<Button>().onClick.AddListener(() => NotifyGridSize(new Vector2Int(7, 7)));
        connector.Button9x9.GetComponent<Button>().onClick.AddListener(() => NotifyGridSize(new Vector2Int(9, 9)));

        connector.custom.GetComponent<TMP_InputField>().onValueChanged.AddListener((string nofb)=>custombombNo(nofb));
        connector.custom.GetComponent<TMP_InputField>().onEndEdit.AddListener((string inp)=>validateInput(inp));
        connector.Button3x3.GetComponentInChildren<TextMeshProUGUI>().text = $"3x3";
        connector.Button5x5.GetComponentInChildren<TextMeshProUGUI>().text = $"5x5";
        connector.Button7x7.GetComponentInChildren<TextMeshProUGUI>().text = $"7x7";
        connector.Button9x9.GetComponentInChildren<TextMeshProUGUI>().text = $"9x9";
    }
    private void validateInput(string inp) {
        if(string.IsNullOrEmpty(inp) || int.Parse(inp) < 1) {
            List<int> selectedBs = gridBombOptions[_selectedGridSize];
            _selectedBombCount = selectedBs[0];
            connector.custom.GetComponent<TMP_InputField>().text = _selectedBombCount.ToString();
        }
        else if(int.Parse(inp) > ((_selectedGridSize.x * _selectedGridSize.x) - 1)) {
            List<int> selectedBs = gridBombOptions[_selectedGridSize];
            _selectedBombCount = ((_selectedGridSize.x * _selectedGridSize.x) - 1);
            connector.custom.GetComponent<TMP_InputField>().text = _selectedBombCount.ToString();
        }
    }
    private void custombombNo(string nofb) {
        _selectedBombCount = int.Parse(nofb);

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyNoOfBombsChanged(_selectedBombCount);
        }
        connector.custom.GetComponent<TMP_InputField>().text = nofb;
    }
    private Button getGridButton(Vector2Int gridS) {
        if (gridS == new Vector2(3f, 3f)) {
            return connector.Button3x3.GetComponent<Button>();
        }
        else if (gridS == new Vector2(5f, 5f)) {
            return connector.Button5x5.GetComponent<Button>();
        }
        else if (gridS == new Vector2(7f, 7f)) {
            return connector.Button7x7.GetComponent<Button>();
        }
        else if (gridS == new Vector2(9f, 9f)) {
            return connector.Button9x9.GetComponent<Button>();
        }
        else {
            Debug.LogError("Unsupported dimensions: " + gridS);
            return null;
        }
    }
    public void setUIOff() {
        connector.Button3x3.GetComponent<Button>().interactable = false;
        connector.Button5x5.GetComponent<Button>().interactable = false;
        connector.Button7x7.GetComponent<Button>().interactable = false;
        connector.Button9x9.GetComponent<Button>().interactable = false;
        connector.custom.GetComponent<TMP_InputField>().interactable = false;

        foreach(Button bombBs in connector.bombButtons) {
            bombBs.interactable = false;
        }
    }
    public void setUIOn() {
        connector.Button3x3.GetComponent<Button>().interactable = true;
        connector.Button5x5.GetComponent<Button>().interactable = true;
        connector.Button7x7.GetComponent<Button>().interactable = true;
        connector.Button9x9.GetComponent<Button>().interactable = true;
        connector.custom.GetComponent<TMP_InputField>().interactable = true;

        foreach (Button bombBs in connector.bombButtons) {
            bombBs.interactable = true;
        }
    }
    public Vector2Int SelectedGridSize { get { return _selectedGridSize; } }
    public int SelectedBombCount { get { return _selectedBombCount; } }
    // Update is called once per frame
    void Update()
    {
        
    }
}
