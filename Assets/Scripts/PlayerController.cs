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

    public float upSpeed = 25;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightSate = true;

    private bool onGroundState = true;

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;

    private Animator marioAnimator;

    private AudioSource marioAudio;


    void Start()
    {
        //set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        //init sprite render of mario object
        marioSprite = GetComponent<SpriteRenderer>();

        //reference to current animator
        marioAnimator = GetComponent<Animator>();

        //reference to audio
        marioAudio = GetComponent<AudioSource>();
    }

//called when collision happened with the Ground
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Ground") && onGroundState==false){
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            //Debug.Log("onGroundState is: "+ onGroundState);
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
        }

        else if (col.gameObject.CompareTag("Obstacles")){
            if(Mathf.Abs(marioBody.velocity.y)<0.01f){
                //ResetMarioGround();
                //ResetMarioScore();
                onGroundState = true;
                marioAnimator.SetBool("onGround", onGroundState);
                countScoreState = false;
                scoreText.text = "Score: " + score.ToString();
            }
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

    void PlayJumpSound(){
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
            marioAnimator.SetBool("onGround", onGroundState);
        }

    }


    // Update is called once per frame
    void Update()
    {
        //to update xSpeed for animation to mario's current speed along the x
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        
        if(Input.GetKeyDown("a")&& faceRightSate==true){
            //set which side mario is facing
            faceRightSate = false;
            marioSprite.flipX = true;
            //display skid animation
            if(Mathf.Abs(marioBody.velocity.x)>1.0){
                marioAnimator.SetTrigger("onSkid");
            }
        }
        if(Input.GetKeyDown("d")&& faceRightSate==false){
            //set which side mario is facing
            faceRightSate = true;
            marioSprite.flipX = false;
            //display skid animation
            if(Mathf.Abs(marioBody.velocity.x)>1.0){
                marioAnimator.SetTrigger("onSkid");
            }
        }

        //to update onGround bool for animation
        marioAnimator.SetBool("onGround", onGroundState);
        //when jumping, and Gomba is near Mario and we havent register score
        if (onGroundState==false && countScoreState==true){
            if(Mathf.Abs(transform.position.x - enemyLocation.position.x)<0.5f){
                countScoreState = false;
                score ++;
                Debug.Log(score);
            }
        }

        
    }

}    
