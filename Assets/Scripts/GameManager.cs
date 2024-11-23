using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float balance = 100000;

    private int sessionseed;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sessionseed = Random.Range(0, 100000);
        
    }
    public float getbalance() {
        return balance;
    }
    public void setbalance(float betAmount) {
        balance += betAmount;
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
