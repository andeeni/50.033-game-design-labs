using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private float speed =5.0f;

    private Vector2 velocity;

    private float originalY;
    private Vector2 currentPosition;
    private int  moveRight = 1;
    private Rigidbody2D mushroom;

    private bool onGroundState = false;




    // Start is called before the first frame update
    void Start()
    {   
        originalY = transform.position.y;
        
        mushroom = GetComponent<Rigidbody2D>();
        currentPosition = mushroom.transform.position;

        mushroom.AddForce(Vector2.up*5, ForceMode2D.Impulse);

        ComputeVelocity();
        
        //Random rnd = new Random();
        //currentDirection = rnd.Next(-1,1);
        

    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Obstacles")){
            ComputeVelocity();
            moveMushroom();
        }
        else if (col.gameObject.CompareTag("Tubes")){
            moveRight*=-1;
            ComputeVelocity();
            moveMushroom();
        }
        else if (col.gameObject.CompareTag("Player") ){
            //mushroom.velocity = Vector2.zero;
            speed =0f;
        }
    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*speed, 0);
    }
    void moveMushroom(){
        mushroom.MovePosition(mushroom.position+velocity*Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //check if mushroom is in air
        if(Mathf.Abs( originalY- mushroom.position.y )!=0){
            onGroundState = false;
        }
        ComputeVelocity();
        moveMushroom();




        
    }
}
