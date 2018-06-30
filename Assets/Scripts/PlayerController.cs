﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerType playerType;

    float speed = 5;
    float inputH;
    float inputV;

    public GameObject otherPlayer;

    public GameObject rightHand, leftHand, punchTarget, rHandOrigin, lHandOrigin;
    public float punchSpeed;

    public bool playerIsPunching, hasPunchReset;

    public bool playerIsTouching;

    public float playerDistance;

    public GameObject boxingRing;
    public float ringRadius;

    // Use this for initialization
    void Start()
    {
        punchSpeed = 4f;

        playerIsPunching = false;
        hasPunchReset = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region Debug
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    GameManager.instance.BreakTime();
        //}
        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    GameManager.instance.GameTime();
        //}
        #endregion

        playerDistance = Vector3.Distance(otherPlayer.transform.position, transform.position);

        PlayerMovement();
        PlayerPunch();
    }

    void PlayerMovement()
    {

        switch (playerType)
        {
            case PlayerType.PlayerOne:
                //inputH = Input.GetAxis("Horizontal");
                //inputV = Input.GetAxis("Vertical");

                //transform.Translate(new Vector3(inputH, 0, inputV) * speed * Time.deltaTime);
                if (!playerIsPunching)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        //rb.AddForce(transform.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                        //rb.AddForce(-transform.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(-Vector3.right * speed * Time.deltaTime);

                        transform.LookAt(otherPlayer.transform);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);

                        transform.LookAt(otherPlayer.transform);
                    }
                }

                /*if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D))
                {
                    rb.velocity = Vector3.zero;
                }*/

                

                break;
            case PlayerType.PlayerTwo:
                //inputH = Input.GetAxis("Horiz2");
                //inputV = Input.GetAxis("Vert2");

                //transform.Translate(new Vector3(inputH, 0, inputV) * speed * Time.deltaTime);
                if (!playerIsPunching)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);

                        transform.LookAt(otherPlayer.transform);
                    }

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        ///transform.Translate(-Vector3.right * speed * Time.deltaTime);

                        transform.LookAt(otherPlayer.transform);
                    }
                }
                break;
            default:
                break;
        }

        /*if (otherPlayer != null)
        {
            transform.LookAt(otherPlayer.transform);
        }*/
        InvisibleBounds();
    }

    void PlayerPunch()
    {
        switch (playerType)
        {
            case PlayerType.PlayerOne:

                if (hasPunchReset)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        playerIsPunching = true;
                        hasPunchReset = false;
                    }
                }

                if (!hasPunchReset)
                {
                    if (playerIsPunching)
                    {
                        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                    }

                    if (rightHand.transform.position == punchTarget.transform.position)
                    {
                        playerIsPunching = false;
                    }

                    if (!playerIsPunching)
                    {
                        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                    }

                    if (rightHand.transform.position == rHandOrigin.transform.position)
                    {
                        hasPunchReset = true;
                    }
                }
                break;
            case PlayerType.PlayerTwo:
                if (hasPunchReset)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                    if (Input.GetKeyDown(KeyCode.Keypad3))
                    {
                        playerIsPunching = true;
                        hasPunchReset = false;
                    }
                }

                if (!hasPunchReset)
                {
                    if (playerIsPunching)
                    {
                        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                    }

                    if (rightHand.transform.position == punchTarget.transform.position)
                    {
                        playerIsPunching = false;
                    }

                    if (!playerIsPunching)
                    {
                        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                    }

                    if (rightHand.transform.position == rHandOrigin.transform.position)
                    {
                        hasPunchReset = true;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(boxingRing.transform.position, ringRadius);
    }
    
    void InvisibleBounds()
    {
        Vector3 centerPosition = boxingRing.transform.position; //centre of boxing ring
        float distance = Vector3.Distance(transform.position, centerPosition); //distance from player to boxing ring centre

        if (distance > ringRadius) //If the distance is less than the radius, it is already within the circle.
        {
            Vector3 fromOriginToObject = transform.position - centerPosition;
            fromOriginToObject *= ringRadius / distance; //Multiply by radius, Divide by Distance
            transform.position = centerPosition + fromOriginToObject; //boxing ring centre + math from above
        }
    }
}
