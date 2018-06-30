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

    public bool isDead = false;

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
            health += 1 * Time.deltaTime;
        }
        if (health <= 0)
        {
            isDead = true;
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        if (stamina < maxStamina)
        {
            stamina += 3 * Time.deltaTime;
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
