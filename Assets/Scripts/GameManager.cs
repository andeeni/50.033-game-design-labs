using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
	private  int playerScore =  0;

    // delegate - ref pointer to a method
    public  delegate  void gameEvent();
    // To allow other scripts to subscribe to this delegate, we need to create an instance of that delegate, using the EVENT keyword
    public  static  event  gameEvent OnPlayerDeath;
    public  static  event  gameEvent OnEnemyDeath;
	
    
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
