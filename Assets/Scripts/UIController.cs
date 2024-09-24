using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private UiConnector connector;

    private GameObject startpanelcomp;
    private float betAmount = 1f;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    void Start()
    {
        addListeners();
        startpanelcomp = PanelCreator.instance.getStartPanel();
    }
    
    private void addListeners() {
        connector.BetAmountInput.onValueChanged.AddListener(inputValue);
        connector.BetAmountInput.onEndEdit.AddListener(validateInput);
        connector.plusButton.onClick.AddListener(valueIncreased);
        connector.minusButton.onClick.AddListener(valuedecreased);
        connector.betButton.onClick.AddListener(applyBet);
    }

    private void applyBet() {
        Tiles.instance.gameReset();
        offUIButton();
        GameManager.instance.setbalance(-betAmount);
    }

    #region INPUT HANDLER
    private void validateInput(string arg0) {
        if (string.IsNullOrEmpty(arg0) || !float.TryParse(arg0, out betAmount)) {
            // Set the default minimum value if input is invalid.
            betAmount = 1f;
            connector.BetAmountInput.text = betAmount.ToString();
        }
        else if (betAmount < 1f) {
            // Clamp to minimum value if it's less than 1.
            betAmount = 1f;
            connector.BetAmountInput.text = betAmount.ToString();
        }
        Debug.Log($"Validated bet amount: {betAmount}");
    }

    private void inputValue(string arg0) {
        betAmount = float.Parse(arg0);
        Debug.Log(betAmount.ToString());
        Debug.Log(arg0);
    }
    private void valueIncreased() {
        betAmount++;
        connector.BetAmountInput.text = betAmount.ToString();
    }
    private void valuedecreased() {
        if (betAmount <= 1f) {
            betAmount = 1f;
        }
        else {
            betAmount--;
        }
        connector.BetAmountInput.text = betAmount.ToString();
    }

    #endregion
    void Update()
    {
        connector.balanceText.text = $"Balance : \n {GameManager.instance.getbalance()}";
        connector.winsText.text = $"Balance : \n {GameManager.instance.getbalance()}";
    }
    private void offUIButton() {
        startpanelcomp.SetActive(false);
        connector.plusButton.GetComponent<Button>().interactable = false;
        connector.minusButton.GetComponent<Button>().interactable = false;
        connector.BetAmountInput.interactable = false;
        connector.betButton.interactable=false;
    }
    public void onUIButton() {
        connector.plusButton.GetComponent<Button>().interactable = true;
        connector.minusButton.GetComponent<Button>().interactable = true;
        connector.BetAmountInput.interactable = true;
        connector.betButton.interactable = true;
    }
}
