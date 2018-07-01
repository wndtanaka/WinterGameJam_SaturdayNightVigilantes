using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingGame : MonoBehaviour {

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

    public O_gamechoice o_gamechoice;
    public T_gamechoice t_gamechoice;

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

    public enum O_gamechoice // Player One's game choice enum
    {
        pullup = 0,
        treadmill = 1,
        punchingBag = 2
    }
    public enum T_gamechoice // Player Two's game choice enum
    {
        pullup = 0,
        treadmill = 1,
        punchingBag = 2
    }

    private bool choiceSelected = false;

    private ChangeView _view;

    void Start()
    {
        choiceSelected = false;

        _view = GameObject.FindWithTag("Main Camera").GetComponent<ChangeView>();

        #region Treadmill Starts
        treadWinScore1 = 0f;
        treadRunUp1 = 10f;
        runDown1 = 40f;

        treadWinScore2 = 0f;
        treadRunUp2 = 10f;
        runDown2 = 40f;

        treadStart = true;

        trainingTimer = 5f;
        #endregion

        #region PunchBag Starts
        bagMove1 = 0f;
        bagNeedle1 = 50f;
        bagUp1 = 10f;

        bagStart1 = true;
        bagBottom1 = true;
        bagTop1 = false;

        bagMove2 = 0f;
        bagNeedle2 = 50f;
        bagUp1 = 10f;

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

    void Update()
    {
        

        if (_view.isTraining)
        {
            if (choiceSelected)
            {
                if (Input.GetKeyDown(o_Action))
                {
                    //play animation etc for correct mini-game for Player one
                    if (o_gamechoice == O_gamechoice.punchingBag)
                    {
                        //anim.setBool("o_isPunching", true);

                        #region Player 1 Heavy-Bag Mini-Game
                        if (bagStart1 == true)
                        {
                            if (bagBottom1 == true && bagTop1 == false)
                            {
                                bagMove1 += bagUp1 * Time.deltaTime;
                            }

                            if (bagBottom1 == false && bagTop1 == true)
                            {
                                bagMove1 -= bagUp1 * Time.deltaTime;
                            }

                            if (bagMove1 >= 100f)
                            {
                                bagMove1 = 100f;

                                bagBottom1 = false;
                                bagTop1 = true;
                            }

                            if (bagMove1 <= 0f)
                            {
                                bagMove1 = 0f;

                                bagBottom1 = true;
                                bagTop1 = false;
                            }

                            if (Input.GetKeyDown(o_Action))
                            {
                                bagStart1 = false;
                            }
                        }

                        if (bagStart1 == false)
                        {
                            if (bagMove1 >= 45 && bagMove1 <= 55)
                            {
                                Debug.Log("Player Wins PUNCHING BAG!");
                            }

                            if (bagMove1 < 45 || bagMove1 > 55)
                            {
                                Debug.Log("Player Loses PUNCHING BAG!");
                            }
                        }
                        #endregion
                    }
                    if (o_gamechoice == O_gamechoice.treadmill)
                    {
                        //anim.setBool("o_isRunning", true);

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
                    }
                    if (o_gamechoice == O_gamechoice.pullup)
                    {
                        //anim.setBool("o_isPullingUp", true);

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
                    }

                }
                if (Input.GetKeyDown(t_Action))
                {
                    //play animation etc for correct mini-game for player two
                    if (t_gamechoice == T_gamechoice.punchingBag)
                    {
                        t_Punch = !t_Punch;
                        //anim.setBool("t_Punch", t_Punch);

                        #region Player 2 Heavy-Bag Mini-Game
                        if (bagStart2 == true)
                        {
                            if (bagBottom2 == true && bagTop2 == false)
                            {
                                bagMove2 += bagUp1 * Time.deltaTime;
                            }

                            if (bagBottom2 == false && bagTop2 == true)
                            {
                                bagMove2 -= bagUp1 * Time.deltaTime;
                            }

                            if (bagMove2 >= 100f)
                            {
                                bagMove2 = 100f;

                                bagBottom2 = false;
                                bagTop2 = true;
                            }

                            if (bagMove2 <= 0f)
                            {
                                bagMove2 = 0f;

                                bagBottom2 = true;
                                bagTop2 = false;
                            }

                            if (Input.GetKeyDown(t_Action))
                            {
                                bagStart2 = false;
                            }
                        }

                        if (bagStart2 == false)
                        {
                            if (bagMove2 >= 45 && bagMove2 <= 55)
                            {
                                Debug.Log("Player Wins PUNCHING BAG!");
                            }

                            if (bagMove2 < 45 || bagMove2 > 55)
                            {
                                Debug.Log("Player Loses PUNCHING BAG!");
                            }
                        }
                        #endregion
                    }
                    if (t_gamechoice == T_gamechoice.treadmill)
                    {
                        t_RunSpeed += 0.5f;
                        //anim.setFloat("t_RunSpeed", t_RunSpeed);

                        #region Player 2 Treadmill Mini-Game
                        if (treadStart == true)
                        {
                            trainingTimer -= 1f * Time.deltaTime;

                            treadWinScore2 -= runDown2 * Time.deltaTime;

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
                            treadWinScore2 = treadWinScore2 + treadRunUp2;
                        }

                        if (treadStart == false && treadWinScore2 >= 100f)
                        {
                            player2Won = true;

                            Debug.Log("Player 2 Won!");
                        }

                        if (treadStart == false && treadWinScore2 < 100f)
                        {
                            player2Lost = true;

                            Debug.Log("Player 2 Lost!");
                        }
                        #endregion
                    }
                    if (t_gamechoice == T_gamechoice.pullup)
                    {
                        //anim.setBool("t_isPullingUp", true);

                        #region Player 2 Pull-Up Mini-Game
                        if (pullStart2 == true)
                        {
                            if (pullReset2 == false)
                            {
                                if (Input.GetKey(t_Action) && pullRelease2 == false)
                                {
                                    pullBar2 += pull2 * Time.deltaTime;
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
                        }
                        #endregion
                    }
                }
            }
            else
            {   //Set Game choice for player one
                if (Input.GetKeyDown(o_ChoiceOne))
                {
                    o_gamechoice = O_gamechoice.punchingBag;
                    choiceSelected = true;
                }
                if (Input.GetKeyDown(o_ChoiceTwo))
                {
                    o_gamechoice = O_gamechoice.treadmill;
                    choiceSelected = true;
                }
                if (Input.GetKeyDown(o_ChoiceThree))
                {
                    o_gamechoice = O_gamechoice.pullup;
                    choiceSelected = true;
                }

                //Set Game choice for player Two
                if (Input.GetKeyDown(t_ChoiceOne))
                {
                    t_gamechoice = T_gamechoice.punchingBag;
                    choiceSelected = true;
                }
                if (Input.GetKeyDown(t_ChoiceTwo))
                {
                    t_gamechoice = T_gamechoice.treadmill;
                    choiceSelected = true;
                }
                if (Input.GetKeyDown(t_ChoiceThree))
                {
                    t_gamechoice = T_gamechoice.pullup;
                    choiceSelected = true;
                }
            }
        }
        GameModeToggler();
        CheckPlayerOneSpeed();
        CheckPlayerTwoSpeed();
    }
    void GameModeToggler()
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
}
// balbalbla