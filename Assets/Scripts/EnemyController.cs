using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    private int moveRight = -1;


    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
    }
    void moveGomba(){
        enemyBody.MovePosition(enemyBody.position+velocity*Time.fixedDeltaTime);
    }

    //called when collision happened with wall
/*     void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Wall")){
            moveRight *= -1;
            ComputeVelocity();
            moveGomba();
        }
    } */

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(enemyBody.position.x - originalX)<maxOffset){
            moveGomba();
        }
        else{
            moveRight *= -1;
            ComputeVelocity();
            moveGomba();
        }
    }
}
