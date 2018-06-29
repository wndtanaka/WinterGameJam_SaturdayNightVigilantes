using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 5;
    float inputH;
    float inputV;

    public GameObject otherPlayer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Player 1")
        {
            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(inputH, 0, inputV) * speed * Time.deltaTime);
        }

        if (gameObject.name == "Player 2")
        {
            inputH = Input.GetAxis("Horiz2");
            inputV = Input.GetAxis("Vert2");

            transform.Translate(new Vector3(inputH, 0, inputV) * speed * Time.deltaTime);
        }

        transform.LookAt(otherPlayer.transform);
    }
}
