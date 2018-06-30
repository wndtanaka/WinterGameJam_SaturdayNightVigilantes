using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // making GameManager a singleton, so accessible easily using instance
    public static GameManager instance;

    public GameChoice gameChoiceOne;
    public GameChoice gameChoiceTwo;

    [Header("Game Time")]
    [SerializeField]
    float gameTime = 3;
    public Text gameTimeText;

    [Header("Break Time")]
    [SerializeField]
    float breakTime = 2;
    public Text breakTimeText;

    [Header("Train Time")]
    [SerializeField]
    float trainTime = 10f;
    public Text trainTimeText;

    [Header("Canvas Group")]
    [SerializeField]
    CanvasGroup breakUI;
    [SerializeField]
    CanvasGroup gameUI;
    [SerializeField]
    CanvasGroup trainUI;
    public CanvasGroup playerOnePanel;
    public CanvasGroup playerTwoPanel;

    [SerializeField]
    Animator animHUD;
    [SerializeField]
    Animator animTrainOne;
    [SerializeField]
    Animator animTrainTwo;
    [SerializeField]
    Animator animTraining;

    [Header("Training Camera")]
    public Camera CameraOne;
    public Camera CameraTwo;

    public TrainingMode[] trainingMode;

    bool isRoundStart = true;
    bool isBreakStart = false;
    bool isTrainingStart = false;

    bool trainingOneSelected = false;
    bool trainingTwoSelected = false;

    int indexSelection;

    ChangeView camView;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        camView = Camera.main.GetComponent<ChangeView>();
    }
    private void Update()
    {
        if (trainingOneSelected)
        {
            playerOnePanel.alpha = Mathf.Lerp(playerOnePanel.alpha, 0, 10 * Time.deltaTime);
        }
        if (trainingTwoSelected)
        {
            playerTwoPanel.alpha = Mathf.Lerp(playerTwoPanel.alpha, 0, 10 * Time.deltaTime);
        }
        if (isRoundStart)
        {
            GameObject.FindGameObjectWithTag("Player One").GetComponent<PlayerController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player Two").GetComponent<PlayerController>().enabled = true;
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;
            }
            else
            {
                isBreakStart = true;
                isRoundStart = false;
                ChangeRounds();
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player One").GetComponent<PlayerController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player Two").GetComponent<PlayerController>().enabled = false;
        }
        if (isBreakStart)
        {
            isRoundStart = false;
            isTrainingStart = false;
            animHUD.SetBool("OpenBreakUI", true);
            camView.isTraining = true;

            breakUI.alpha = Mathf.Lerp(breakUI.alpha, 1, 2 * Time.deltaTime);

            if (breakTime > 0)
            {
                breakTime -= Time.deltaTime;
                TrainingChoices();
            }
            else
            {
                animTraining.SetBool("StartTraining", true);
                StartCoroutine(StartTraining());
                ChangeRounds();
            }
        }
        if (isTrainingStart)
        {
            animHUD.SetBool("OpenBreakUI", false);
            if (trainTime > 0)
            {
                trainTime -= Time.deltaTime;
            }
            else
            {
                isRoundStart = true;
                isTrainingStart = false;
                animTraining.SetBool("StartTraining", false);
                ChangeRounds();
            }
        }

        breakTimeText.text = breakTime.ToString("F0");
        gameTimeText.text = gameTime.ToString("F0");
        trainTimeText.text = trainTime.ToString("F0");
    }

    public void ChangeRounds()
    {
        gameTime = 10;
        breakTime = 5;
        trainTime = 10;
        if (isRoundStart)
        {
            gameUI.alpha = 1;
            breakUI.alpha = 0;
            trainUI.alpha = 0;
        }
        if (isBreakStart)
        {
            gameUI.alpha = 0;
            trainUI.alpha = 0;
        }
        if (isTrainingStart)
        {
            gameUI.alpha = 0;
            breakUI.alpha = 0;
            trainUI.alpha = 1;
        }
    }

    public void TrainingChoices()
    {
        #region Training Selection for Player One
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            trainingOneSelected = true;
            CameraOne.transform.position = trainingMode[0].trainingMode[0].transform.position;
            PlayerOne.Instance.TrainingResult(GameChoice.PullUp, 10);
            gameChoiceOne = GameChoice.PullUp;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            trainingOneSelected = true;
            CameraOne.transform.position = trainingMode[0].trainingMode[1].transform.position;
            PlayerOne.Instance.TrainingResult(GameChoice.Treadmill, 10);
            gameChoiceOne = GameChoice.Treadmill;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            trainingOneSelected = true;
            CameraOne.transform.position = trainingMode[0].trainingMode[2].transform.position;
            PlayerOne.Instance.TrainingResult(GameChoice.PunchingBag, 10);
            gameChoiceOne = GameChoice.PunchingBag;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            trainingOneSelected = true;
            CameraOne.transform.position = trainingMode[0].trainingMode[3].transform.position;
            PlayerOne.Instance.TrainingResult(GameChoice.Rest, 0);
            gameChoiceOne = GameChoice.Rest;
        }
        #endregion
        #region Training Selection for Player Two
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            trainingTwoSelected = true;
            CameraTwo.transform.position = trainingMode[1].trainingMode[0].transform.position;
            PlayerTwo.Instance.TrainingResult(GameChoice.PullUp, 10);
            gameChoiceTwo = GameChoice.PullUp;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            trainingTwoSelected = true;
            CameraTwo.transform.position = trainingMode[1].trainingMode[1].transform.position;
            PlayerTwo.Instance.TrainingResult(GameChoice.Treadmill, 10);
            gameChoiceTwo = GameChoice.Treadmill;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            trainingTwoSelected = true;
            CameraTwo.transform.position = trainingMode[1].trainingMode[2].transform.position;
            PlayerTwo.Instance.TrainingResult(GameChoice.PunchingBag, 10);
            gameChoiceTwo = GameChoice.PunchingBag;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            trainingTwoSelected = true;
            CameraTwo.transform.position = trainingMode[1].trainingMode[3].transform.position;
            PlayerTwo.Instance.TrainingResult(GameChoice.Rest, 0);
            gameChoiceTwo = GameChoice.Rest;
        }
        #endregion
    }

    public IEnumerator StartTraining()
    {
        isBreakStart = false;
        isRoundStart = false;
        isTrainingStart = true;
        yield return new WaitForSeconds(3);
    }

    [System.Serializable]
    public class TrainingMode
    {
        public Transform[] trainingMode;
    }
}
