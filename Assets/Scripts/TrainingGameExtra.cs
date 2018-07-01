using System.Collections;
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

    //public O_gamechoice o_gamechoice;
    //public T_gamechoice t_gamechoice;

    [Header("Treadmill - Press 2")]
    public float treadWinScore1;
    public float treadRunUp1, treadRunUp2, treadWinScore2;
    public float runDown1, runDown2;
    public bool player1Lost, player1Won, treadStart1, treadStart2, player2Lost, player2Won;
    public float trainingTimer;

    [Header("PunchingBag - Press 3")]
    public float bagMove1;
    public float bagMove2, bagUp1;
    public float bagNeedle1, bagNeedle2;
    public bool bagBottom1, bagTop1, bagBottom2, bagTop2, bagStart1, bagStart2, bagHit;

    [Header("Pull-Ups - Press 1")]
    public float pullBar1;
    public float pullBar2, pull1, pull2;
    public bool pullStart1, pullStart2, pullReset1, pullReset2, pullCounted1, pullCounted2, pullRelease1, pullRelease2;
    public int pullCount1, pullCount2;
    public bool playerOneChosen, playerTwoChosen;

    [Header("Animators")]
    public Animator player1PullupsAnim;
    public Animator player1TreadmillAnim, player1PunchbagAnim, player2PullupsAnim, player2TreadmillAnim, player2PunchbagAnim;

    private bool choiceSelected = false;

    private bool trainEnded = false;

    [SerializeField]
    private ChangeView _view;

    [Header("Training Variables")]
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

    [Header("GameObject Variables")]
    public GameObject[] trainingModePlayerOne;
    public GameObject[] trainingModePlayerTwo;

    //public enum O_gamechoice // Player One's game choice enum
    //{
    //    pullup = 0,
    //    treadmill = 1,
    //    punchingBag = 2
    //}
    //public enum T_gamechoice // Player Two's game choice enum
    //{
    //    pullup = 0,
    //    treadmill = 1,
    //    punchingBag = 2
    //}

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

        //treadStart1 = true;
        //treadStart2 = true;

        trainingTimer = 10f;
        #endregion

        #region PunchBag Starts
        bagMove1 = 0f;
        bagNeedle1 = 50f;
        bagUp1 = 10f;

        //bagStart1 = true;
        bagBottom1 = true;
        bagTop1 = false;

        bagMove2 = 0f;
        bagNeedle2 = 50f;
        bagUp1 = 10f;

        //bagStart2 = true;
        bagBottom2 = true;
        bagTop2 = false;
        #endregion

        #region Pull-Up Starts
        pullBar1 = 0f;
        pullBar2 = 0f;

        pull1 = 40f;
        pull2 = 40f;

        //pullStart1 = true;
        //pullStart2 = true;

        pullCount1 = 0;
        pullCount2 = 0;

        pullCounted1 = false;
        pullCounted2 = false;

        pullRelease1 = false;
        pullRelease2 = false;
        #endregion
    }

    void TrainingTimerFunction()
    {
        trainingTimer -= Time.deltaTime;

        if (trainingTimer <= 0)
        {
            trainingTimer = 0f;

            treadStart1 = false;
            treadStart2 = false;

            bagStart1 = false;
            bagStart2 = false;

            pullStart1 = false;
            pullStart2 = false;
            trainEnded = true;
        }
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
                        if (playerOneChosen == false)
                        {
                            pullStart1 = true;
                            //player1PullupsAnim.SetBool("PullupTraining", true);

                            playerOneChosen = true;
                        }

                        if (pullStart1 == true)
                        {
                            if (pullReset1 == false)
                            {
                                if (Input.GetKey(o_Action) && pullRelease1 == false)
                                {
                                    pullBar1 += pull1 * Time.deltaTime;

                                    player1PullupsAnim.SetBool("PullupTraining", true);
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
                                    player1PullupsAnim.SetBool("PullupTraining", false);

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

                            if (pullCount1 >= 3)
                            {
                                player1Won = true;

                                pullStart1 = false;

                                Debug.Log("Player 1 Won PULL-UPS!");
                            }
                            if (player1Won == true)
                            {
                                PlayerOne.Instance.TrainingResult(GameChoice.PullUp, 25);
                                player1Won = false;
                            }
                        }

                        if (pullStart1 == false)
                        {
                            if (pullCount1 < 3)
                            {
                                Debug.Log("Player 1 Lost PULL-UPS!");

                                player1Lost = true;
                            }
                        }
                        #endregion
                        break;
                    case GameChoice.Treadmill:
                        #region Player 1 Treadmill Mini-Game
                        if (playerOneChosen == false)
                        {
                            treadStart1 = true;
                            playerOneChosen = true;
                        }

                        if (treadStart1 == true)
                        {
                            treadWinScore1 -= runDown1 * Time.deltaTime;

                            if (treadWinScore1 <= 0f)
                            {
                                treadWinScore1 = 0f;
                            }
                            if (treadWinScore1 >= 110f)
                            {
                                treadWinScore1 = 110f;
                            }
                        }

                        if (Input.GetKeyDown(o_Action) && treadStart1 == true)
                        {
                            treadWinScore1 = treadWinScore1 + treadRunUp1;

                            player1TreadmillAnim.SetBool("RunningTraining", true);
                        }

                        if (treadWinScore1 > 5f)
                        {
                            player1TreadmillAnim.SetFloat("RunSpeed", .2f);
                        }
                        else
                        {
                            player1TreadmillAnim.SetFloat("RunSpeed", .05f);
                            player1TreadmillAnim.SetBool("RunningTraining", false);
                        }

                        if (treadStart1 == false && treadWinScore1 >= 100f)
                        {
                            player1Won = true;
                            Debug.Log("Player 1 Won TREADMILL!");
                            treadWinScore1 = 0f;
                        }
                        if (player1Won == true && trainEnded)
                        {
                            PlayerOne.Instance.TrainingResult(GameChoice.Treadmill, 1);
                            player1Won = false;
                        }
                        if (treadStart1 == false && treadWinScore1 < 100f)
                        {
                            player1Lost = true;
                            Debug.Log("Player 1 Lost TREADMILL!");
                        }
                        #endregion
                        break;
                    case GameChoice.PunchingBag:
                        #region Player 1 Heavy-Bag Mini-Game
                        if (playerOneChosen == false)
                        {
                            bagStart1 = true;
                            playerOneChosen = true;
                        }

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
                            if (countPunchOne >= 3 && trainingTimer <= 0)
                            {
                                PlayerOne.Instance.TrainingResult(GameChoice.PunchingBag, 5);
                                countPunchOne = 0;
                                bagStart1 = true;
                                return;
                            }
                            if (bagMove1 >= 0 && bagMove1 <= 20)
                            {
                                //Debug.Log("Player Wins PUNCHING BAG!");
                                countPunchOne++;
                                bagStart1 = true;
                            }

                            if (bagMove1 < 0 || bagMove1 > 20)
                            {
                                //Debug.Log("Player Loses PUNCHING BAG!");
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
                        #region Player 2 Pull-Up Mini-Game
                        if (playerTwoChosen == false)
                        {
                            pullStart2 = true;
                            playerTwoChosen = true;
                        }

                        if (pullStart2 == true)
                        {
                            if (pullReset2 == false)
                            {
                                if (Input.GetKey(t_Action) && pullRelease2 == false)
                                {
                                    pullBar2 += pull2 * Time.deltaTime;

                                    player2PullupsAnim.SetBool("PullupTraining", true);
                                }

                                else
                                {
                                    pullBar2 -= (pull2 * 3) * Time.deltaTime;
                                }

                                if (pullBar2 <= 0f)
                                {
                                    pullBar2 = 0f;
                                }

                                if (pullBar2 >= 100f)
                                {
                                    if (pullCounted2 == false)
                                    {
                                        pullCount2 += 1;

                                        pullCounted2 = true;
                                    }

                                    pullBar2 = 100f;
                                }

                                if (Input.GetKeyUp(t_Action))
                                {
                                    player2PullupsAnim.SetBool("PullupTraining", false);

                                    pullRelease2 = true;

                                    if (pullCounted2 == true)
                                    {
                                        pullCounted2 = false;
                                    }

                                    pullReset2 = true;
                                }
                            }

                            if (pullReset2 == true)
                            {
                                pullBar2 -= (pull2 * 6) * Time.deltaTime;

                                if (pullBar2 <= 0f)
                                {
                                    pullRelease2 = false;

                                    pullReset2 = false;
                                }
                            }

                            if (pullCount2 >= 3)
                            {
                                player2Won = true;

                                pullStart2 = false;

                                Debug.Log("Player 2 Won PULL-UPS!");
                            }
                            if (player2Won == true)
                            {
                                PlayerTwo.Instance.TrainingResult(GameChoice.PullUp, 25);
                                player2Won = false;
                            }
                        }

                        if (pullStart2 == false)
                        {
                            if (pullCount2 < 3)
                            {
                                Debug.Log("Player 2 Lost PULL-UPS!");

                                player2Lost = true;
                            }
                        }
                        #endregion
                        break;
                    case GameChoice.Treadmill:
                        #region Player 2 Treadmill Mini-Game
                        if (playerTwoChosen == false)
                        {
                            treadStart2 = true;
                            playerTwoChosen = true;
                        }

                        if (treadStart2 == true)
                        {
                            //trainingTimer -= Time.deltaTime;

                            treadWinScore2 -= runDown1 * Time.deltaTime;

                            if (treadWinScore2 <= 0f)
                            {
                                treadWinScore2 = 0f;
                            }
                            if (treadWinScore2 >= 110f)
                            {
                                treadWinScore2 = 110f;
                            }

                            if (trainingTimer <= 0)
                            {
                                trainingTimer = 0f;

                                treadStart2 = false;
                            }
                        }

                        if (Input.GetKeyDown(t_Action) && treadStart2 == true)
                        {
                            treadWinScore2 = treadWinScore2 + treadRunUp1;

                            player2TreadmillAnim.SetBool("RunningTraining", true);
                        }

                        if (treadWinScore2 > 5f)
                        {
                            player2TreadmillAnim.SetFloat("RunSpeed", .2f);
                        }
                        else
                        {
                            player2TreadmillAnim.SetFloat("RunSpeed", .05f);
                            player2TreadmillAnim.SetBool("RunningTraining", false);
                        }

                        if (treadStart2 == false && treadWinScore2 >= 100f)
                        {
                            player2Won = true;
                            treadWinScore2 = 0f;
                            Debug.Log("Player 2 Won TREADMILL!");
                        }

                        if (player2Won == true && trainEnded)
                        {
                            PlayerTwo.Instance.TrainingResult(GameChoice.Treadmill, 1);
                            player2Won = false;
                        }

                        if (treadStart2 == false && treadWinScore2 < 100f)
                        {
                            player2Lost = true;

                            Debug.Log("Player 2 Lost TREADMILL!");
                        }
                        #endregion
                        break;
                    case GameChoice.PunchingBag:
                        #region Player 2 Heavy-Bag Mini-Game
                        if (playerTwoChosen == false)
                        {
                            bagStart2 = true;
                            playerTwoChosen = true;
                        }

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
                            if (countPunchTwo >= 3 && trainingTimer <= 0)
                            {
                                PlayerTwo.Instance.TrainingResult(GameChoice.PunchingBag, 5);
                                countPunchTwo = 0;
                                bagStart2 = true;
                                return;
                            }
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
        #region Commented-Out Old Code
        //if (_view.isTraining)
        //{
        //    if (choiceSelected)
        //    {
        //        if (Input.GetKeyDown(o_Action))
        //        {
        //            //play animation etc for correct mini-game for Player one
        //            if (o_gamechoice == O_gamechoice.punchingBag)
        //            {
        //                //anim.setBool("o_isPunching", true);

        //                #region Player 1 Heavy-Bag Mini-Game
        //                if (bagStart1 == true)
        //                {
        //                    if (bagBottom1 == true && bagTop1 == false)
        //                    {
        //                        bagMove1 += bagUp1 * Time.deltaTime;
        //                    }

        //                    if (bagBottom1 == false && bagTop1 == true)
        //                    {
        //                        bagMove1 -= bagUp1 * Time.deltaTime;
        //                    }

        //                    if (bagMove1 >= 100f)
        //                    {
        //                        bagMove1 = 100f;

        //                        bagBottom1 = false;
        //                        bagTop1 = true;
        //                    }

        //                    if (bagMove1 <= 0f)
        //                    {
        //                        bagMove1 = 0f;

        //                        bagBottom1 = true;
        //                        bagTop1 = false;
        //                    }

        //                    if (Input.GetKeyDown(o_Action))
        //                    {
        //                        bagStart1 = false;
        //                    }
        //                }

        //                if (bagStart1 == false)
        //                {
        //                    if (bagMove1 >= 45 && bagMove1 <= 55)
        //                    {
        //                        Debug.Log("Player Wins PUNCHING BAG!");
        //                    }

        //                    if (bagMove1 < 45 || bagMove1 > 55)
        //                    {
        //                        Debug.Log("Player Loses PUNCHING BAG!");
        //                    }
        //                }
        //                #endregion
        //            }
        //            if (o_gamechoice == O_gamechoice.treadmill)
        //            {
        //                //anim.setBool("o_isRunning", true);

        //                #region Player 1 Treadmill Mini-Game
        //                if (treadStart == true)
        //                {
        //                    trainingTimer -= 1f * Time.deltaTime;

        //                    treadWinScore1 -= runDown1 * Time.deltaTime;

        //                    if (treadWinScore1 <= 0f)
        //                    {
        //                        treadWinScore1 = 0f;
        //                    }

        //                    if (trainingTimer <= 0)
        //                    {
        //                        trainingTimer = 0f;

        //                        treadStart = false;
        //                    }
        //                }

        //                if (Input.GetKeyDown(o_Action) && treadStart == true)
        //                {
        //                    treadWinScore1 = treadWinScore1 + treadRunUp1;
        //                }

        //                if (treadStart == false && treadWinScore1 >= 100f)
        //                {
        //                    player1Won = true;

        //                    Debug.Log("Player 1 Won!");
        //                }

        //                if (treadStart == false && treadWinScore1 < 100f)
        //                {
        //                    player1Lost = true;

        //                    Debug.Log("Player 1 Lost!");
        //                }
        //                #endregion
        //            }
        //            if (o_gamechoice == O_gamechoice.pullup)
        //            {
        //                //anim.setBool("o_isPullingUp", true);

        //                #region Player 1 Pull-Up Mini-Game
        //                if (pullStart1 == true)
        //                {
        //                    if (pullReset1 == false)
        //                    {
        //                        if (Input.GetKey(o_Action) && pullRelease1 == false)
        //                        {
        //                            pullBar1 += pull1 * Time.deltaTime;
        //                        }

        //                        else
        //                        {
        //                            pullBar1 -= (pull1 * 3) * Time.deltaTime;
        //                        }

        //                        if (pullBar1 <= 0f)
        //                        {
        //                            pullBar1 = 0f;
        //                        }

        //                        if (pullBar1 >= 100f)
        //                        {
        //                            if (pullCounted1 == false)
        //                            {
        //                                pullCount1 += 1;

        //                                pullCounted1 = true;
        //                            }

        //                            pullBar1 = 100f;
        //                        }

        //                        if (Input.GetKeyUp(o_Action))
        //                        {
        //                            pullRelease1 = true;

        //                            if (pullCounted1 == true)
        //                            {
        //                                pullCounted1 = false;
        //                            }

        //                            pullReset1 = true;
        //                        }
        //                    }

        //                    if (pullReset1 == true)
        //                    {
        //                        pullBar1 -= (pull1 * 6) * Time.deltaTime;

        //                        if (pullBar1 <= 0f)
        //                        {
        //                            pullRelease1 = false;

        //                            pullReset1 = false;
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }

        //        }
        //        if (Input.GetKeyDown(t_Action))
        //        {
        //            //play animation etc for correct mini-game for player two
        //            if (t_gamechoice == T_gamechoice.punchingBag)
        //            {
        //                t_Punch = !t_Punch;
        //                //anim.setBool("t_Punch", t_Punch);

        //                #region Player 2 Heavy-Bag Mini-Game
        //                if (bagStart2 == true)
        //                {
        //                    if (bagBottom2 == true && bagTop2 == false)
        //                    {
        //                        bagMove2 += bagUp1 * Time.deltaTime;
        //                    }

        //                    if (bagBottom2 == false && bagTop2 == true)
        //                    {
        //                        bagMove2 -= bagUp1 * Time.deltaTime;
        //                    }

        //                    if (bagMove2 >= 100f)
        //                    {
        //                        bagMove2 = 100f;

        //                        bagBottom2 = false;
        //                        bagTop2 = true;
        //                    }

        //                    if (bagMove2 <= 0f)
        //                    {
        //                        bagMove2 = 0f;

        //                        bagBottom2 = true;
        //                        bagTop2 = false;
        //                    }

        //                    if (Input.GetKeyDown(t_Action))
        //                    {
        //                        bagStart2 = false;
        //                    }
        //                }

        //                if (bagStart2 == false)
        //                {
        //                    if (bagMove2 >= 45 && bagMove2 <= 55)
        //                    {
        //                        Debug.Log("Player Wins PUNCHING BAG!");
        //                    }

        //                    if (bagMove2 < 45 || bagMove2 > 55)
        //                    {
        //                        Debug.Log("Player Loses PUNCHING BAG!");
        //                    }
        //                }
        //                #endregion
        //            }
        //            if (t_gamechoice == T_gamechoice.treadmill)
        //            {
        //                t_RunSpeed += 0.5f;
        //                //anim.setFloat("t_RunSpeed", t_RunSpeed);

        //                #region Player 2 Treadmill Mini-Game
        //                if (treadStart == true)
        //                {
        //                    trainingTimer -= 1f * Time.deltaTime;

        //                    treadWinScore2 -= runDown2 * Time.deltaTime;

        //                    if (treadWinScore2 <= 0f)
        //                    {
        //                        treadWinScore2 = 0f;
        //                    }

        //                    if (trainingTimer <= 0)
        //                    {
        //                        trainingTimer = 0f;

        //                        treadStart = false;
        //                    }
        //                }

        //                if (Input.GetKeyDown(t_Action) && treadStart == true)
        //                {
        //                    treadWinScore2 = treadWinScore2 + treadRunUp2;
        //                }

        //                if (treadStart == false && treadWinScore2 >= 100f)
        //                {
        //                    player2Won = true;

        //                    Debug.Log("Player 2 Won!");
        //                }

        //                if (treadStart == false && treadWinScore2 < 100f)
        //                {
        //                    player2Lost = true;

        //                    Debug.Log("Player 2 Lost!");
        //                }
        //                #endregion
        //            }
        //            if (t_gamechoice == T_gamechoice.pullup)
        //            {
        //                //anim.setBool("t_isPullingUp", true);

        //                #region Player 2 Pull-Up Mini-Game
        //                if (pullStart2 == true)
        //                {
        //                    if (pullReset2 == false)
        //                    {
        //                        if (Input.GetKey(t_Action) && pullRelease2 == false)
        //                        {
        //                            pullBar2 += pull2 * Time.deltaTime;
        //                        }

        //                        else
        //                        {
        //                            pullBar2 -= (pull2 * 3) * Time.deltaTime;
        //                        }

        //                        if (pullBar2 <= 0f)
        //                        {
        //                            pullBar2 = 0f;
        //                        }

        //                        if (pullBar2 >= 100f)
        //                        {
        //                            if (pullCounted2 == false)
        //                            {
        //                                pullCount2 += 1;

        //                                pullCounted2 = true;
        //                            }

        //                            pullBar2 = 100f;
        //                        }

        //                        if (Input.GetKeyUp(t_Action))
        //                        {
        //                            pullRelease2 = true;

        //                            if (pullCounted2 == true)
        //                            {
        //                                pullCounted2 = false;
        //                            }

        //                            pullReset2 = true;
        //                        }
        //                    }

        //                    if (pullReset2 == true)
        //                    {
        //                        pullBar2 -= (pull2 * 6) * Time.deltaTime;

        //                        if (pullBar2 <= 0f)
        //                        {
        //                            pullRelease2 = false;

        //                            pullReset2 = false;
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }
        //        }
        //    }
        //    else
        //    {   //Set Game choice for player one
        //        if (Input.GetKeyDown(o_ChoiceOne))
        //        {
        //            o_gamechoice = O_gamechoice.punchingBag;
        //            choiceSelected = true;
        //        }
        //        if (Input.GetKeyDown(o_ChoiceTwo))
        //        {
        //            o_gamechoice = O_gamechoice.treadmill;
        //            choiceSelected = true;
        //        }
        //        if (Input.GetKeyDown(o_ChoiceThree))
        //        {
        //            o_gamechoice = O_gamechoice.pullup;
        //            choiceSelected = true;
        //        }

        //        //Set Game choice for player Two
        //        if (Input.GetKeyDown(t_ChoiceOne))
        //        {
        //            t_gamechoice = T_gamechoice.punchingBag;
        //            choiceSelected = true;
        //        }
        //        if (Input.GetKeyDown(t_ChoiceTwo))
        //        {
        //            t_gamechoice = T_gamechoice.treadmill;
        //            choiceSelected = true;
        //        }
        //        if (Input.GetKeyDown(t_ChoiceThree))
        //        {
        //            t_gamechoice = T_gamechoice.pullup;
        //            choiceSelected = true;
        //        }
        //    }
        //}
        #endregion

        if (_view.isTraining && GameManager.instance.trainingOneSelected == true && GameManager.instance.trainingTwoSelected == true)
        {
            TrainingTimerFunction();
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
            foreach (GameObject mode in trainingModePlayerOne)
            {
                mode.SetActive(false);
                switch (GameManager.instance.gameChoiceOne)
                {
                    case GameChoice.PullUp:
                        trainingModePlayerOne[0].SetActive(true);
                        break;
                    case GameChoice.Treadmill:
                        trainingModePlayerOne[1].SetActive(true);
                        break;
                    case GameChoice.PunchingBag:
                        trainingModePlayerOne[2].SetActive(true);
                        break;
                    case GameChoice.Rest:
                        trainingModePlayerOne[3].SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            foreach (GameObject mode in trainingModePlayerTwo)
            {
                mode.SetActive(false);
                switch (GameManager.instance.gameChoiceTwo)
                {
                    case GameChoice.PullUp:
                        trainingModePlayerTwo[0].SetActive(true);
                        break;
                    case GameChoice.Treadmill:
                        trainingModePlayerTwo[1].SetActive(true);
                        break;
                    case GameChoice.PunchingBag:
                        trainingModePlayerTwo[2].SetActive(true);
                        break;
                    case GameChoice.Rest:
                        trainingModePlayerTwo[3].SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
        //GameModeToggler();
        //CheckPlayerOneSpeed();
        //CheckPlayerTwoSpeed();
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
    /*void GameModeToggler()
    {
        if (choiceSelected)
        {
            if (o_gamechoice == O_gamechoice.punchingBag)
            {
                //anim.setBool("o_isPunching", true);
                //anim.setBool("o_isRunning", false);
                //anim.setBool("o_isPullingUp", false);
            }
            if (o_gamechoice == O_gamechoice.treadmill)
            {
                //anim.setBool("o_isPunching", false);
                //anim.setBool("o_isRunning", true);
                //anim.setBool("o_isPullingUp", false);
            }
            if (o_gamechoice == O_gamechoice.pullup)
            {
                //anim.setBool("o_isPunching", false);
                //anim.setBool("o_isRunning", false);
                //anim.setBool("o_isPullingUp", true);
            }

            if (t_gamechoice == T_gamechoice.punchingBag)
            {
                //anim.setBool("t_isPunching", true);
                //anim.setBool("t_isRunning", false);
                //anim.setBool("t_isPullingUp", false);
            }
            if (t_gamechoice == T_gamechoice.treadmill)
            {
                //anim.setBool("t_isPunching", false);
                //anim.setBool("t_isRunning", true);
                //anim.setBool("t_isPullingUp", false);
            }
            if (t_gamechoice == T_gamechoice.pullup)
            {
                //anim.setBool("t_isPunching", false);
                //anim.setBool("t_isRunning", false);
                //anim.setBool("t_isPullingUp", true);
            }
        }

    }
    void CheckPlayerOneSpeed()
    {
        //Player One Run Speed stuff
        o_RunSpeed -= 0.5f * Time.deltaTime;
        if (o_RunSpeed <= 0)
            o_RunSpeed = 0;

        if (o_RunSpeed >= 3)
            o_RunSpeed = 3;

        if (o_RunSpeed < 1f)
        {
            //No Speed Gain in real game
        }

        if (o_RunSpeed >= 1f)
        {
            //Speed Gain in real game
        }
    }
    void CheckPlayerTwoSpeed()
    {
        //Player Two Run Speed stuff
        t_RunSpeed -= 0.5f;
        if (t_RunSpeed <= 0)
            t_RunSpeed = 0;

        if (t_RunSpeed >= 3)
            t_RunSpeed = 3;

        if (t_RunSpeed < 1f)
        {
            //No Speed Gain in real game
        }

        if (t_RunSpeed >= 1f)
        {
            //Speed Gain in real game
        }
    }
    */
}