using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tiles : MonoBehaviour {
    
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

    void Start() {
        pC = PanelCreator.instance;
        noOfBomb = SelectionUIController.instance.SelectedBombCount;
        int sqL = SelectionUIController.instance.SelectedGridSize.x;
        noOfTiles = sqL*sqL;
    }
    void Update() {
        noOfBomb = SelectionUIController.instance.SelectedBombCount;

        if (clickCount > 0) {
            buttonIntractablityOff();
            UIController.Instance.onUIButton();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    //public void startbuttoncoroutine(GameObject pO) {
    //    panelObject = pO;
    //    StartCoroutine(Buttontilecreator());
    //}
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

        

        Debug.LogError(pC.selectedTile.name);

        mainToTemp();
        
        AddListeners();

        tempToMain();


        yield return null;

        ////Instanstiating the TileButtons
        //for (int i = 0; i < noOfBomb; i++) {
        //    GameObject tileobject = Instantiate(buttonTile);
        //    tileobject.GetComponent<ObjectTag>().objectType = ObjectTag.Type.BOMB;
        //    tileobject.GetComponent<Button>().interactable=false;
        //    tilelist.Add(tileobject);
        //}
        //for (int i = 0; i < noOfTiles - noOfBomb; i++) {
        //    GameObject tileobject = Instantiate(buttonTile);
        //    tileobject.GetComponent<Button>().interactable = false;
        //    tilelist.Add(Instantiate(tileobject));
        //}

        // Clear the list (this only affects the list, not the child objects)

        //restoreTiles();

        //Setting UP Parent
        //if (panelObject.transform != null) {
        //    foreach (GameObject tileobject in tilelist) {
        //        if (tileobject != null) { 
        //            tileobject.transform.SetParent(panelObject.transform);
        //            tileobject.transform.localScale = Vector3.one;
        //        }
        //    }
        //}
        //for (int i = 0; i < tilelist.Count; i++) {
        //    TextMeshProUGUI[] tmpComponents = tilelist[i].GetComponentsInChildren<TextMeshProUGUI>();
        //    foreach (TextMeshProUGUI tmp in tmpComponents) {
        //        tmp.enabled = false;
        //    }
        //}
    }
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
        ShuffleList(tilelist);
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
    private void AddListeners() {
        foreach (var tile in tilelist) {
            //tile.GetComponent<Button>().interactable = true;
            tile.GetComponent<Button>().onClick.RemoveAllListeners();
            tile.GetComponent<Button>().onClick.AddListener(() => OnClick(tile));
        }
        //for (int i = 0; i < tilelist.Count; i++) {
        //    int index = i; // Prevent closure issue
        //    tilelist[index].GetComponent<Button>().onClick.RemoveAllListeners();
        //    tilelist[index].GetComponent<Button>().onClick.AddListener(() => OnClick(index));
        //    buttonIntractablityOn();
        //}
    }

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
    public void OnClick(GameObject tile) {
        // Debug log to check if tilelist has items
        //Debug.Log($"Tilelist count: {tilelist.Count}");

        //// Debug log to compare the incoming tile with list items
        //for (int i = 0; i < tilelist.Count; i++) {
        //    Debug.Log($"Comparing tile {i}: List tile = {tilelist[i].name}, Clicked tile = {tile.name}");
        //}

        //ObjectTag objectTag = tile.GetComponent<ObjectTag>();
        //int index = tilelist.IndexOf(tile);

        //if (index != -1) {
        //    revel(objectTag.objectType, tile);
        //    Debug.Log($"Button no = {index} | Object Type = {objectTag.objectType}");
        //}
        //else {
        //    Debug.LogError("Tile not found in the list. Tile name: " + tile.name);
        //}

        revel(tile);
    }
    public void restoreTiles() {
        resetTiles();
        clickCount = 0;
    }
    public void resetAllTiles() {

    }
    //revel Logic
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

    private void resetTiles() {
        //Debug.LogError("Entry");

       // Debug.LogError(tilelist.Count);

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
    //Shuffle Logic
    //private List<GameObject> Shufflelist(List<GameObject> Tilelist) {
    //    for (int i = 0; i < (Tilelist.Count - 1); i++) {
    //        var r =  Random.Range(i, Tilelist.Count);
    //        var temp = Tilelist[i];
    //        Tilelist[i] = Tilelist[r];
    //        Tilelist[r] = temp;
    //    }
    //    return Tilelist;
    //}
    private void ShuffleList(List<GameObject> tileList) {
        // Create a new list to avoid modifying the original

        int randomIndex = Random.Range(0, tileList.Count);

        foreach (GameObject tile in tileList) { 
            tile.GetComponent<ObjectTag>().objectType = ObjectTag.Type.DAIMOND;
        }

        tilelist[randomIndex].GetComponent<ObjectTag>().objectType = ObjectTag.Type.BOMB;

    }

    public void setNoofTiles(int x) {
        Debug.Log(x);
        noOfTiles = x * x;
    }
}
//public void hidetiles() {
//    foreach (GameObject gameObject in tilelist) {
//        if (gameObject.GetComponent<ObjectTag>().revelType == ObjectTag.RevelType.REVELED) {
//            gameObject.GetComponent<Image>().sprite = tileSprite;
//            gameObject.GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.UNREVELED;
//            clickCount = 0;
//        }
//    }
//}