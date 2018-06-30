using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerType playerType;

    public float health;
    protected float maxHealth = 100;
    public float stamina;
    protected float maxStamina = 100;

    public float damage = 10;
    public float speed = 10;
    public float range = 10;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health < maxHealth)
        {
            health += .5f * Time.deltaTime;
        }
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        if (stamina < maxStamina)
        {
            stamina += .5f * Time.deltaTime;
        }
        if (stamina <= 0)
        {
            stamina = 0;
        }
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
        }

    }
    public void StaminaCost(float amount)
    {
        stamina -= amount;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
