using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int balance = 100000;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    public int getbalance() {
        return balance;
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
