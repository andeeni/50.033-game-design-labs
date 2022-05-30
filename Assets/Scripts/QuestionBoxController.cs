using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{

    public  Rigidbody2D rigidBody;
    public  SpringJoint2D springJoint;
    public  GameObject consummablePrefab; // the spawned mushroom prefab
    public  SpriteRenderer spriteRenderer;
    public  Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    private bool hit =  false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // If the EdgeDetector has collided with Mario, then it has to bounce
    // And spawn the ConsummableMushroomSimple instantly, exactly once
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") &&  !hit){
            hit  =  true;

            // ensure that we move this object sufficiently, add additional force 
            rigidBody.AddForce(new  Vector2(0, rigidBody.mass*20), ForceMode2D.Impulse);

            // spawn the mushroom prefab slightly above the box
            // this.transform.position.x is world position (of parent)
            Instantiate(consummablePrefab, new  Vector3(this.transform.position.x, this.transform.position.y  +  1.0f, this.transform.position.z), Quaternion.identity);

            // begin check to disable object's spring and rigidbody (whether box is still moving)
            StartCoroutine(DisableHittable());


            
            // TODO
            // springs out of the box once instantiated
        }
    }

    // return true when obj is stationary
    bool  ObjectMovedAndStopped(){
        return  Mathf.Abs(rigidBody.velocity.magnitude)<0.01;
    }

    // returns control to Unity until condition happens
    IEnumerator  DisableHittable(){
        // if box is still moving
        if (!ObjectMovedAndStopped()){ 
            yield  return  new  WaitUntil(() =>  ObjectMovedAndStopped());
        }
        // here, return control to unity
        spriteRenderer.sprite  =  usedQuestionBox; // change sprite to be "used-box" sprite
        rigidBody.bodyType  =  RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        this.transform.localPosition  =  Vector3.zero;
        springJoint.enabled  =  false; // disable spring
    }


    // Update is called once per frame
    // must run to completion before returning control to Unity
    void Update()
    {
        
    }
}
