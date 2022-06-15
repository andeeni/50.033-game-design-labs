using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// give player jump boost for 5 seconds
public  class RedMushroom : MonoBehaviour, ConsumableInterface
{
	public  Texture t;
	public  void  consumedBy(GameObject player){
		// give player jump boost
		player.GetComponent<PlayerController>().upSpeed  +=  10;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f); // for 5 seconds
		player.GetComponent<PlayerController>().upSpeed  -=  10;
	}

    // when mario collide with mushroom
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // update UI
            CentralManager.centralManagerInstance.addPowerup(t, 0, this);
            GetComponent<Collider2D>().enabled  =  false;
        }
    }
}
