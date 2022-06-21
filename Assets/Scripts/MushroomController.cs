using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private  Rigidbody2D rigidBody; 
    private Vector2 velocity; 
    private int direction = 1; // to be randomised to either or -1

    private bool collidePlayer = false;
    


    // Start is called before the first frame update
    void Start()
    {   
        this.rigidBody = GetComponent<Rigidbody2D>();

        // compute initial velocity
		ComputeVelocity();

        // upwards force (Vector2.up * 20) to be applied on the mushroom is automatically set as the amount of TOTAL force to be applied over ONE second (50 physics frame).
        this.rigidBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);

        var val = Random.value;
        if(val < 0.5f)
            direction = 1;
        else
            direction = -1;
    }

    void  ComputeVelocity()
	{
        velocity  =  new  Vector2(5, 0);
	}

    // randomly move to the left or to the right at a constant speed
    void  MoveMushroom()
	{
		this.rigidBody.MovePosition(this.rigidBody.position  +  velocity  *  Time.fixedDeltaTime * direction);
	}

    void OnCollisionEnter2D(Collision2D col) {
        // change direction when colliding with obstacles
        if (col.gameObject.CompareTag("Tubes") ){
            this.direction = this.direction * -1;
            MoveMushroom();
        }

        // stop moving when it collides with Mario 
        if (col.gameObject.CompareTag("Player") ){
            collidePlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (collidePlayer == false){
            ComputeVelocity();
            MoveMushroom();
        } 
    }

    
}
