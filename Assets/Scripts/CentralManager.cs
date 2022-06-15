using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public  static  CentralManager centralManagerInstance;

    // add reference to GameManager
    public  GameObject gameManagerObject;
	private  GameManager gameManager;
	

    // add reference to PowerupManager
    public  GameObject powerupManagerObject;
    private  PowerupManager powerUpManager;
	
	void  Awake(){
		centralManagerInstance  =  this;
	}
	// Start is called before the first frame update
	void  Start()
	{
		gameManager  =  gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerupManager>();
	}

	public  void  increaseScore(){
		gameManager.increaseScore();
	}

    // player collides with enemy sideways
    public  void  damagePlayer(){
        gameManager.damagePlayer();
    }


    // called by RedMushroom.cs and OrangeMushroom.cs
    public  void  consumePowerup(KeyCode k, GameObject g){
        powerUpManager.consumePowerup(k,g);
    }
    // called by PlayerController.cs in Update()
    public  void  addPowerup(Texture t, int i, ConsumableInterface c){
        powerUpManager.addPowerup(t, i, c); 
    }
}
