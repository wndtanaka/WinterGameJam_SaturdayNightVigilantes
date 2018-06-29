using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{

    public bool inFight = false; // If player is in fight change this
    public bool isTraining = false;

    public float maxZoom = 10; // maximum distance the camera can zoom out to
    public float minZoom = 5; // minimum distance the camera can zoom in to
    
    public float positionOne = 10f, positionTwo = 20f; // Amount of Zoom 
    public float lerpTime = 12; // Lerp time Multiplier
    
    private GameObject playerOne;
    private GameObject playerTwo;


    void Start()
    {
        
        playerOne = GameObject.FindWithTag("Player One");
        playerTwo = GameObject.FindWithTag("Player Two");
    }

    // Update is called once per frame
    void Update()
    {
        //View Changing
        if (isTraining)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.back * positionTwo, lerpTime * Time.deltaTime);
        }
        else if (!inFight && !isTraining)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.back * positionOne, lerpTime * Time.deltaTime);
        }
        
        //Character Zooming
        float zoom = Vector3.Distance(playerOne.transform.position, playerTwo.transform.position);
        Debug.Log(zoom);
        if (inFight)
        {
            transform.position = new Vector3(0, 0, -zoom);
            if (transform.position.z >= minZoom)
            {
                transform.position = Vector3.back * minZoom;
            }
            if (transform.position.z <= maxZoom)
            {
                transform.position = Vector3.back * maxZoom;
            }
        }



    }
}
