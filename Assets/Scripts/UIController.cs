using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private UiConnector connector;

    private TextMeshProUGUI multiplierText;
    private GameObject startpanelcomp;
    private float betAmount = 1f;
    private float winAmount = 0f;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    void Start()
    {
        addListeners();
        startpanelcomp = PanelCreator.instance.getStartPanel();
        multiplierText = SelectionUIController.instance.MultiplierText;
    }
    
    private void addListeners() {
        connector.BetAmountInput.onValueChanged.AddListener(inputValue);
        connector.BetAmountInput.onEndEdit.AddListener(validateInput);
        connector.plusButton.onClick.AddListener(valueIncreased);
        connector.minusButton.onClick.AddListener(valuedecreased);
        connector.betButton.onClick.AddListener(applyBet);
        connector.collectButton.onClick.AddListener(collectWins);
    }

    private void applyBet() {
        Tiles.instance.gameReset();
        offUIButton();
        GameManager.instance.setbalance(-betAmount);
        MultiplierCalculator.ResetMultiplier();
        multiplierText.text = $"{MultiplierCalculator.getCalculatedMultiplier()}x";
    }
    
    private void collectWins() {
        winAmount = GameManager.instance.getCalculatedWinAmount();
        Tiles.instance.gameReset();
        GameManager.instance.setbalance(winAmount);
        connector.winsText.text = $"Wins : {winAmount}";
        onUiButtonAC();
        GameManager.instance.resetWinAmount();
        winAmount = 0f;
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
        connector.winsText.text = $"Wins : 0";
        connector.collectWinAmount.GetComponent<TextMeshProUGUI>().text = $"Collect \n {GameManager.instance.getCalculatedWinAmount()}";
        //connector.winsText.text = $"Wins : 0 \n {GameManager.instance.getbalance()}";
    }
    private void offUIButton() {
        startpanelcomp.SetActive(false);
        connector.plusButton.GetComponent<Button>().interactable = false;
        connector.minusButton.GetComponent<Button>().interactable = false;
        connector.BetAmountInput.interactable = false;
        connector.betButton.gameObject.SetActive(false);
        connector.collectButton.gameObject.SetActive(true);
        SelectionUIController.instance.setUIOff();
    }
    public void onUIButton() {
        connector.plusButton.GetComponent<Button>().interactable = true;
        connector.minusButton.GetComponent<Button>().interactable = true;
        connector.BetAmountInput.interactable = true;
        connector.betButton.gameObject.SetActive(true);
        connector.collectButton.gameObject.SetActive(false);
        SelectionUIController.instance.setUIOn();
    }
    public void onUiButtonAC() {
        connector.plusButton.GetComponent<Button>().interactable = true;
        connector.minusButton.GetComponent<Button>().interactable = true;
        connector.BetAmountInput.interactable = true;
        connector.betButton.gameObject.SetActive(true);
        connector.collectButton.gameObject.SetActive(false);
        SelectionUIController.instance.setUIOn();
        Tiles.instance.buttonIntractablityOff();
    }
    public float getBetAmount() {
        return betAmount;
    }
}
