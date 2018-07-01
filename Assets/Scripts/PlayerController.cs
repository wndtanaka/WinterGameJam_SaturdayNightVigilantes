using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerType playerType;

    float speed = 5;
    float inputH;
    float inputV;

    public GameObject otherPlayer;

    public GameObject rightHand, leftHand, punchTarget, rHandOrigin, lHandOrigin;
    public float punchSpeed;
    public bool isHittingLeft;

    public bool playerIsPunching, r_hasPunchReset, l_hasPunchReset;

    public bool playerIsTouching;

    public float playerDistance;

    public GameObject boxingRing;
    public float ringRadius;

    Player player;
    Animator playerOne;
    Animator playerTwo;

    float timer;

    // Use this for initialization
    void Start()
    {
        punchSpeed = 4f;

        playerIsPunching = false;
        r_hasPunchReset = true;
        l_hasPunchReset = true;

        player = GetComponent<Player>();

        playerOne = GameObject.FindWithTag("Player One").GetComponent<Animator>();
        playerTwo = GameObject.FindWithTag("Player Two").GetComponent<Animator>();

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

                if (!playerIsPunching)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        playerOne.SetBool("MovingForward", true);
                    }
                    else
                    {
                        playerOne.SetBool("MovingForward", false);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                        playerOne.SetBool("MovingBack", true);
                    }
                    else
                    {
                        playerOne.SetBool("MovingBack", false);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(-Vector3.right * speed * Time.deltaTime);
                        playerOne.SetBool("MovingLeft", true);
                    }
                    else
                    {
                        playerOne.SetBool("MovingLeft", false);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                        playerOne.SetBool("MovingRight", true);
                    }
                    else
                    {
                        playerOne.SetBool("MovingRight", false);
                    }
                }

                break;
            case PlayerType.PlayerTwo:


                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    playerTwo.SetBool("MovingForward", true);
                }
                else
                {
                    playerTwo.SetBool("MovingForward", false);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                    playerTwo.SetBool("MovingBack", true);
                }
                else
                {
                    playerTwo.SetBool("MovingBack", false);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                    playerTwo.SetBool("MovingRight", true);
                }
                else
                {
                    playerTwo.SetBool("MovingRight", false);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(-Vector3.right * speed * Time.deltaTime);
                    playerTwo.SetBool("MovingLeft", true);
                }
                else
                {
                    playerTwo.SetBool("MovingLeft", false);
                }

                break;
            default:
                break;
        }

        transform.LookAt(otherPlayer.transform);

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
            #region Player One
            case PlayerType.PlayerOne:

               
                if (isHittingLeft)
                {
                    if (l_hasPunchReset)
                    {
                        leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, lHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                        if (Input.GetKeyDown(KeyCode.H) && player.stamina >= 15)
                        {
                            Player player = GetComponent<Player>();
                            playerOne.SetBool("isHitting", true);

                            player.StaminaCost(2);
                            playerIsPunching = true;
                            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                                playerOne.SetBool("isHittingLeft", true);

                            l_hasPunchReset = false;
                            isHittingLeft = Random.value > 0.5f;
                            //isHittingLeft = !isHittingLeft;
                        }
                    }
                    else
                    {
                        if (playerIsPunching)
                        {
                            leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                            playerOne.SetBool("isHittingLeft", false);
                        }

                        if (leftHand.transform.position == punchTarget.transform.position)
                        {
                            playerIsPunching = false;
                        }

                        if (!playerIsPunching)
                        {
                            leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, lHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                        }

                        if (leftHand.transform.position == lHandOrigin.transform.position)
                        {
                            l_hasPunchReset = true;
                        }
                    }
                }
                else
                {
                    if (r_hasPunchReset)
                    {
                        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                        if (Input.GetKeyDown(KeyCode.H) && player.stamina >= 15)
                        {
                            Player player = GetComponent<Player>();
                            playerOne.SetBool("isHitting", true);

                            player.StaminaCost(2);
                            playerIsPunching = true;
                            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                            {
                                playerOne.SetBool("isHittingRight", true);
                                Debug.Log("PlayerOne_isHittingRight is True");
                            }


                            r_hasPunchReset = false;
                            isHittingLeft = Random.value > 0.5f;
                            //isHittingLeft = !isHittingLeft;

                        }

                    }

                    if (!r_hasPunchReset)
                    {
                        if (playerIsPunching)
                        {
                            rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                            playerOne.SetBool("isHittingRight", false);
                            Debug.Log("PlayerOne_isHittingRight is false");


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
                            r_hasPunchReset = true;
                        }
                    }
                }


                break;
            #endregion
            #region Player Two
            case PlayerType.PlayerTwo:

                if (isHittingLeft)
                {
                    if (l_hasPunchReset)
                    {
                        leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, lHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                        if (Input.GetKeyDown(KeyCode.Keypad3) && player.stamina >= 15)
                        {
                            Player player = GetComponent<Player>();
                            playerTwo.SetBool("isHitting", true);

                            player.StaminaCost(2);
                            playerIsPunching = true;
                            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                            {
                                playerTwo.SetBool("isHittingLeft", true);
                                Debug.Log("isHittingleft is True");
                            }

                            l_hasPunchReset = false;
                            isHittingLeft = Random.value > 0.5f;
                            //isHittingLeft = !isHittingLeft;
                        }
                    }
                    else
                    {
                        if (playerIsPunching)
                        {
                            leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                            playerTwo.SetBool("isHittingLeft", false);
                            Debug.Log("isHittingleft is False");
                        }

                        if (leftHand.transform.position == punchTarget.transform.position)
                        {
                            playerIsPunching = false;
                        }

                        if (!playerIsPunching)
                        {
                            leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, lHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                        }

                        if (leftHand.transform.position == lHandOrigin.transform.position)
                        {
                            l_hasPunchReset = true;
                        }
                    }
                }
                else
                {

                    if (r_hasPunchReset)
                    {
                        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);

                        if (Input.GetKeyDown(KeyCode.Keypad3) && player.stamina >= 15)
                        {
                            Player player = GetComponent<Player>();
                            playerTwo.SetBool("isHitting", true);

                            player.StaminaCost(2);
                            playerIsPunching = true;
                            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                            {
                                playerTwo.SetBool("isHittingRight", true);
                            }


                            r_hasPunchReset = false;
                            isHittingLeft = Random.value > 0.5f;
                            //isHittingLeft = !isHittingLeft;
                        }

                    }

                    else
                    {
                        if (playerIsPunching)
                        {
                            rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                            playerTwo.SetBool("isHittingRight", false);
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
                            r_hasPunchReset = true;
                        }
                    }
                }
                break;
            #endregion
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
