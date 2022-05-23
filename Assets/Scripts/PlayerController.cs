using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed; //this will appear as input in unity
    public float maxSpeed = 10;
    public float upSpeed = 20;

    private Rigidbody2D marioBody;

    private float moveHorizontal;

    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    
    // for scoring
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>(); //return RIgidbody2D object

        // Instantiate the MarioSprite
        marioSprite = GetComponent<SpriteRenderer>();
    }

    //  use this instead of Update() for physics engine
    void  FixedUpdate()
    {
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          countScoreState = true; //check if Gomba is underneath

          Debug.Log("Mario is jumping");
        }
    }
    
    
    // called when mario lands on the ground
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("onGroundState is "+onGroundState);
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        };
    }

    // collide with enemy (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            SceneManager.LoadScene("mario");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score

        Debug.Log("onGroundState is "+onGroundState+ " countscorestate is "+countScoreState);
        if (!onGroundState && countScoreState)
        {
            Debug.Log("distance between objects: " +Mathf.Abs(transform.position.x - enemyLocation.position.x));

            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                Debug.Log("distance <0.5f");
                countScoreState = false; 
                score++;
                Debug.Log("current score: "+ score);
            }
            else{
                Debug.Log("distance >=0.5f");
            }
        }




    }
}
