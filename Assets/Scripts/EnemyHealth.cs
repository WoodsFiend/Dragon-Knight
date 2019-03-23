using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int health = 1;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(health == 0)
        {
            StartCoroutine("Die");
        }
		
	}
    IEnumerator Die()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
