using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tiles : MonoBehaviour {
    
    public GameObject buttonTile;

    public GameObject panelObject;

    public static Tiles instance;

    public int uid;
    private List<GameObject> tilelist = new List<GameObject>();
    public enum Type {
        BOMB,
        DAIMOND
    };

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    public void startbuttoncoroutine(GameObject pO) {
        panelObject = pO;
        StartCoroutine(Buttontilecreator());
    }
    IEnumerator Buttontilecreator() {
        
        for (int i = 0; i < 16; i++) {
            tilelist.Add(Instantiate(buttonTile, panelObject.transform));
            // Get all TextMeshProUGUI components in the children of the tile
            TextMeshProUGUI[] tmpComponents = tilelist[i].GetComponentsInChildren<TextMeshProUGUI>();

            // Disable each TextMeshProUGUI component
            foreach (TextMeshProUGUI tmp in tmpComponents) {
                tmp.enabled = false;
            }
        }
        yield return null;
    }
}
