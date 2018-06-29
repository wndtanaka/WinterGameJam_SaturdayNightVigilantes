using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 5;
    float inputH;
    float inputV;

    public GameObject otherPlayer;

    public GameObject rightHand, leftHand, punchTarget, rHandOrigin, lHandOrigin;
    public float punchSpeed;

    // Use this for initialization
    void Start()
    {
        punchSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (gameObject.name == "Player 1")
        {
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
        }

        if (gameObject.name == "Player 2")
        {
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
        }

        PlayerPunch();

        transform.LookAt(otherPlayer.transform);
    }

    void PlayerPunch()
    {
        if (gameObject.name == "Player 1")
        {
            if (Input.GetKey(KeyCode.G))
            {
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
            }

            if (!Input.GetKey(KeyCode.G))// && rightHand.transform.position != rHandOrigin.transform.position)
            {
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed * Time.deltaTime);
            }
        }

        if (gameObject.name == "Player 2")
        {
            if (Input.GetKey(KeyCode.Keypad2))
            {
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, punchTarget.transform.position, punchSpeed * Time.deltaTime);
            }

            if (!Input.GetKey(KeyCode.Keypad2))// && rightHand.transform.position != rHandOrigin.transform.position)
            {
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rHandOrigin.transform.position, punchSpeed * Time.deltaTime);
            }
        }
    }
}
