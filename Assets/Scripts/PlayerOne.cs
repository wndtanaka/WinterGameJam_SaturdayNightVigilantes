using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : Player
{
    private static PlayerOne instance;
    public static PlayerOne Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerOne>();
            }
            return instance;
        }
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
