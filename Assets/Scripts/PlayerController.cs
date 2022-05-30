using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    void Awake(){

    }
    // Start is called before the first frame update

    public float speed;
    public float maxSpeed = 10;

    public float upSpeed = 20;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightSate = true;

    private bool onGroundState = true;

    // public Transform enemyLocation;
    // public Text scoreText;
    // private int score = 0;
    private bool countScoreState = false;

    private  Animator marioAnimator;

    private AudioSource marioAudio;

    void Start()
    {
        //set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        //init sprite render of mario object
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator  =  GetComponent<Animator>();

        marioAudio = GetComponent<AudioSource>();
    }

//called when collision happened with the Ground
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Ground")){
            onGroundState = true;
            //Debug.Log("onGroundState is: "+ onGroundState);
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false;
            // scoreText.text = "Score: " + score.ToString();
        };

        if(col.gameObject.CompareTag("Obstacles") && Mathf.Abs(marioBody.velocity.y) < 0.01f){
            //reset
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

//called when mario collide with enemy
     void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Gomba!");
            //this line go back to menu
            //Application.LoadLevel(0);

            //get the UI layer back: button, filter
            SceneManager.LoadScene("SampleScene");

        }
    }

    void  PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }


    //event callback 
    //things to do with physics engine
    void FixedUpdate(){
        //force will result in sliding effect
/*      float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal,0);
        marioBody.AddForce(movement*speed); */

        //stop moving object immediately when key is up
        if (Input.GetKeyUp("a")||Input.GetKeyUp("d")){
            marioBody.velocity = Vector2.zero;
        }
        //dynamic rigidbody
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(Mathf.Abs(moveHorizontal)>0){
            Vector2 movement = new Vector2(moveHorizontal,0);
            if(marioBody.velocity.magnitude <maxSpeed){
                marioBody.AddForce(movement*speed);
            }
        }
        //make object jump when spacebar pressed
        //sample code
        if (Input.GetKeyDown("space") && onGroundState==true){
            marioBody.AddForce(Vector2.up*upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            //Debug.Log("Mario is jumping");
            //Debug.Log("onGroundState is: "+ onGroundState);
            countScoreState = true; 
        }

    }


    // Update is called once per frame
    void Update()
    {
        //set which side mario is facing
        if(Input.GetKeyDown("a")&& faceRightSate==true){
            faceRightSate = false;
            marioSprite.flipX = true;
            //enable the onSkid trigger
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
               marioAnimator.SetTrigger("onSkid"); 
            }
        }
        if(Input.GetKeyDown("d")&& faceRightSate==false){
            faceRightSate = true;
            marioSprite.flipX = false;
            //enable the onSkid trigger
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
               marioAnimator.SetTrigger("onSkid"); 
            }
        }

        // always update the xSpeed parameter to match Mario’s current speed along the x-axis.
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        // To handle Mario’s jumping state, set the animator’s onGround parameter to match the current onGroundState value whenever it’s changed in the script
        marioAnimator.SetBool("onGround", onGroundState);

    }
}    
