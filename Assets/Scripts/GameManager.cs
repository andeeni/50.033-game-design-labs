using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Singleton Pattern
    private  static  GameManager _instance;
    // Getter
    public  static  GameManager Instance
    {
        get { return  _instance; }
    }
    public Text score;
    // public  TextMeshProUGUI score;
	private  int playerScore =  0;


    // delegate - ref pointer to a method
    public  delegate  void gameEvent();
    // To allow other scripts to subscribe to this delegate, we need to create an instance of that delegate, using the EVENT keyword
    public  static  event  gameEvent OnPlayerDeath;
    public  static  event  gameEvent OnEnemyDeath;


    private  void  Awake()
    {
        // check if the _instance is not this, means it's been set before, return
        if (_instance  !=  null  &&  _instance  !=  this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        // otherwise, this is the first time this instance is created
        _instance  =  this;
        // add to preserve this object open scene loading
        DontDestroyOnLoad(this.gameObject); // only works on root gameObjects
    }
	
    
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();

        OnEnemyDeath();
	}

    public  void  damagePlayer(){
        //cast delegate
        OnPlayerDeath(); // which calls EnemyRejoice() and PlayerDiesSequence() (bc they are subscribed to event)
    }

    
}
