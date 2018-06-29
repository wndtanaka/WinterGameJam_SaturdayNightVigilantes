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
                    if(o_gamechoice == O_gamechoice.punchingBag)
                    {
                        //anim.setBool("o_isPunching", true);
                    }
                    if (o_gamechoice == O_gamechoice.treadmill)
                    {
                        //anim.setBool("o_isRunning", true);
                    }
                    if (o_gamechoice == O_gamechoice.pullup)
                    {
                        //anim.setBool("o_isPullingUp", true);
                    }

                }
                if (Input.GetKeyDown(t_Action))
                {
                    //play animation etc for correct mini-game for player two
                    if (t_gamechoice == T_gamechoice.punchingBag)
                    {
                        t_Punch = !t_Punch;
                        //anim.setBool("t_Punch", t_Punch);
                    }
                    if (t_gamechoice == T_gamechoice.treadmill)
                    {
                        t_RunSpeed += 0.5f;
                        //anim.setFloat("t_RunSpeed", t_RunSpeed);
                    }
                    if (t_gamechoice == T_gamechoice.pullup)
                    {
                        //anim.setBool("t_isPullingUp", true);
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

        //Player One Run Speed stuff
        o_RunSpeed -= 0.5f;
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
}
