using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellerAi : MonoBehaviour {
    public Transform[] PatrolPoints;
    public float speed;
    public bool facingRight = true;
    public float lookTime;
    public int lookAmount;
    Transform currentPatrolPoint;
    Animator anim;
    int currentPatrolIndex;
    float direction;
    bool stopped = false;
    int health;


	// Use this for initialization
	void Start () {
        //first index
        currentPatrolIndex = 0;
        //first patrol point
        currentPatrolPoint = PatrolPoints[currentPatrolIndex];
        //right direction facing
        direction = 1;
        //animator
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //gets health from health script
        health = this.GetComponent<EnemyHealth>().health;
        //check if patrol point getting far away
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) > 4f && !stopped && health > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("speed", Mathf.Abs(1));
        }
        //check if patrol point getting getting close
        else if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 4f && !stopped && health > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * (speed * 0.7f), GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("speed", Mathf.Abs(1));
        }
        //check if on patrol point
        if (Vector3.Distance(transform.position,currentPatrolPoint.position)<0.5f && !stopped && health > 0)
        {
            //check if not at end of patrol points array
            if (currentPatrolIndex + 1 < PatrolPoints.Length)
            {
                //get next patrol point
                currentPatrolIndex++;
            }
            //resets to first patrol point in array
            else
            {
                currentPatrolIndex = 0;
            }
            //re-assign current patrol point with current index
            currentPatrolPoint = PatrolPoints[currentPatrolIndex];
            //stop and look around
            StartCoroutine("Look");
            //stop walking animation
            anim.SetFloat("speed", Mathf.Abs(0));
        }

	}
    //used to flip character left and right
    void Flip()
    {
        if(direction == 1)
        {
            direction = -1;
        }
        else { direction = 1; }
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //used to stop the character and look around
    IEnumerator Look()
    {

        stopped = true;
        if (lookAmount%2 == 0)
        {
            lookAmount++;
        }
        for (int i = 0; i < lookAmount; i++)
        {
            yield return new WaitForSeconds(lookTime);
            Flip();
        }
        stopped = false;

    }
}
