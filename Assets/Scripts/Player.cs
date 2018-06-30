using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerType playerType;

    public float health;
    float maxHealth = 100;
    public float stamina;
    float maxStamina = 100;

    public float damage = 10;
    public float speed = 10;
    public float range = 10;

    Image healthBar;
    Image staminaBar;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;

        // getting the Health Bar and Stamina Bar of a players referring to their enums
        switch (playerType)
        {
            case PlayerType.PlayerOne:
                healthBar = GameObject.FindGameObjectWithTag("Player One UI").transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
                staminaBar = GameObject.FindGameObjectWithTag("Player One UI").transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
                break;
            case PlayerType.PlayerTwo:
                healthBar = GameObject.FindGameObjectWithTag("Player Two UI").transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
                staminaBar = GameObject.FindGameObjectWithTag("Player Two UI").transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < maxHealth)
        {
            health += 1 * Time.deltaTime;
        }
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
        if (stamina < maxStamina)
        {
            stamina += 3 * Time.deltaTime;
        }
        if (stamina <= 0)
        {
            stamina = 0;
        }

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, 5 * Time.deltaTime);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stamina / maxStamina, 5 * Time.deltaTime);
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
