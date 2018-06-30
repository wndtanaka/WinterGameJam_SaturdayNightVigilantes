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

    public CanvasGroup playerOnePanel;
    public CanvasGroup playerTwoPanel;

    [SerializeField]
    Animator anim;

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
            anim.SetBool("OpenBreakUI", false);
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
            anim.SetBool("OpenBreakUI",true);
            if (breakTime > 0)
            {
                breakTime -= Time.deltaTime;
                TrainingChoices();
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
        gameTime = 60;
        breakTime = 10;
        breakUI.alpha = breakUI.alpha > 0 ? 0 : 1;
        breakUI.blocksRaycasts = breakUI.blocksRaycasts == true ? false : true;
        gameUI.alpha = gameUI.alpha > 0 ? 0 : 1;
        gameUI.blocksRaycasts = gameUI.blocksRaycasts == true ? false : true;
    }

    public void TrainingChoices()
    {
        #region Training Selection for Player One
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerOne.Instance.TrainingResult(GameChoice.PullUp, 10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerOne.Instance.TrainingResult(GameChoice.Treadmill, 10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerOne.Instance.TrainingResult(GameChoice.PunchingBag, 10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerOne.Instance.TrainingResult(GameChoice.Rest, 0);
        }
        #endregion
        #region Training Selection for Player Two
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerTwo.Instance.TrainingResult(GameChoice.PullUp, 10);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PlayerTwo.Instance.TrainingResult(GameChoice.Treadmill, 10);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            PlayerTwo.Instance.TrainingResult(GameChoice.PunchingBag, 10);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            PlayerTwo.Instance.TrainingResult(GameChoice.Rest, 0);
        }
        #endregion
    }
}
