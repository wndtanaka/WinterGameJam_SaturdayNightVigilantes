using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // making GameManager a singleton, so accessible easily using instance
    public static GameManager instance;

    [SerializeField]
    GameObject breakUI;
    [SerializeField]
    GameObject gameUI;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void BreakTime()
    {
        breakUI.SetActive(true);
        gameUI.SetActive(false);
    }

    public void GameTime()
    {
        gameUI.SetActive(true);
        breakUI.SetActive(false);
    }
}
