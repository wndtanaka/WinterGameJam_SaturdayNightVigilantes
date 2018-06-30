using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{

    public bool inFight = false; // If player is in fight change this
    public bool isTraining = false;

    public float maxZoom = 10; // maximum distance the camera can zoom out to
    public float minZoom = 5; // minimum distance the camera can zoom in to

    public float positionOne = 10.01f, positionTwo = 20f; // Amount of Zoom 
    public float heightOne = 2f, heightTwo = 1.5f;
    public float lerpTime = 12; // Lerp time Multiplier

    private GameObject playerOne;
    private GameObject playerTwo;

    /*TESTING*/
    private const float DISTANCE_MARGIN = 1.0f;

    private Vector3 middlePoint;
    private float distanceFromMiddlePoint;
    private float distanceBetweenPlayers;
    private float cameraDistance;
    private float aspectRatio;
    private float fov;
    private float tanFov;

    /*TESTING*/


    void Start()
    {

        playerOne = GameObject.FindWithTag("Player One");
        playerTwo = GameObject.FindWithTag("Player Two");
        //cameraPivot = GameObject.Find("Camera Pivot").transform;


        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //View Changing
        if (isTraining)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, heightTwo, -positionTwo), lerpTime * Time.deltaTime);
        }
        else
        {
            if (!inFight)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, heightOne, -positionOne), lerpTime * Time.deltaTime);
            }
            else
            {
                Vector3 newCameraPos = Camera.main.transform.position;
                newCameraPos.x = middlePoint.x;
                Camera.main.transform.position = newCameraPos;

                // Find the middle point between players.
                Vector3 vectorBetweenPlayers = playerTwo.transform.position - playerOne.transform.position;
                middlePoint = playerOne.transform.position + 0.5f * vectorBetweenPlayers;

                // Calculate the new distance.
                distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
                cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

                if (cameraDistance <= minZoom)
                {
                    cameraDistance = minZoom;
                }
                if (cameraDistance >= maxZoom)
                {
                    cameraDistance = maxZoom;
                }
                //cameraPivot.position = Vector3.Lerp(cameraPivot.position, middlePoint, lerpTime * Time.deltaTime);
                

                // Set camera to new position.
                Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
                Camera.main.transform.position = Vector3.Lerp(transform.position,middlePoint + dir * (cameraDistance + DISTANCE_MARGIN), lerpTime * Time.deltaTime);
            }
        }
    }
}
