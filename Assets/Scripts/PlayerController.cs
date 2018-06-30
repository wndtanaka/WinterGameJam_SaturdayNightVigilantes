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

    // Use this for initialization
    void Start()
    {
        punchSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        #region Debug
        if (Input.GetKey(KeyCode.Alpha1))
        {
            GameManager.instance.BreakTime();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            GameManager.instance.GameTime();
        }
        #endregion
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
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                }
                break;
            case PlayerType.PlayerTwo:
                //inputH = Input.GetAxis("Horiz2");
                //inputV = Input.GetAxis("Vert2");

                //transform.Translate(new Vector3(inputH, 0, inputV) * speed * Time.deltaTime);

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
                if (Input.GetKey(KeyCode.G))
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                }

                if (!Input.GetKey(KeyCode.G))// && rightHand.transform.position != rHandOrigin.transform.position)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.H))
                {
                    leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                }

                if (!Input.GetKey(KeyCode.H))// && rightHand.transform.position != rHandOrigin.transform.position)
                {
                    leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, lHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                }
                break;
            case PlayerType.PlayerTwo:
                if (Input.GetKey(KeyCode.Keypad2))
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                }

                if (!Input.GetKey(KeyCode.Keypad2))// && rightHand.transform.position != rHandOrigin.transform.position)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.Keypad3))
                {
                    leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
                }

                if (!Input.GetKey(KeyCode.Keypad3))// && rightHand.transform.position != rHandOrigin.transform.position)
                {
                    leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, lHandOrigin.transform.position, punchSpeed / 2 * Time.deltaTime);
                }
                break;
            default:
                break;
        }
    }
}
