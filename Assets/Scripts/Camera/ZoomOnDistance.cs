using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnDistance : MonoBehaviour {
    
    public bool inFight = false; // If player is in fight change this

    public float maxZoom = 10; // maximum distance the camera can zoom out to
    public float minZoom = 5; // minimum distance the camera can zoom in to

    private GameObject playerOne;
    private GameObject playerTwo;

	// Use this for initialization
	void Start () {
        playerOne = GameObject.FindWithTag("Player One");
        playerTwo = GameObject.FindWithTag("Player Two");
    }
	
	// Update is called once per frame
	void Update () {
        float zoom = Vector3.Distance(playerOne.transform.position, playerTwo.transform.position);
        Debug.Log(zoom);
        if (inFight)
        {
            transform.position = new Vector3(0,0,-zoom);
            if(transform.position.z >= minZoom)
            {
                transform.position = Vector3.back * minZoom;
            }
            if(transform.position.z <= maxZoom)
            {
                transform.position = Vector3.back * maxZoom;
            }
        }
	}
}
