using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public AudioClip[] songs;

    AudioSource audi;
    GameManager manager;

    public bool isPlayingRoundSong = false;
    public bool isPlayingTrainingSong = false;
    // Use this for initialization
    void Start()
    {
        audi = GetComponent<AudioSource>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isRoundStart && !isPlayingRoundSong)
        {
            audi.clip = songs[0];
            audi.Play();
            isPlayingTrainingSong = false;
            isPlayingRoundSong = true;
        }
        else if ((manager.isTrainingStart || manager.isBreakStart) && !isPlayingTrainingSong)
        {
            audi.clip = songs[1];
            audi.Play();
            isPlayingRoundSong = false;
            isPlayingTrainingSong = true;
        }
    }
}
