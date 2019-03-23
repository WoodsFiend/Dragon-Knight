using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceController : MonoBehaviour {
    public float ffWait = 1f;
    public float jumpWait = 2f;
    public float jumpHeight = 40f;
    public float jumpSpeed = 2f;
    public bool isGrounded = false;
    bool isTriggered;
    bool hasFell = false;
    public GameObject fallTrigger;
    Animator anim;
    
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        isTriggered = fallTrigger.GetComponent<MaceTrigger>().isTriggered;
        if(isTriggered == true && hasFell == false)
        {
            hasFell = true;
            StartCoroutine("FirstFall");
        }
    }
 

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("Shake", false);
            StartCoroutine("Jump");
        }
        
        

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    IEnumerator FirstFall()
    {
        anim.SetBool("Shake", true);
        yield return new WaitForSeconds(ffWait);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

    }
    IEnumerator Jump()
    {
        if (isGrounded)
        {
            anim.SetTrigger("Reset");
            yield return new WaitForSeconds(jumpWait);
            anim.SetBool("Shake", true);
            //transform.position = originalPos;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight * jumpSpeed);
        }
        
    }
}
