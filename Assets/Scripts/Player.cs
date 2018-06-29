using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float health;
    float maxHealth = 100;
    public float stamina;
    float maxStamina = 100;

    public float damage = 10;
    public float speed;
    public float range;

    Image healthBar;
    Image staminaBar;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;

        if (this.gameObject.tag == "Player One")
        {
            healthBar = GameObject.FindGameObjectWithTag("Player One UI").transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
            staminaBar = GameObject.FindGameObjectWithTag("Player One UI").transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        }
        if (this.gameObject.tag == "Player Two")
        {
            healthBar = GameObject.FindGameObjectWithTag("Player Two UI").transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
            staminaBar = GameObject.FindGameObjectWithTag("Player Two UI").transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Debug
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage();
            StaminaCost();
        }
        #endregion
        if (health < maxHealth)
        {
            health += 1 * Time.deltaTime;
        }
        else if (health <= 0)
        {
            Debug.Log("Dead");
        }
        if (stamina < maxStamina)
        {
            stamina += 3 * Time.deltaTime;
        }


        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, 5 * Time.deltaTime);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stamina / maxStamina, 5 * Time.deltaTime);
    }

    void TakeDamage()
    {
        health -= damage;
    }
    void StaminaCost()
    {
        stamina -= 20;
    }
}
