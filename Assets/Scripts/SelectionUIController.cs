using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUIController : MonoBehaviour
{
    [SerializeField] private SelectionUIConnector connector;

    private Dictionary<Vector2Int, List<int>> gridBombOptions;
    private Vector2Int selectedGridSize;
    private int selectedBombCount;
    void Start()
    {
        gridBombOption();
        addListeners();
        SetGridSize(new Vector2Int(5, 5));
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
    private void addListeners() {
        connector.Button3x3.GetComponent<Button>().onClick.AddListener(()=> SetGridSize(new Vector2Int(3,3)));
        connector.Button5x5.GetComponent<Button>().onClick.AddListener(() => SetGridSize(new Vector2Int(5, 5)));
        connector.Button7x7.GetComponent<Button>().onClick.AddListener(() => SetGridSize(new Vector2Int(7, 7)));
        connector.Button9x9.GetComponent<Button>().onClick.AddListener(() => SetGridSize(new Vector2Int(9, 9)));
    }
    private void SetGridSize(Vector2Int gridSize) {
        selectedGridSize = gridSize;
        Debug.Log("Grid Size Selected: " + gridSize.x + "x" + gridSize.y);
        UpdateBombButtons(gridBombOptions[gridSize]);
    }

    private void UpdateBombButtons(List<int> bombOptions) {
        for (int i = 0; i < bombOptions.Count; i++) {
            if (i < connector.bombButtons.Count) {
                int bombCount = bombOptions[i];
                connector.bombButtons[i].GetComponentInChildren<Text>().text = bombCount.ToString();
                int index = i;
                connector.bombButtons[i].onClick.RemoveAllListeners();
                connector.bombButtons[i].onClick.AddListener(() => SetBombCount(bombCount));
            }
        }
    }
    private void SetBombCount(int bombCount) {
        selectedBombCount = bombCount;
        Debug.Log("Bomb Count Selected: " + bombCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
