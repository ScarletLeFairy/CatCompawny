﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClock : MonoBehaviour {

    private Vector3 timerPos;
    public Text timerText;
    
    private bool stop = true;

    private float timeLeft = 0;
    private float minutes;
    private float seconds;

    private Camera cam;
    private GameManager gameManager;

    public Vector3 TimerPosOffset;

    int[] snoozeTime = { 60, 50, 40, 30, 20 };
    private int snoozeIndex = 0;

    private Transform parentTransform;

    AudioSource audioSource;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parentTransform = timerText.gameObject.GetComponentInParent<Transform>();

        audioSource = GetComponent<AudioSource>();
    }


    // Use this for initialization
    void Start () {
        timerPos = gameObject.transform.position;
        timerPos += TimerPosOffset;
        cam = Camera.main;
    }

    public void StartTimer()
    {
        stop = false;
        timeLeft = snoozeTime[0];
        snoozeIndex = 1;
        Update();
        StartCoroutine(UpdateAlarmClock());
        Debug.Log("Timer is started");
    }
	
	// Update is called once per frame
	void Update () {
        timerPos = gameObject.transform.position;
        timerPos += TimerPosOffset;
        timerText.transform.position = timerPos;
        timerText.transform.LookAt(cam.transform, Vector3.up);
        timerText.transform.Rotate(new Vector3(0, 180, 0));

        // just for testing
        //if (Input.GetKeyDown(KeyCode.JoystickButton2))
        //{
        //    Snooze();
        //}


        if (!stop)
        {
            timeLeft -= Time.deltaTime;

            minutes = Mathf.Floor(timeLeft / 60);
            seconds = timeLeft % 60;
            if (seconds > 59)
            {
                seconds = 59;
            }            
            if (minutes < 0)
            {
                stop = true;
                Debug.Log("Stop");
                minutes = 0;
                seconds = 0;
            }
        } else if (stop && gameManager.GameIsRunning) {
            audioSource.Play();
            gameManager.EndGame();
        }
    }

    private IEnumerator UpdateAlarmClock()
    {
        while (!stop)
        {
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            //Debug.Log(minutes + "."+seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }

    /**
     * Snooze only works if there is a Snooze time left in the array (not endless)
     **/
    public void Snooze()
    {
        if (snoozeIndex < snoozeTime.Length && timeLeft < snoozeTime[(snoozeIndex + 1) % snoozeTime.Length ])
        {
            Debug.Log("Snooze " + (snoozeIndex + 1));
            timeLeft = snoozeTime[snoozeIndex++];
        } 
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent(typeof(PlayerCat))) {
            Snooze();
        }
    }*/

}
