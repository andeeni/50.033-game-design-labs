using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public  Rigidbody2D rigidBody;
    // private int direction = 1;
    // private bool move = true;
    private float speed = 2.0f;
    private Vector2 currentPosition;
    private Vector2 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // upwards force (Vector2.up * 20) to be applied on the mushroom is automatically set as the amount of TOTAL force to be applied over ONE second (50 physics frame).
        rigidBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }


    // TODO: collide normally with bricks or Ground, so it will travel above the bricks or ground normally
    // TODO: will not collide with the TopCollider GameObject (child of HittableSimple) so as not to affect the spring motion of the box ---> Ensure the mushroom’s Layer do not collide with the TopCollider’s Layer

    
    // void  OnCollisionEnter2D(Collision2D col)
    // {
    //     // TODO: change direction when colliding with obstacles
    //     if (col.gameObject.CompareTag("Obstacles") ){
    //         direction = direction * (-1);
    //     }

    //     // TODO: stop moving when it collides with Mario 
    //     if (col.gameObject.CompareTag("Player") ){
    //         move = false;
    //     }
    // }

    // // TODO: randomly move to the left or to the right at a constant speed
    // private void FixedUpdate()
    // {
    //     if (move)
    //     {
    //         rigidBody.velocity = new Vector2(speed * direction, rigidBody.velocity.y);
    //     }
    // }


    // void  OnBecameInvisible(){
    //     Destroy(gameObject);	
    // }



    // Update is called once per frame
    void Update()
    {
        Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
rigidBody.MovePosition(nextPosition);
    }
}
