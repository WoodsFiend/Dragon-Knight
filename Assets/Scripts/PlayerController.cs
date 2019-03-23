using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float maxSpeed = 10f;
    public float jumpPower = 25f;
    public float dashMult=1.5f;
    bool facingRight = true;
    public bool isGrounded = true;
    public GameObject slashHitbox;
    public GameObject stabHitbox;
    Animator anim;
    public int health = 10;
    float move;
    public bool isStab;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        move = 0;
        isStab = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (health > 0)
        {
            //gets movement from A D keys
            move = Input.GetAxis("Horizontal");
            //sets the speed for animator to perform run animation
            anim.SetFloat("Speed", Mathf.Abs(move));
            // applys movement based on input direction
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            //checks to flip character left and right
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();

            //checks if the player is stabbing
            if (isStab)
            {
                //adds dash force to player for stab
                GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * maxSpeed * dashMult, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
	}
    private void Update()
    {
        //checks if player is dead
        if (health <= 0)
        {
            //player dies
            anim.SetBool("Die", true);
            StartCoroutine("waitDeath");
        }
        //JUMP
        if (Input.GetKeyDown("space") && isGrounded==true && health > 0)
        {
            Jump();
        }
        //Slash
        if (Input.GetKeyDown("w") && health > 0)
        {
            Slash();
        }
        //Cast Spell
        if (Input.GetKeyDown("r") && health > 0)
        {
            Cast();
        }
        //Stab
        if (Input.GetKeyDown("e") && health > 0)
        {
            Stab();
        }
        //Block ON
        if (Input.GetKeyDown("f") && health > 0)
        {
            BlockOn();
        }
        //Block OFF
        if (Input.GetKeyUp("f") && health > 0)
        {
            BlockOff();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //player standing on ground
        if (col.gameObject.tag == "Ground") 
            isGrounded = true;
        //player takes damage
        if (col.gameObject.tag == "Damage" || col.gameObject.tag == "Enemy")
        {
            //health reduced by 1
            if (health > 1)
            {
                health = health - 1;
                anim.SetTrigger("Stun");
            }
            //player dies
            else
            {
                health = health - 1;
                anim.SetBool("Die", true);
                StartCoroutine("waitDeath");
            }
        }
        

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        //check if player in the air
        if (col.gameObject.tag == "Ground")
            isGrounded = false;
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            isGrounded = true;
    }
    //used to flip character direction
    void Flip()
    {
        //flips bool
        facingRight = !facingRight;
        //gets local transform
        Vector3 theScale = transform.localScale;
        //flips local transform direction
        theScale.x *= -1;
        //applys change to local scale
        transform.localScale = theScale;
    }
    //used to perform slash attack
    void Slash()
    {
        anim.SetTrigger("Attack");
        //checks if enemy is within the hitbox
        bool isEnemy = slashHitbox.GetComponent<HitBox>().isEnemy;
        if (isEnemy)
        {   
            //gets enemy gameobject and deals damage to health script
            slashHitbox.GetComponent<HitBox>().enemy.GetComponent<EnemyHealth>().health -= 1;
        }
    }
    void Stab()
    {
        anim.SetTrigger("Stab");
        //duration of the dash for stabbing
        StartCoroutine("Stabbing");
        //checks if enemy is within the hitbox
        bool isEnemy = stabHitbox.GetComponent<HitBox>().isEnemy;
        if (isEnemy)
        {
            //gets enemy game object and applies damage to health script
            stabHitbox.GetComponent<HitBox>().enemy.GetComponent<EnemyHealth>().health -= 1;
        }
    }
    //flips boolean for duration of stab dash
    IEnumerator Stabbing()
    {
        isStab = true;
        yield return new WaitForSeconds(0.1f);
        isStab = false;
    }
    void BlockOn()
    {
        anim.SetBool("Block",true);
    }
    void BlockOff()
    {
        anim.SetBool("Block", false);
    }
    void Jump()
    {   
        //applies upward force to player to jump
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
        anim.SetTrigger("Jump");
    }
    void Cast()
    {
        anim.SetTrigger("Cast");
    }
    void Die()
    {
        anim.SetBool("Die", true);
    }
    void Stun()
    {
        anim.SetTrigger("Stun");
    }
    //timer to wait until animation end to destroy player
    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(2.9f);
        Destroy(this.gameObject);
    }
}
