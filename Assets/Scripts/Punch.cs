using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public PlayerType playerType;

    public Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "helmet")
        {
            player = other.GetComponentInParent<Player>();
            player.TakeDamage(10);
        }
    }
}
