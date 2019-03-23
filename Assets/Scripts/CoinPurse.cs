using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPurse : MonoBehaviour {
    public int coins;
    public Text coinDisplay;
	// Use this for initialization
	void Start () {
        coins = 0;
	}
	
	// Update is called once per frame
	void Update () {
        coinDisplay.text = "Coins: " + coins;
	}
}
