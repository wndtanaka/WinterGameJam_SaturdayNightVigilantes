﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingGameExtra : MonoBehaviour
{

    [Header("Player One")] //Player One's button binds during mini-game
    public KeyCode o_Action = KeyCode.Space;
    public KeyCode o_ChoiceOne = KeyCode.Z, o_ChoiceTwo = KeyCode.X, o_ChoiceThree = KeyCode.C;
    public float o_RunSpeed;
    public bool o_Punch;

    [Header("Player Two")]
    public KeyCode t_Action = KeyCode.KeypadEnter; //Player One's button binds during mini-game
    public KeyCode t_ChoiceOne = KeyCode.KeypadEnter, t_ChoiceTwo = KeyCode.KeypadEnter, t_ChoiceThree = KeyCode.KeypadEnter;
    public float t_RunSpeed;
    public bool t_Punch;

    [Header("Treadmill")]
    public float treadWinScore1, treadWinScore2;
    public float treadRunUp1, treadRunUp2;
    public float runDown1, runDown2;
    public bool player1Lost, player1Won, treadStart, player2Lost, player2Won;
    public float trainingTimer;

    [Header("PunchingBag")]
    public float bagMove1;
    public float bagMove2, bagUp1;
    public float bagNeedle1, bagNeedle2;
    public bool bagBottom1, bagTop1, bagBottom2, bagTop2, bagStart1, bagStart2, bagHit;

    [Header("Pull-Ups")]
    public float pullBar1;
    public float pullBar2, pull1, pull2;
    public bool pullStart1, pullStart2, pullReset1, pullReset2, pullCounted1, pullCounted2, pullRelease1, pullRelease2;
    public int pullCount1, pullCount2;

    private bool choiceSelected = false;

    [SerializeField]
    private ChangeView _view;

    public Image treadmillbarOne;
    public Image treadmillbarTwo;

    public Image punchingBagOne;
    public Image punchingBagTwo;
    public bool rightOne = true;
    public bool rightTwo = true;
    public int countPunchOne;
    public int countPunchTwo;

    public Image pullUpOne;
    public Image pullUpTwo;


    void Start()
    {
        choiceSelected = false;

        _view = GameObject.FindWithTag("MainCamera").GetComponent<ChangeView>();

        #region Treadmill Starts
        treadWinScore1 = 0f;
        treadRunUp1 = 10f;
        runDown1 = 40f;

        treadWinScore2 = 0f;
        treadRunUp2 = 10f;
        runDown2 = 40f;

        treadStart = true;

        trainingTimer = 10f;
        #endregion

        #region PunchBag Starts
        bagMove1 = 0f;
        bagNeedle1 = 50f;
        bagUp1 = 100f;

        bagStart1 = true;
        bagBottom1 = true;
        bagTop1 = false;

        bagMove2 = 0f;
        bagNeedle2 = 50f;
        bagUp1 = 100f;

        bagStart2 = true;
        bagBottom2 = true;
        bagTop2 = false;
        #endregion

        #region Pull-Up Starts
        pullBar1 = 0f;
        pullBar2 = 0f;

        pull1 = 40f;

        pullStart1 = true;
        pullStart2 = true;

        pullCount1 = 0;
        pullCount2 = 0;

        pullCounted1 = false;
        pullCounted2 = false;

        pullRelease1 = false;
        pullRelease2 = false;
        #endregion
    }

    void TrainingSelection(PlayerType player, GameChoice gameChoice)
    {
        switch (player)
        {
            case PlayerType.PlayerOne:
                switch (gameChoice)
                {
                    case GameChoice.PullUp:
                        #region Player 1 Pull-Up Mini-Game
                        if (pullStart1 == true)
                        {
                            if (pullReset1 == false)
                            {
                                if (Input.GetKey(o_Action) && pullRelease1 == false)
                                {
                                    pullBar1 += pull1 * Time.deltaTime;
                                }

                                else
                                {
                                    pullBar1 -= (pull1 * 3) * Time.deltaTime;
                                }

                                if (pullBar1 <= 0f)
                                {
                                    pullBar1 = 0f;
                                }

                                if (pullBar1 >= 100f)
                                {
                                    if (pullCounted1 == false)
                                    {
                                        pullCount1 += 1;

                                        pullCounted1 = true;
                                    }

                                    pullBar1 = 100f;
                                }

                                if (Input.GetKeyUp(o_Action))
                                {
                                    pullRelease1 = true;

                                    if (pullCounted1 == true)
                                    {
                                        pullCounted1 = false;
                                    }

                                    pullReset1 = true;
                                }
                            }

                            if (pullReset1 == true)
                            {
                                pullBar1 -= (pull1 * 6) * Time.deltaTime;

                                if (pullBar1 <= 0f)
                                {
                                    pullRelease1 = false;

                                    pullReset1 = false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case GameChoice.Treadmill:
                        #region Player 1 Treadmill Mini-Game
                        if (treadStart == true)
                        {
                            trainingTimer -= 1f * Time.deltaTime;

                            treadWinScore1 -= runDown1 * Time.deltaTime;

                            if (treadWinScore1 <= 0f)
                            {
                                treadWinScore1 = 0f;
                            }

                            if (trainingTimer <= 0)
                            {
                                trainingTimer = 0f;

                                treadStart = false;
                            }
                        }

                        if (Input.GetKeyDown(o_Action) && treadStart == true)
                        {
                            treadWinScore1 = treadWinScore1 + treadRunUp1;
                        }

                        if (treadStart == false && treadWinScore1 >= 100f)
                        {
                            player1Won = true;

                            Debug.Log("Player 1 Won!");
                        }

                        if (treadStart == false && treadWinScore1 < 100f)
                        {
                            player1Lost = true;

                            Debug.Log("Player 1 Lost!");
                        }
                        #endregion
                        break;
                    case GameChoice.PunchingBag:
                        #region Player 1 Heavy-Bag Mini-Game
                        if (bagStart1 == true)
                        {
                            bagMove1 = punchingBagOne.transform.localPosition.magnitude;

                            if (Input.GetKeyDown(o_Action))
                            {
                                bagStart1 = false;
                            }
                        }

                        if (bagStart1 == false)
                        {
                            if (bagMove1 >= 0 && bagMove1 <= 20)
                            {
                                Debug.Log("Player Wins PUNCHING BAG!");
                                countPunchOne++;
                                bagStart1 = true;
                            }

                            if (bagMove1 < 0 || bagMove1 > 20)
                            {
                                Debug.Log("Player Loses PUNCHING BAG!");
                                bagStart1 = true;
                            }
                        }
                        #endregion
                        break;
                    case GameChoice.Rest:
                        break;
                }
                break;
            case PlayerType.PlayerTwo:
                switch (gameChoice)
                {
                    case GameChoice.PullUp:
                        #region Player 1 Pull-Up Mini-Game
                        if (pullStart1 == true)
                        {
                            if (pullReset1 == false)
                            {
                                if (Input.GetKey(t_Action) && pullRelease1 == false)
                                {
                                    pullBar1 += pull1 * Time.deltaTime;
                                }

                                else
                                {
                                    pullBar1 -= (pull1 * 3) * Time.deltaTime;
                                }

                                if (pullBar1 <= 0f)
                                {
                                    pullBar1 = 0f;
                                }

                                if (pullBar1 >= 100f)
                                {
                                    if (pullCounted1 == false)
                                    {
                                        pullCount1 += 1;

                                        pullCounted1 = true;
                                    }

                                    pullBar1 = 100f;
                                }

                                if (Input.GetKeyUp(t_Action))
                                {
                                    pullRelease1 = true;

                                    if (pullCounted1 == true)
                                    {
                                        pullCounted1 = false;
                                    }

                                    pullReset1 = true;
                                }
                            }

                            if (pullReset1 == true)
                            {
                                pullBar1 -= (pull1 * 6) * Time.deltaTime;

                                if (pullBar1 <= 0f)
                                {
                                    pullRelease1 = false;

                                    pullReset1 = false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case GameChoice.Treadmill:
                        #region Player 1 Treadmill Mini-Game
                        if (treadStart == true)
                        {
                            trainingTimer -= 1f * Time.deltaTime;

                            treadWinScore2 -= runDown1 * Time.deltaTime;

                            if (treadWinScore2 <= 0f)
                            {
                                treadWinScore2 = 0f;
                            }

                            if (trainingTimer <= 0)
                            {
                                trainingTimer = 0f;

                                treadStart = false;
                            }
                        }

                        if (Input.GetKeyDown(t_Action) && treadStart == true)
                        {
                            treadWinScore2 = treadWinScore2 + treadRunUp1;
                        }

                        if (treadStart == false && treadWinScore2 >= 100f)
                        {
                            player2Won = true;

                            Debug.Log("Player 1 Won!");
                        }

                        if (treadStart == false && treadWinScore2 < 100f)
                        {
                            player2Lost = true;

                            Debug.Log("Player 1 Lost!");
                        }
                        #endregion
                        break;
                    case GameChoice.PunchingBag:
                        #region Player 2 Heavy-Bag Mini-Game
                        if (bagStart2 == true)
                        {
                            bagMove2 = punchingBagTwo.transform.localPosition.magnitude;

                            if (Input.GetKeyDown(t_Action))
                            {
                                bagStart2 = false;
                            }
                        }

                        if (bagStart2 == false)
                        {
                            if (bagMove2 >= 0 && bagMove2 <= 20)
                            {
                                countPunchTwo++;
                                bagStart2 = true;
                                Debug.Log("Player Wins PUNCHING BAG!");
                            }

                            if (bagMove2 < 0 || bagMove2 > 20)
                            {
                                bagStart2 = true;
                                Debug.Log("Player Loses PUNCHING BAG!");
                            }
                        }
                        #endregion
                        break;
                    case GameChoice.Rest:
                        break;
                }
                break;
        }
    }

    void Update()
    {
        if (_view.isTraining && GameManager.instance.trainingOneSelected == true && GameManager.instance.trainingTwoSelected == true)
        {
            if (GameManager.instance.trainingOneSelected == true)
            {
                TrainingSelection(PlayerOne.Instance.playerType, GameManager.instance.gameChoiceOne);
            }
            if (GameManager.instance.trainingTwoSelected == true)
            {
                TrainingSelection(PlayerTwo.Instance.playerType, GameManager.instance.gameChoiceTwo);
            }
            if (GameManager.instance.gameChoiceOne == GameChoice.Treadmill)
            {
                treadmillbarOne.fillAmount = Mathf.Lerp(treadmillbarOne.fillAmount, treadWinScore1 / 100, 5 * Time.deltaTime);
            }
            if (GameManager.instance.gameChoiceTwo == GameChoice.Treadmill)
            {
                treadmillbarTwo.fillAmount = Mathf.Lerp(treadmillbarTwo.fillAmount, treadWinScore2 / 100, 5 * Time.deltaTime);
            }
            if (GameManager.instance.gameChoiceOne == GameChoice.PullUp)
            {
                pullUpOne.fillAmount = Mathf.Lerp(pullUpOne.fillAmount, pullBar1 / 100, 5 * Time.deltaTime);
            }
            if (GameManager.instance.gameChoiceTwo == GameChoice.PullUp)
            {
                pullUpTwo.fillAmount = Mathf.Lerp(pullUpTwo.fillAmount, pullBar2 / 100, 5 * Time.deltaTime);
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.gameChoiceOne == GameChoice.PunchingBag)
        {
            if (punchingBagOne.transform.localPosition.x < 150 && rightOne)
            {
                punchingBagOne.transform.localPosition += new Vector3(5, 0, 0);
            }
            if (punchingBagOne.transform.localPosition.x >= 150)
            {
                rightOne = false;
            }
            if (rightOne == false)
            {
                punchingBagOne.transform.localPosition -= new Vector3(5, 0, 0);
            }
            if (punchingBagOne.transform.localPosition.x <= -150)
            {
                rightOne = true;
            }
            
        }
        if (GameManager.instance.gameChoiceTwo == GameChoice.PunchingBag)
        {
            if (punchingBagTwo.transform.localPosition.x < 150 && rightTwo)
            {
                punchingBagTwo.transform.localPosition += new Vector3(5, 0, 0);
            }
            if (punchingBagTwo.transform.localPosition.x >= 150)
            {
                rightTwo = false;
            }
            if (rightTwo == false)
            {
                punchingBagTwo.transform.localPosition -= new Vector3(5, 0, 0);
            }
            if (punchingBagTwo.transform.localPosition.x <= -150)
            {
                rightTwo = true;
            }
        }
    }
}