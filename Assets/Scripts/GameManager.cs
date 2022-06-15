using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
	private  int playerScore =  0;
	
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
	}

    public  void  damagePlayer(){
        //cast delegate
        OnPlayerDeath(); // which calls EnemyRejoice() and PlayerDiesSequence() (bc they are subscribed to event)
    }

    // delegate - ref pointer to a method
    public  delegate  void gameEvent();

    // To allow other scripts to subscribe to this delegate, we need to create an instance of that delegate, using the EVENT keyword
    public  static  event  gameEvent OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
