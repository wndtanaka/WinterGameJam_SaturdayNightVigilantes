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

    public bool playerIsPunching, hasPunchReset;

    public bool playerIsTouching;

    public float playerDistance, redBackDistance, blueBackDistance;
    public GameObject redBackBorder, blueBackBorder;

    Player player;

    // Use this for initialization
    void Start()
    {
        punchSpeed = 4f;

        playerIsPunching = false;
        hasPunchReset = true;

        player = GetComponent<Player>();
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
        redBackDistance = Vector3.Distance(redBackBorder.transform.position, transform.position);
        blueBackDistance = Vector3.Distance(blueBackBorder.transform.position, transform.position);

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
                        if (playerDistance >= 1.15f)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }
                        //rb.AddForce(transform.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        if (blueBackDistance >= 1.3f)
                        {
                            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                        }
                        //rb.AddForce(-transform.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(-Vector3.right * speed * Time.deltaTime);

                        //transform.LookAt(otherPlayer.transform);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);

                        //transform.LookAt(otherPlayer.transform);
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
                        if (playerDistance >= 1.15f)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }
                    }

                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        if (redBackDistance >= 1.3f)
                        {
                            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                        }
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);

                        //transform.LookAt(otherPlayer.transform);
                    }

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        transform.Translate(-Vector3.right * speed * Time.deltaTime);

                        //transform.LookAt(otherPlayer.transform);
                    }
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
    }

    void PlayerPunch()
    {
        switch (playerType)
        {
            case PlayerType.PlayerOne:

                if (hasPunchReset)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                    if (player.stamina >= 15)
                    {
                        if (Input.GetKeyDown(KeyCode.H))
                        {
                            Player player = GetComponent<Player>();
                            player.StaminaCost(15);
                            playerIsPunching = true;
                            hasPunchReset = false;
                        }
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

                    if (player.stamina >= 15)
                    {
                        if (Input.GetKeyDown(KeyCode.Keypad3))
                        {
                            Player player = GetComponent<Player>();
                            player.StaminaCost(15);
                            playerIsPunching = true;
                            hasPunchReset = false;
                        }
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
}
