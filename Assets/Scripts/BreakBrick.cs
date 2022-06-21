using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public  GameObject debris;

    //public GameObject gameObject;
    private bool broken = false;
    private GameObject coin;

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            debris.SetActive(true);

            // assume we have 5 debris per box
            // for (int x =  0; x<5; x++){
            //     Instantiate(debris, transform.position, Quaternion.identity);
            // }

            //Debug.Log(gameObject);
            //Debug.Log(gameObject.transform);

            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
            GetComponent<EdgeCollider2D>().enabled  =  false;

            coin = this.transform.parent.GetChild(2).gameObject;
		    coin.SetActive(true);
        }
        
    }
}
