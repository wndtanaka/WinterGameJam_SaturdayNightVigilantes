using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwo : Player
{
    private static PlayerTwo instance;
    public static PlayerTwo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTwo>();
            }
            return instance;
        }
    }

    [SerializeField]
    Image healthBar;
    [SerializeField]
    Image staminaBar;

    protected override void Update()
    {
        base.Update();
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, 5 * Time.deltaTime);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stamina / maxStamina, 5 * Time.deltaTime);
    }

    public void TrainingResult(GameChoice gameChoice, float amount)
    {
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
        }
    }
}
