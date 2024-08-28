using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject containers;

    private int balance;

    public static UiManager instance;
    private GameManager gameManager;
    private List<GameObject> containersList;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);  // To ensure singleton behavior
        }
        containersList = new List<GameObject>();
        gameManager = GameManager.instance;
    }
    public void createBLocks(GameObject rP) {
        StartCoroutine(createBlocks(rP));
    }

    IEnumerator createBlocks(GameObject rP) {
        int count = 6;
        RectTransform parentTransform = rP.transform as RectTransform;

        for (int i = 0; i < count; i++) {
            GameObject container = Instantiate(containers, parentTransform);
            RectTransform rectTransform = container.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(320f, 90f);
            containersList.Add(container);
        }
        blockConfigurator();
        yield return null;
    }

    void blockConfigurator() {
        for (int i = 0; i < containersList.Count; i++) {
            int index = i;
            Transform blocktransform = containersList[index].transform;
            
            switch (i) {

                case 0:
                    textAdder(blocktransform,"Balance : " );
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    return;

            }
        }
    }

    void textAdder(Transform blocktransform, string Text) {
        // Create a new TextMeshProUGUI object
        GameObject textObject = new GameObject("Balance", typeof(RectTransform), typeof(TextMeshProUGUI));

        // Set the TextMeshPro object as a child of the Image object
        textObject.transform.SetParent(blocktransform, false); // 'false' to keep the local position

        // Configure the RectTransform of the TextMeshPro
        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(290f,50f); // Match the size of the Image
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.anchoredPosition = Vector2.zero;

        // Configure the TextMeshProUGUI component
        TextMeshProUGUI tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = Text;
        tmp.fontStyle = FontStyles.Bold;
        tmp.fontSize = 30;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.black;  // Set text color (example)
    }

    void buttonMaker(Transform blocktransform, Sprite ui, int index) {

    }

    void textInputFieldAdder(Transform blocktransform, int index) {

    }
}
