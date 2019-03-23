using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameEventController : MonoBehaviour {
    GameObject player;
    public int coinMaxCollected=10;
    public Text endText; 
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<CoinPurse>().coins == coinMaxCollected)
        {
            endText.text = "You Win!";
        }
        if (player.GetComponent<PlayerController>().health <= 0)
        {
            endText.text = "You Died!";
        }
	}
}
