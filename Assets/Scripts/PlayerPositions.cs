using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositions : MonoBehaviour
{

    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    public Vector3[] positons;
    public Vector3[] lookDirection;

    PlayerController ControllerOne;
    PlayerController ControllerTwo;

    public void ResetFightPosition()
    {
        ControllerOne = PlayerOne.GetComponent<PlayerController>();
        ControllerTwo = PlayerTwo.GetComponent<PlayerController>();

        PlayerOne.transform.position = positons[0];
        PlayerOne.transform.rotation = Quaternion.Euler(lookDirection[0]);

        PlayerTwo.transform.position = positons[1];
        PlayerTwo.transform.rotation = Quaternion.Euler(lookDirection[1]);

        ControllerOne.r_hasPunchReset = false;
        ControllerOne.l_hasPunchReset = false;

        ControllerTwo.r_hasPunchReset = false;
        ControllerTwo.l_hasPunchReset = false;
    }
}
