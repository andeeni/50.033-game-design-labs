using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void  OnTriggerEnter2D(Collider2D col){
	if (col.gameObject.CompareTag("Player")){
        CentralManager.centralManagerInstance.increaseScore();
        this.transform.gameObject.SetActive(false);
	}
}

}
