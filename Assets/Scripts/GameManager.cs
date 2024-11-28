using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float balance = 100000;

    private float winAmount = 0f;

    private int sessionSeed;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sessionSeed = Random.Range(0, 100000);
        
    }
    public float getbalance() {
        return balance;
    }
    public void setbalance(float betAmount) {
        balance += betAmount;
    }

    public void calculateWinAmount() {
        winAmount = UIController.Instance.getBetAmount() * MultiplierCalculator.getCalculatedMultiplier();
    } 
    public float getCalculatedWinAmount() {
        return winAmount;
    }

    public void resetWinAmount() {
        winAmount = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        GameManager.instance.calculateWinAmount();
    }
}
