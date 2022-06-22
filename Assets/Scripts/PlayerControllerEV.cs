using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;

    private Rigidbody2D marioBody;
    private BoxCollider2D marioCollider;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudioSource;

    private bool onGroundState = true;
    private bool faceRightState = true;
    //public CustomCastEvent onPlayerCast;

    void Start()
    {
	    Application.targetFrameRate =  30;
        
	    marioBody = GetComponent<Rigidbody2D>();
        marioCollider = GetComponent<BoxCollider2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudioSource = GetComponent<AudioSource>();
        //Debug.Log("Start() checkpoint 1");

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;

        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    void FixedUpdate()
    {
      if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
      {
          // stop
          marioBody.velocity = Vector2.zero;

      }
    
      // dynamic rigidbody
      float moveHorizontal = Input.GetAxis("Horizontal");
      if (Mathf.Abs(moveHorizontal) > 0)
      {
          Vector2 movement = new Vector2(moveHorizontal, 0);
          if (marioBody.velocity.magnitude < marioMaxSpeed.Value) 
          {
              marioBody.AddForce(movement * force);
          }
      }

      if (Input.GetKeyDown("space") && onGroundState) 
      {
          marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
          onGroundState = false;
          marioAnimator.SetBool("onGround", onGroundState);
          // countScoreState = true;
      }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) 
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            // countScoreState = false;
            // scoreText.text = "SCORE: " + score.ToString();
        }

        if (col.gameObject.CompareTag("Obstacles")) // && Mathf.Abs(marioBody.velocity.y) < 0.01f
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void  PlayerDiesSequence() {
        // Mario dies
        Debug.Log("Mario dies...");
        marioAnimator.SetBool("onDeath", true);
        marioCollider.enabled = false;
        marioBody.gravityScale = 10;
        marioBody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
        StartCoroutine(dead());
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(5.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }

    void PlayJumpSound() 
    {
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        if (Input.GetKeyDown("a") && faceRightState) 
        {
            faceRightState = false;
            marioSprite.flipX = true;

            // check speed to skid
            if (Mathf.Abs(marioBody.velocity.x) > 0.5)
            {
                marioAnimator.SetTrigger("onSkid");
                // Debug.Log("skid left");
            }
        }

        if (Input.GetKeyDown("d") && !faceRightState) 
        {
            faceRightState = true;
            marioSprite.flipX = false;

            // check speed to skid
            if (Mathf.Abs(marioBody.velocity.x) > 0.5)
            {
                marioAnimator.SetTrigger("onSkid");
                // Debug.Log("skid right");
            }

        }

/*         if (Input.GetKeyDown("z")) {
            // CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
            onPlayerCast.Invoke(KeyCode.Z);
        }

        if (Input.GetKeyDown("x")) {
            // CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
            onPlayerCast.Invoke(KeyCode.X);
        } */

    }

}