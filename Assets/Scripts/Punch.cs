using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public PlayerType playerType;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Score")
        {
            switch (playerType)
            {
                case (PlayerType.PlayerOne):
                    Debug.Log("Blue Hits!!!");
                break;
                case (PlayerType.PlayerTwo):
                    Debug.Log("Red Hits!!!");
                    break;
            }
        }
    }
}
