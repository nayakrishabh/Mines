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
    private TMP_InputField selectedInputField = null;


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

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyNoOfBombsChanged(_selectedBombCount);
        }

    }
    private void UpdateBombButtons(List<int> bombOptions) {

        _selectedBombCount = bombOptions[0];

        int optionsCount = Mathf.Min(bombOptions.Count, connector.bombButtons.Count);

        for (int i = 0; i < bombOptions.Count; i++) {
                int bombCount = bombOptions[i];
                connector.bombButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = bombCount.ToString();
                connector.bombButtons[i].onClick.RemoveAllListeners();
                int index = i;
                connector.bombButtons[i].onClick.AddListener(() => SetBombCount(bombCount, connector.bombButtons[index]));
        }

        ResetCustomInputFieldColor();
        connector.custom.GetComponent<TMP_InputField>().text = "Custom";
    }
    private void ResetCustomInputFieldColor() {
        TMP_InputField customInputField = connector.custom.GetComponent<TMP_InputField>();
        ColorBlock colorBlock = customInputField.colors;

        // Set colors back to default
        colorBlock.normalColor = ColorBlock.defaultColorBlock.normalColor;
        colorBlock.highlightedColor = ColorBlock.defaultColorBlock.highlightedColor;
        colorBlock.selectedColor = ColorBlock.defaultColorBlock.selectedColor;
        colorBlock.disabledColor = ColorBlock.defaultColorBlock.disabledColor;

        customInputField.colors = colorBlock;
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
    #region Setting Bomb Count
    private void SetBombCount(int bombCount, Button bombButton) {
        _selectedBombCount = bombCount;

        Debug.Log("Bomb Count Selected: " + _selectedBombCount);

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyNoOfBombsChanged(_selectedBombCount);
        }
        if (selectedInputField != null) {
            SetInputColor(selectedInputField, defaultButtonColor);
        }
        HighlightButton(bombButton, ref selectedBombButton);
    }
    private void custombombNo(string nofb) {

        _selectedBombCount = int.Parse(nofb);

        TMP_InputField custIF = connector.custom.GetComponent<TMP_InputField>();

        if (selectedBombButton != null) {
            SetButtonColor(selectedBombButton, defaultButtonColor);
        }

        highLightCustomField(custIF, ref selectedInputField);

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.NotifyNoOfBombsChanged(_selectedBombCount);
        }
        custIF.text = nofb;
    }
    private void validateInput(string inp) {

        TMP_InputField custIF = connector.custom.GetComponent<TMP_InputField>();

        if (string.IsNullOrEmpty(inp) || int.Parse(inp) < 1) {
            List<int> selectedBs = gridBombOptions[_selectedGridSize];
            _selectedBombCount = selectedBs[0];
            custIF.text = _selectedBombCount.ToString();
        }
        else if(int.Parse(inp) > ((_selectedGridSize.x * _selectedGridSize.x) - 1)) {
            List<int> selectedBs = gridBombOptions[_selectedGridSize];
            _selectedBombCount = ((_selectedGridSize.x * _selectedGridSize.x) - 1);
            custIF.text = _selectedBombCount.ToString();
        }
    }
    #endregion

    #region Highlighting Buttons and Custom InputField
    private void HighlightButton(Button newSelectedButton, ref Button previouslySelectedButton) {

        if (previouslySelectedButton != null) {
            SetButtonColor(previouslySelectedButton, defaultButtonColor);
            previouslySelectedButton.interactable = true;
        }

        SetButtonColor(newSelectedButton, selectedButtonColor , true);
        previouslySelectedButton = newSelectedButton;
    }
    private void SetButtonColor(Button button, Color color, bool retainColorOnDisable = false) {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        colorBlock.highlightedColor = color;
        colorBlock.selectedColor = color;

        if (retainColorOnDisable) {
            colorBlock.disabledColor = color;
        }
        else {
            // Reset disabled color to default
            colorBlock.disabledColor = ColorBlock.defaultColorBlock.disabledColor; // Or whatever default color you prefer
        }

        button.colors = colorBlock;
    }
    private void highLightCustomField(TMP_InputField custIF, ref TMP_InputField previouslySelectedField) {
        if (previouslySelectedField != null) {
            SetInputColor(previouslySelectedField, defaultButtonColor);
            previouslySelectedField.interactable = true;
        }
        SetInputColor(custIF, selectedButtonColor , true);

        previouslySelectedField = custIF;
    }
    private void SetInputColor(TMP_InputField custIF, Color color, bool retainColorOnDisable = false) {
        ColorBlock colorBlock = custIF.colors;
        colorBlock.normalColor = color;
        colorBlock.highlightedColor = color;
        colorBlock.selectedColor = color;

        if (retainColorOnDisable) {
            colorBlock.disabledColor = color;
        }
        else {
            // Reset disabled color to default
            colorBlock.disabledColor = Color.gray; // Or whatever default color you prefer
        }
        custIF.colors = colorBlock;
    }
    #endregion
  
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
    void Update()
    {
        
    }
}
