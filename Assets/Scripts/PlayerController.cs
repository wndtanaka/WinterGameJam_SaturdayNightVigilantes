using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 5;
    float inputH;
    float inputV;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(inputH, 0, inputV) * speed * Time.deltaTime);
    }
}
