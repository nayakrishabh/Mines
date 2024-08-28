using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tiles : MonoBehaviour {
    
    public GameObject buttonTile, panelObject;

    public Sprite bombSprite, daimondSprite;

    private int uid, noOfBomb = 10, noOfTiles = 16,clickCount = 0;

    private List<GameObject> tilelist = new List<GameObject>();

    public static Tiles instance;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        for (int i = 0; i < tilelist.Count; i++) {

            int index = i;
            tilelist[index].GetComponent<Button>().onClick.AddListener(() => OnClick(index));
        }

    }
    public void startbuttoncoroutine(GameObject pO) {
        panelObject = pO;
        StartCoroutine(Buttontilecreator());
    }

    public List<GameObject> getList() {
        return tilelist;
    }

    //ButtonTILECREATOR WTIH SHUFFLING LOGIC
    IEnumerator Buttontilecreator() {
        
        //Instanstiating the TileButtons
        for (int i = 0; i < noOfBomb; i++) {
            GameObject tileobject = Instantiate(buttonTile);
            tileobject.GetComponent<ObjectTag>().objectType = ObjectTag.Type.BOMB;
            tilelist.Add(tileobject);
        }
        for (int i = 0; i < noOfTiles - noOfBomb; i++) {
            tilelist.Add(Instantiate(buttonTile));
        }
        //Shuffling the List
        Shufflelist(tilelist);
        //Setting UP Parent
        if (panelObject.transform != null) {
            
            foreach (GameObject tileobject in tilelist) {
                if (tileobject != null) { 
                    tileobject.transform.SetParent(panelObject.transform);
                }
            }
        }
        for (int i = 0; i < tilelist.Count; i++) {
            TextMeshProUGUI[] tmpComponents = tilelist[i].GetComponentsInChildren<TextMeshProUGUI>();

            // Disable each TextMeshProUGUI component
            foreach (TextMeshProUGUI tmp in tmpComponents) {
                tmp.enabled = false;
            }
        }
       
        yield return null;
    }


    public void OnClick(int index) {
        
            ObjectTag objectTag = tilelist[index].GetComponent<ObjectTag>();

            revel(objectTag.objectType, index);

            Debug.LogError("Button no = " + index + objectTag.objectType);
        
    }

    //revel Logic
    void revel(ObjectTag.Type type, int index) {
        if (clickCount == 0) {
            if (type == ObjectTag.Type.BOMB) {
                tilelist[index].GetComponent<Image>().sprite = bombSprite;
                clickCount++;
            }
            else if (type == ObjectTag.Type.DAIMOND) {
                tilelist[index].GetComponent<Image>().sprite = daimondSprite;
            }
        }
    }
    //Shuffle Logic
    List<GameObject> Shufflelist(List<GameObject> Tilelist) {
        for (int i = 0; i < (Tilelist.Count - 1); i++) {
            var r =  Random.Range(i, Tilelist.Count);
            var temp = Tilelist[i];
            Tilelist[i] = Tilelist[r];
            Tilelist[r] = temp;
        }
        return Tilelist;
    }

    void Update() {

        if (clickCount > 0) {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }
}
