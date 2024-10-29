using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tiles : MonoBehaviour ,INoOfBombsListener {
    
    public GameObject buttonTile;

    public Sprite bombSprite, daimondSprite,tileSprite;

    private int uid, noOfBomb = 1, noOfTiles,clickCount = 0;

    private List<GameObject> tilelist = new List<GameObject>();

    private PanelCreator pC;

    public static Tiles instance;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void OnDisable() {
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.UnregisterBlisteners(this);
        }
    }
    void Start() {

        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.RegisterBlisteners(this);
        }

        pC = PanelCreator.instance;
        noOfBomb = SelectionUIController.instance.SelectedBombCount;
        int sqL = SelectionUIController.instance.SelectedGridSize.x;
        noOfTiles = sqL*sqL;
    }
    void Update() {

        if (clickCount > 0) {
            buttonIntractablityOff();
            UIController.Instance.onUIButton();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }
    public void gameReset() {
        StartCoroutine(GameResetCoroutine());
    }

    private IEnumerator GameResetCoroutine() {
        yield return StartCoroutine(Buttontilecreator());
        AddListeners();
    }

    //ButtonTILECREATOR WTIH SHUFFLING LOGIC
    IEnumerator Buttontilecreator() {

        restoreTiles();

        mainToTemp();

        ShuffleBomb(tilelist);

        AddListeners();

        tempToMain();

        yield return null;
    }
    
    public void OnClick(GameObject tile) {
        revel(tile);
    }
    private void restoreTiles() {
        resetTiles();
        clickCount = 0;
    }

    public void NoOfBombsChanged(int noOfBombs) {
        noOfBomb = noOfBombs;
    }

    #region BASIC LOGICS
    private void ShuffleBomb(List<GameObject> tileList) {

        foreach (GameObject tile in tileList) {
            tile.GetComponent<ObjectTag>().objectType = ObjectTag.Type.DAIMOND;
        }

        HashSet<int> usedIndices = new HashSet<int>();

        for (int i = 0; i < noOfBomb; i++) {
            int randomIndex;

            
            do {
                randomIndex = Random.Range(0, tileList.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            tileList[randomIndex].GetComponent<ObjectTag>().objectType = ObjectTag.Type.BOMB;
        }

    }
    public void setNoofTiles(int x) {
        Debug.Log(x);
        noOfTiles = x * x;
    }
    private void resetTiles() {
        if (pC.selectedTile.name == "SquarePanel-3x3(Clone)") {
            foreach (GameObject gameObject in pC.tilelist3x3) {
                gameObject.GetComponent<Image>().sprite = tileSprite;
                gameObject.GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.UNREVELED;
            }
        }
        else if (pC.selectedTile.name == "SquarePanel-5x5(Clone)") {
            foreach (GameObject gameObject in pC.tilelist5x5) {
                gameObject.GetComponent<Image>().sprite = tileSprite;
                gameObject.GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.UNREVELED;
            }
        }
        else if (pC.selectedTile.name == "SquarePanel-7x7(Clone)") {
            foreach (GameObject gameObject in pC.tilelist7x7) {
                gameObject.GetComponent<Image>().sprite = tileSprite;
                gameObject.GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.UNREVELED;
            }
        }
        else if (pC.selectedTile.name == "SquarePanel-9x9(Clone)") {
            foreach (GameObject gameObject in pC.tilelist9x9) {
                gameObject.GetComponent<Image>().sprite = tileSprite;
                gameObject.GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.UNREVELED;
            }
        }
    }
    private void revel(GameObject tile) {
        ObjectTag.Type type = tile.GetComponent<ObjectTag>().objectType;
        if (clickCount == 0) {
            if (type == ObjectTag.Type.BOMB) {
                tile.GetComponent<Image>().sprite = bombSprite;
                clickCount++;
            }
            else if (type == ObjectTag.Type.DAIMOND) {
                tile.GetComponent<Image>().sprite = daimondSprite;
            }
        }
        tile.GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.REVELED;
    }
    private void AddListeners() {
        foreach (var tile in tilelist) {
            tile.GetComponent<Button>().onClick.RemoveAllListeners();
            tile.GetComponent<Button>().onClick.AddListener(() => OnClick(tile));
        }
    }

    #region OBJECT TRANSFER
    private void mainToTemp() {

        if (pC.selectedTile.name == "SquarePanel-3x3(Clone)") {
            tilelist.AddRange(pC.tilelist3x3);
            pC.tilelist3x3.Clear();
        }
        else if (pC.selectedTile.name == "SquarePanel-5x5(Clone)") {
            tilelist.AddRange(pC.tilelist5x5);
            pC.tilelist5x5.Clear();
        }
        else if (pC.selectedTile.name == "SquarePanel-7x7(Clone)") {
            tilelist.AddRange(pC.tilelist7x7);
            pC.tilelist7x7.Clear();
        }
        else if (pC.selectedTile.name == "SquarePanel-9x9(Clone)") {
            tilelist.AddRange(pC.tilelist9x9);
            pC.tilelist9x9.Clear();
        }
    }
    private void tempToMain() {

        buttonIntractablityOn();

        if (pC.selectedTile.name == "SquarePanel-3x3(Clone)") {
            pC.tilelist3x3 = tilelist;
            tilelist.Clear();
        }
        else if (pC.selectedTile.name == "SquarePanel-5x5(Clone)") {
            pC.tilelist5x5 = tilelist;
            tilelist.Clear();
        }
        else if (pC.selectedTile.name == "SquarePanel-7x7(Clone)") {
            pC.tilelist7x7 = tilelist;
            tilelist.Clear();
        }
        else if (pC.selectedTile.name == "SquarePanel-9x9(Clone)") {
            pC.tilelist9x9 = tilelist;
            tilelist.Clear();
        }
    }
    #endregion

    #region BUTTON INTRACTABLITY
    private void buttonIntractablityOn() {
        foreach (var tile in tilelist) {
            tile.GetComponent<Button>().interactable = true;
        }
    }
    private void buttonIntractablityOff() {

        if (pC.selectedTile.name == "SquarePanel-3x3(Clone)") {
            foreach (GameObject gameObject in pC.tilelist3x3) {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else if (pC.selectedTile.name == "SquarePanel-5x5(Clone)") {
            foreach (GameObject gameObject in pC.tilelist5x5) {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else if (pC.selectedTile.name == "SquarePanel-7x7(Clone)") {
            foreach (GameObject gameObject in pC.tilelist7x7) {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else if (pC.selectedTile.name == "SquarePanel-9x9(Clone)") {
            foreach (GameObject gameObject in pC.tilelist9x9) {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
    #endregion

    #endregion
}