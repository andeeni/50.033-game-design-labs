using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public  GameObject gameManagerObject;
	private  GameManager gameManager;
	public  static  CentralManager centralManagerInstance;
	
	void  Awake(){
		centralManagerInstance  =  this;
	}
	// Start is called before the first frame update
	void  Start()
	{
		gameManager  =  gameManagerObject.GetComponent<GameManager>();
	}

	public  void  increaseScore(){
		gameManager.increaseScore();
	}

    // player collides with enemy sideways
    public  void  damagePlayer(){
        gameManager.damagePlayer();
    }
}
