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

    public float playerDistance;

    // Use this for initialization
    void Start()
    {
        punchSpeed = 2f;

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

                    if (Input.GetKey(KeyCode.D))
                    {
                    if (playerDistance >= 1.2f)
                    {
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    }
                        //rb.AddForce(transform.forward * speed * Time.deltaTime);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(-Vector3.forward * speed * Time.deltaTime);

                        //rb.AddForce(-transform.forward * speed * Time.deltaTime);
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

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (playerDistance >= 1.2f)
                    {
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    }
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                }
                break;
            default:
                break;
        }

        if (otherPlayer != null)
        {
            transform.LookAt(otherPlayer.transform);
        }
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
}
