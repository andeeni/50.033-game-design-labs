using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    public  GameConstants gameConstants;
	private  int moveRight;
	private  float originalX;
	private  Vector2 velocity ;
	private  Rigidbody2D enemyBody;
	private SpriteRenderer enemySprite;


    // Start is called before the first frame update
    void  Start()
	{
		enemyBody  =  GetComponent<Rigidbody2D>();
		enemySprite = GetComponent<SpriteRenderer>();
		
		// get the starting position
		originalX  =  transform.position.x;
	
		// randomise initial direction
		moveRight  =  Random.Range(0, 2) ==  0  ?  -1  :  1;
		
		// compute initial velocity
		ComputeVelocity();

        // subscribe to player event
        GameManager.OnPlayerDeath  +=  EnemyRejoice;    

		gameConstants.enemyRejoice = false;
	}
	
	void  ComputeVelocity()
	{
			velocity  =  new  Vector2((moveRight) *  gameConstants.maxOffset  /  gameConstants.enemyPatroltime, 0);
	}
  
	void  MoveEnemy()
	{
		enemyBody.MovePosition(enemyBody.position  +  velocity  *  Time.fixedDeltaTime);
	}

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with Mario
		if (other.gameObject.tag  ==  "Player"){
			// check if collides on top
			float yoffset = (other.transform.position.y  -  this.transform.position.y);
            //  check if the player’s y location is higher than the enemy’s (i.e Player is stomping the enemy from above)
			if (yoffset  >  0.75f){
				KillSelf();
			}
			else{
				// hurt player
                CentralManager.centralManagerInstance.damagePlayer();
			}
		}
	}

    void  KillSelf(){
		// enemy dies
		CentralManager.centralManagerInstance.increaseScore();
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
	}

    IEnumerator  flatten(){
		Debug.Log("Flatten starts");
		int steps =  5;
		float stepper =  1.0f/(float) steps;

		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield  break;
	}

    // animation when player is dead
    void  EnemyRejoice(){
        Debug.Log("Enemy killed Mario");
        // do whatever you want here, animate etc
        // ...

		gameConstants.enemyRejoice = true;

    }

	void  Update()
	{
		if (gameConstants.enemyRejoice == true)
		{
        enemySprite.flipX = !enemySprite.flipX;
        velocity = new Vector2(0,0);
      	}

		if (Mathf.Abs(enemyBody.position.x  -  originalX) <  gameConstants.maxOffset)
		{// move goomba
			MoveEnemy();
		}
		else
		{
			// change direction
			moveRight  *=  -1;
			ComputeVelocity();
			MoveEnemy();
		}
	}


    private void OnDestroy()
    {
		GameManager.OnPlayerDeath -= EnemyRejoice;

	}
}
