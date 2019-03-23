using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceTrigger : MonoBehaviour {
    public bool isTriggered;
    public bool hasBeenTrig;
    // Use this for initialization
    void Start () {
        isTriggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTriggered = true;
        }
            
        
    }
}
