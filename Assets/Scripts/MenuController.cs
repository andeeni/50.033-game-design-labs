using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    //iterate children of UI and disable them
    public void StartButtonClicked() //public so will appear in unity
    {
        // Debug.Log("test button");
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score") //disable all except scoretext
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f; //set the timescale of the game to 0 in the beginning and set it to 1 after button is pressed
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
