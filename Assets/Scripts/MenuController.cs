using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    void Awake() {
        Time.timeScale = 0.0f;
    }

    //when start button is clicked, iterate each child of UI and disable them
    public void StartButtonClicked(){
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score"){
                Debug.Log("Child found. Name: "+ eachChild.name);
                //disable object
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
