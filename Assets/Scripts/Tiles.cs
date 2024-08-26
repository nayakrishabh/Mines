using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tiles : MonoBehaviour {
    
    public GameObject buttonTile;

    public GameObject panelObject;

    public static Tiles instance;

    public int uid;
    private List<GameObject> tilelist = new List<GameObject>();
    

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
    IEnumerator Buttontilecreator() {
        
        for (int i = 0; i < 10; i++) {
            GameObject gameObject = Instantiate(buttonTile, panelObject.transform);
            gameObject.GetComponent<ObjectTag>().objectType = ObjectTag.Type.BOMB;
            tilelist.Add(gameObject);
        }

        for (int i = 0; i < 6; i++) {
            tilelist.Add(Instantiate(buttonTile, panelObject.transform));
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
    void OnClick(int index) {
        Debug.LogError("Button no = " + index);
    }
    List<GameObject> Shufflelist(List<GameObject> Tilelist) {
        for (int i = 0; i < (Tilelist.Count - 1); i++) {
            var r =  Random.Range(i, Tilelist.Count);
            var temp = Tilelist[i];
            Tilelist[i] = Tilelist[r];
            Tilelist[r] = temp;
        }
        return Tilelist;
    }
}
