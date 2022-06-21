using System.Collections;
using UnityEngine;

public  class ChangeScene : MonoBehaviour
{
	public  AudioSource changeSceneSound;
	void  OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag  ==  "Player")
		{   
            StartCoroutine(LoadYourAsyncScene("MarioLevel2"));
			changeSceneSound.PlayOneShot(changeSceneSound.clip);
			
		}
	}

	IEnumerator  LoadYourAsyncScene(string sceneName)
	{
		yield  return  new  WaitUntil(() =>  !changeSceneSound.isPlaying);
		CentralManager.centralManagerInstance.changeScene();
	}
}
