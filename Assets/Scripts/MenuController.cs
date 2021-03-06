using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    void Awake()
    {
        Time.timeScale = 0.0f;
    }


    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {   // Only Score will remain as a UI Element
            if (eachChild.name != "Score" && eachChild.name != "Image" && eachChild.name != "Panel") 
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                Debug.Log("Child Disabled. Name: " + eachChild.name);
            } 
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
