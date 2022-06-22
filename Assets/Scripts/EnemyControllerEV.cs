using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EnemyControllerEV : MonoBehaviour
{
    // private float maxOffset = 5.0f;
    // private float enemyPatroltime = 2.0f;

    public GameConstants gameConstants;
    private float originalX;
    private int moveRight;
    private bool dancing = false;

    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private BoxCollider2D enemyCollider;

    // events to subscribe
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;
    //private AudioSource stompedAudio;


    // Start is called before the first frame update
    void Start()
    {
        //stompedAudio = GetComponent<AudioSource>();
        enemyBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        originalX = transform.position.x;
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1; // randomly decide move right or left
        ComputeVelocity();
        // GameManager.OnPlayerDeath += EnemyRejoice;
    }

    void ComputeVelocity()
    {
        velocity = new Vector2( (moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }
    
    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (dancing) 
        {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
        
        if (Mathf.Abs(enemyBody.position.x - originalX) < 2.0f)
        {
            MoveGomba();
        }
        else 
        {
            // change dir
            moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }

    void  OnTriggerEnter2D(Collider2D other) {
        // check if it collides with Mario
        if (other.gameObject.tag == "Player"){
            Debug.Log("collided!");
            // check if collides on top
            
            float yoffset = (other.transform.position.y  -  this.transform.position.y);
            if (yoffset > 0.80f){
                KillSelf();
                onEnemyDeath.Invoke();
            }
            else{
                // hurt player
                // CentralManager.centralManagerInstance.damagePlayer();
                onPlayerDeath.Invoke();
            }
        }
	}
    

    void KillSelf(){
		// enemy dies
		// CentralManager.centralManagerInstance.increaseScore();
        //stompedAudio.PlayOneShot(stompedAudio.clip);
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
	}

    IEnumerator flatten(){
		Debug.Log("Flatten starts");
		int steps = 5;
		float stepper = 1.0f/(float) steps;

		for (int i = 0; i < steps; i++){
			this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield return null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield break;
	}

    public void PlayerDeathResponse()
    {
        Debug.Log("Enemy killed Mario!");
        moveRight = 0;
        dancing = true;
    }
}