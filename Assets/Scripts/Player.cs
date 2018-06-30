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
        #region Debug
        switch (playerType)
        {
            case PlayerType.PlayerOne:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    TakeDamage(damage);
                    StaminaCost(20);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    TrainingResult(GameChoice.PullUp, 10);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    TrainingResult(GameChoice.Treadmill, 10);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    TrainingResult(GameChoice.PunchingBag,10);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    TrainingResult(GameChoice.Rest, 10);
                }
                break;
            case PlayerType.PlayerTwo:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    TakeDamage(damage);
                    StaminaCost(20);
                }
                break;
            default:
                break;
        }
        #endregion
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

    public void TrainingResult(GameChoice gameChoice, float amount)
    {
        switch (playerType)
        {
            case PlayerType.PlayerOne:
                switch (gameChoice)
                {
                    case GameChoice.PullUp:
                        range += amount;
                        break;
                    case GameChoice.Treadmill:
                        speed += amount;
                        break;
                    case GameChoice.PunchingBag:
                        damage += amount;
                        break;
                    case GameChoice.Rest:
                        health += 50;
                        stamina += 50;
                        break;
                    default:
                        break;
                }
                break;
            case PlayerType.PlayerTwo:
                switch (gameChoice)
                {
                    case GameChoice.PullUp:
                        range += amount;
                        break;
                    case GameChoice.Treadmill:
                        speed += amount;
                        break;
                    case GameChoice.PunchingBag:
                        damage += amount;
                        break;
                    case GameChoice.Rest:
                        health += 50;
                        stamina += 50;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
    public void StaminaCost(float amount)
    {
        stamina -= amount;
    }
}
