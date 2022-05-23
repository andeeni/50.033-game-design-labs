using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f; //max gomba patrol offset
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    // patrol up to 5.0 units to the left and to the right, and change direction accordingly when the max offset distance is reached
    void ComputeVelocity(){
        // divide supposed distance travelled with time, and then compute the position at each Time.fixedDeltaTime. 
        velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
    }
    void MoveGomba(){
        // then, move the enemy to the calculated position
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset) { // If Gomba isn’t too far away from its starting position yet
            // move gomba to the designated direction
            MoveGomba();
        } else { // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }
}
