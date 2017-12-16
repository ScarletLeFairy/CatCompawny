using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClock : MonoBehaviour {

    private static int timer = 0;
    public static int Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    Vector2 screenPos;
    Vector3 timerPos;

    public Text timerText;

    private int startTime = 20;

    public float timeLeft = 0;
    public bool stop = true;

    private float minutes;
    private float seconds;

    Camera cam;

    public Vector3 TimerPosOffset;

    private int[] snoozeTime = { 25, 20, 15, 10, 5 };
    private int snoozeIndex = 0;

    /*
     * TODO: Lauter ticken die letzten 5 Sekunden --> nur dann ist Snooze möglich?
     */


    // Use this for initialization
    void Start () {
        timerPos = gameObject.transform.position;
        timerPos += TimerPosOffset;
        cam = Camera.main;
        //timerText.transform.SetPositionAndRotation(position, timerText.transform.rotation);
        StartTimer();
    }

    void StartTimer()
    {
        // TODO: do this on pressing start later
        stop = false;
        timeLeft = startTime;
        snoozeIndex = 0;
        Update();
        StartCoroutine(UpdateAlarmClock());
        Debug.Log("Timer is started");
    }
	
	// Update is called once per frame
	void Update () {
        screenPos = cam.WorldToScreenPoint(timerPos);
        timerText.transform.position = timerPos;
        timerText.transform.LookAt(cam.transform, transform.up);
        timerText.transform.Rotate(new Vector3(0, 180, 0));
        //timerText.transform.localEulerAngles = new Vector3(0, 180, 0);

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
    private void Snooze()
    {
        if (timeLeft <= 5 && snoozeIndex < snoozeTime.Length)
        {
            Debug.Log("Snooze " + (snoozeIndex + 1));
            timeLeft = snoozeTime[snoozeIndex++];
        } 
    }

}
