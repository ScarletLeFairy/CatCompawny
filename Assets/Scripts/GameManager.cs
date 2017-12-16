using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private bool gameRunning = false;

    public Text gameTitle;
    public Text highscoreTitle;
    public Text pointsText;
    public Text pointsCount;
    public Text highscoreText;
    public Text startText;
    public InputField inputField;

    private void Awake()
    {
        gameTitle.enabled = true;
        startText.enabled = true;

        pointsText.enabled = false;
        pointsCount.enabled = false;
        highscoreTitle.enabled = false;
        highscoreText.enabled = true;

        // TODO get alarm clock and timer or use event
    }

    public void StartGame()
    {
        gameTitle.enabled = false;
        highscoreTitle.enabled = false;
        highscoreText.enabled = false;
        startText.enabled = false;
        inputField.enabled = false;

        pointsText.enabled = true;
        pointsCount.enabled = true;

        gameRunning = true;
    }

    public void EndGame()
    {
        gameTitle.enabled = true;
        highscoreTitle.enabled = true;
        highscoreText.enabled = true;
        startText.enabled = true;

        pointsText.enabled = false;
        pointsCount.enabled = false;

        gameRunning = false;
    }

    // Use this for initialization
    void Start () {

        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Start the game");
            StartGame();
        }
    }

    
}
