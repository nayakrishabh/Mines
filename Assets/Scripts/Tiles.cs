using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tiles : MonoBehaviour {
    
    public GameObject buttonTile, panelObject;

    public Sprite bombSprite, daimondSprite,tileSprite;

    private int uid, noOfBomb = 1, noOfTiles,clickCount = 0;

    private List<GameObject> tilelist = new List<GameObject>();

    public static Tiles instance;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        noOfBomb = SelectionUIController.instance.SelectedBombCount;
        int sqL = SelectionUIController.instance.SelectedGridSize.x;
        noOfTiles = sqL*sqL;
    }
    void Update() {
        noOfBomb = SelectionUIController.instance.SelectedBombCount;

        if (clickCount > 0) {
            foreach (GameObject gameObject in tilelist) {
                gameObject.GetComponent<Button>().interactable = false;
            }
            UIController.Instance.onUIButton();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    public void startbuttoncoroutine(GameObject pO) {
        panelObject = pO;
        StartCoroutine(Buttontilecreator());
    }
    public void gameReset() {
        destroyTiles();
        StartCoroutine(GameResetCoroutine());
    }

    private IEnumerator GameResetCoroutine() {
        yield return StartCoroutine(Buttontilecreator());
        AddListeners();
    }

    //ButtonTILECREATOR WTIH SHUFFLING LOGIC
    IEnumerator Buttontilecreator() {
        tilelist.Clear();
        //Instanstiating the TileButtons
        for (int i = 0; i < noOfBomb; i++) {
            GameObject tileobject = Instantiate(buttonTile);
            tileobject.GetComponent<ObjectTag>().objectType = ObjectTag.Type.BOMB;
            tileobject.GetComponent<Button>().interactable=false;
            tilelist.Add(tileobject);
        }
        for (int i = 0; i < noOfTiles - noOfBomb; i++) {
            GameObject tileobject = Instantiate(buttonTile);
            tileobject.GetComponent<Button>().interactable = false;
            tilelist.Add(Instantiate(tileobject));
        }
        //Shuffling the List
        Shufflelist(tilelist);
        //Setting UP Parent
        if (panelObject.transform != null) {
            foreach (GameObject tileobject in tilelist) {
                if (tileobject != null) { 
                    tileobject.transform.SetParent(panelObject.transform);
                    tileobject.transform.localScale = Vector3.one;
                }
            }
        }
        for (int i = 0; i < tilelist.Count; i++) {
            TextMeshProUGUI[] tmpComponents = tilelist[i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI tmp in tmpComponents) {
                tmp.enabled = false;
            }
        }
        AddListeners();
        yield return null;
    }
    private void AddListeners() {
        for (int i = 0; i < tilelist.Count; i++) {
            int index = i; // Prevent closure issue
            tilelist[index].GetComponent<Button>().onClick.RemoveAllListeners();
            tilelist[index].GetComponent<Button>().onClick.AddListener(() => OnClick(index));
        }
    }
    public void OnClick(int index) {
        
            ObjectTag objectTag = tilelist[index].GetComponent<ObjectTag>();

            revel(objectTag.objectType, index);

            Debug.LogError("Button no = " + index +" "+ objectTag.objectType);
        
    }
    public void destroyTiles() {
        foreach (GameObject tileobject in tilelist) {
            Destroy(tileobject);
        }
        tilelist.Clear();
        clickCount = 0;
    }
    //revel Logic
    private void revel(ObjectTag.Type type, int index) {
        if (clickCount == 0) {
            if (type == ObjectTag.Type.BOMB) {
                tilelist[index].GetComponent<Image>().sprite = bombSprite;
                clickCount++;
            }
            else if (type == ObjectTag.Type.DAIMOND) {
                tilelist[index].GetComponent<Image>().sprite = daimondSprite;
            }
        }
        tilelist[index].GetComponent<ObjectTag>().revelType = ObjectTag.RevelType.REVELED;
    }
    //Shuffle Logic
    private List<GameObject> Shufflelist(List<GameObject> Tilelist) {
        for (int i = 0; i < (Tilelist.Count - 1); i++) {
            var r =  Random.Range(i, Tilelist.Count);
            var temp = Tilelist[i];
            Tilelist[i] = Tilelist[r];
            Tilelist[r] = temp;
        }
        return Tilelist;
    }
    
    public List<GameObject> getList() {
        return tilelist;
    }
    public void tileInactive() {
        foreach (GameObject gameObject in tilelist) {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
    public void tileActive() {
        foreach (GameObject gameObject in tilelist) {
            gameObject.GetComponent<Button>().interactable = true;
        }
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