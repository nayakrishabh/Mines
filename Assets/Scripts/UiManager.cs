using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    private int balance;
    private UiConnector connector;
    private List<GameObject> containersList;
    private PanelCreator pcRef;
    private GameObject UiPanel;
    private int betamount;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        pcRef = PanelCreator.instance;
        UiPanel = pcRef.getUiPanel();
        containersList = new List<GameObject>();
    }
    void Start() {
        connector = UiPanel.GetComponent<UiConnector>();
        Debug.Log(connector.name); 
        if (connector != null) {
            addlisteners();
        }
        else {
            Debug.LogError("Failed to find UiConnector component on UiPanel!");
        }
    }
    //public void createBLocks(GameObject rP) {
    //    StartCoroutine(createBlocks(rP));
    //}
    void addlisteners() {
        connector.increaseButton.onClick.AddListener(increaseBal);
        connector.decreaseButton.onClick.AddListener(decreaseBal);
        connector.betButton.onClick.AddListener(applybet);
        connector.inputField.onValueChanged.AddListener(valuechanged);
    }
    void buttonUpdater() {
    }
    private void Update() {
        connector.balance.text = $"Balance : \n {GameManager.instance.getbalance()}";
        connector.wins.text = $"Balance : \n {GameManager.instance.getbalance()}";
    }
    private void valuechanged(string arg0) {
        betamount = int.Parse(arg0);
    }

    private void applybet() {
        
    }

    private void decreaseBal() {
        betamount--;
        connector.inputField.text = betamount.ToString();
        Debug.Log(betamount);
    }

    private void increaseBal() {
        betamount++;
        connector.inputField.text = betamount.ToString();
    }

    //IEnumerator createBlocks(GameObject rP) {
    //    int count = 6;
    //    RectTransform parentTransform = rP.transform as RectTransform;

    //    for (int i = 0; i < count; i++) {
    //        GameObject container = Instantiate(containers, parentTransform);
    //        RectTransform rectTransform = container.GetComponent<RectTransform>();
    //        rectTransform.sizeDelta = new Vector2(320f, 122f);
    //        containersList.Add(container);
    //    }
    //    blockConfigurator();
    //    yield return null;
    //}

    //void blockConfigurator() {
    //    for (int i = 0; i < containersList.Count; i++) {
    //        int index = i;
    //        Transform blocktransform = containersList[index].transform;
            
    //        switch (i) {

    //            case 0:
    //                StartCoroutine(TextAdder(blocktransform, $"Balance : \n {GameManager.instance.getbalance()}"));
    //                break;
    //            case 1:
    //                StartCoroutine(TextAdder(blocktransform, $"Win : \n"));
    //                break;
    //            case 2:
                    
    //                break;
    //            case 3:
    //                break;
    //            case 4:
    //                break;
    //            case 5:
    //                break;
    //            default:
    //                return;

    //        }
    //    }
    //}
    #region BalancePanel Creator
    //IEnumerator TextAdderCoroutine() {
    //    StartCoroutine(TextAdder());
    //    yield return null;
    //}
    //IEnumerator TextAdder(Transform blocktransform, string Text) {
    //    // Create a new TextMeshProUGUI object
    //    GameObject textObject = new GameObject("Balance", typeof(RectTransform), typeof(TextMeshProUGUI));

    //    // Set the TextMeshPro object as a child of the Image object
    //    textObject.transform.SetParent(blocktransform, false); // 'false' to keep the local position

    //    // Configure the RectTransform of the TextMeshPro
    //    RectTransform textRect = textObject.GetComponent<RectTransform>();
    //    textRect.pivot = new Vector2(0.5f, 0.5f);
    //    textRect.anchoredPosition = Vector2.zero;
    //    textRect.sizeDelta = new Vector2(320f, 100f); // Match the size of the Image

    //    // Configure the TextMeshProUGUI component
    //    while (true) {
    //        TextMeshProUGUI tmp = textObject.GetComponent<TextMeshProUGUI>();
    //        tmp.text = Text;
    //        tmp.fontStyle = FontStyles.Bold;
    //        tmp.fontSize = 30;
    //        tmp.alignment = TextAlignmentOptions.Center;
    //        tmp.color = Color.black;  // Set text color (example)

    //        yield return null;
    //    }
    //}
    #endregion

    void buttonMaker(Transform blocktransform, Sprite ui) {
        Image buttonIMG = blocktransform.GetComponent<Image>();
        buttonIMG.sprite = ui;
        buttonIMG.type = Image.Type.Simple;
        buttonIMG.preserveAspect = true;
        buttonIMG.useSpriteMesh = true;
        buttonIMG.SetNativeSize();

    }

    void textInputFieldAdder(Transform blocktransform, int index) {

    }
}
