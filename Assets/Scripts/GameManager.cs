﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private bool gameIsRunning = false;
    public bool GameIsRunning 
    {
        get { return gameIsRunning; }
    }

    public Text gameTitle;
    public Text highscoreTitle;
    public Text pointsText;
    public Text pointsCount;
    public Text highscoreText;
    public Text startText;
    public InputField inputField;

    public AlarmClock alarmClock;

    private HighscoreManager highscoreManager;

    private int points = 0;

    private void Awake()
    {
        gameTitle.enabled = true;
        startText.enabled = true;

        pointsText.enabled = false;
        pointsCount.enabled = false;
        highscoreTitle.enabled = false;
        highscoreText.enabled = false;
        inputField.gameObject.SetActive(false); 

        highscoreManager = GetComponent<HighscoreManager>();

        // TODO get alarm clock and timer or use event
    }


    public void StartGame()
    {
        points = 0;

        gameTitle.enabled = false;
        highscoreTitle.enabled = false;
        highscoreText.enabled = false;
        startText.enabled = false;
        inputField.enabled = false;

        pointsText.enabled = true;
        pointsCount.enabled = true;

        gameIsRunning = true;

        alarmClock.StartTimer();
    }

    bool endGAME = false;
    float time = 0;

    public void EndGame()
    {
        gameTitle.enabled = true;
        highscoreTitle.enabled = true;
        highscoreText.enabled = true;
        startText.enabled = true;

        pointsText.enabled = false;
        pointsCount.enabled = false;

        gameIsRunning = false;

        highscoreManager.CheckToSaveScore(points);

        endGAME = true;
        time = Time.time;

        // TODO get score
    }

    //// Use this for initialization
    //void Start () {

        
    //}

    // Update is called once per frame
    void Update () {

        if (endGAME && time + 5 < Time.time)
        {
            Application.LoadLevel("BlackScreen");
        }

        if (!gameIsRunning)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton2) && !endGAME)
            {
                Debug.Log("Start the game");
                StartGame();
            }
        } else
        {
            pointsCount.text = points.ToString();
        }
        
    }

    public void AddPoints(int p)
    {
        points += p;
    }

    
}
