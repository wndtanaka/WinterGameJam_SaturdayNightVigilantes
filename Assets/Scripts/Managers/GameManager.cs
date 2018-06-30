using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // making GameManager a singleton, so accessible easily using instance
    public static GameManager instance;

    [SerializeField]
    CanvasGroup breakUI;
    [SerializeField]
    CanvasGroup gameUI;

    [Header("Game Time")]
    [SerializeField]
    float gameTime = 60;
    public Text gameTimeText;

    [Header("Break Time")]
    [SerializeField]
    float breakTime = 10;
    public Text breakTimeText;

    bool isRoundStart = true;
    bool isBreakStart = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (isRoundStart)
        {
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;
            }
            else
            {
                ChangeRounds();
                isBreakStart = !isBreakStart;
                isRoundStart = !isRoundStart;
            }
        }
        if (isBreakStart)
        {
            if (breakTime > 0)
            {
                breakTime -= Time.deltaTime;
            }
            else
            {
                ChangeRounds();
                isBreakStart = !isBreakStart;
                isRoundStart = !isRoundStart;
            }
        }
        breakTimeText.text = breakTime.ToString("F0");
        gameTimeText.text = gameTime.ToString("F0");
    }

    public void ChangeRounds()
    {
        gameTime = 5;
        breakTime = 2;
        breakUI.alpha = breakUI.alpha > 0 ? 0 : 1;
        breakUI.blocksRaycasts = breakUI.blocksRaycasts == true ? false : true;
        gameUI.alpha = gameUI.alpha > 0 ? 0 : 1;
        gameUI.blocksRaycasts = gameUI.blocksRaycasts == true ? false : true;
    }
}
