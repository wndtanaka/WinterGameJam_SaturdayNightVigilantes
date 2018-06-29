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
        #region Debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.instance.GameTime();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.instance.BreakTime();
        }
        #endregion

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
        if (otherPlayer != null)
        {
            transform.LookAt(otherPlayer.transform);
        }

    }
}
