using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public  Rigidbody2D rigidBody; 
    private float velocity = 4.0f; // mushroom moves at constant speed
    private int direction = 1; // to be randomised to either or -1
    private bool isMoving = true;


    //private bool onGroundState = false;


    // Start is called before the first frame update
    void Start()
    {   
        rigidBody = GetComponent<Rigidbody2D>();

        // upwards force (Vector2.up * 20) to be applied on the mushroom is automatically set as the amount of TOTAL force to be applied over ONE second (50 physics frame).
        rigidBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);

        var val = Random.value;
        if(val < 0.5f)
            direction = 1;
        else
            direction = -1;
    }

        // TODO: randomly move to the left or to the right at a constant speed
    private void FixedUpdate()
    {
        if (isMoving)
        {
            rigidBody.velocity = new Vector2(velocity * direction, rigidBody.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        // TODO: change direction when colliding with obstacles
        if (col.gameObject.CompareTag("Tubes") ){
            direction = direction * (-1);
        }

        // TODO: stop moving when it collides with Mario 
        if (col.gameObject.CompareTag("Player") ){
            isMoving = false;

        }
    }

    // void  OnBecameInvisible(){
    //     Destroy(gameObject);	
    // }

    
}
