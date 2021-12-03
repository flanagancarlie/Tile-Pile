using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float timePassed = 0;
    public bool timerIsRunning = false;
    public Text timeText;

    public void Start() {
        // Starts the timer automatically
        timerIsRunning = true;
        timePassed = 0;
    }

    public void Update() {
        if(timerIsRunning) {
            if(timePassed >= 0) {
                timePassed += Time.deltaTime;
                DisplayTime(timePassed);
            }
            else {
                Debug.Log("Time has run out!");
                timePassed = 0;
                timerIsRunning = false;
            }
        }
    }

    public void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        timerIsRunning = false;
    }
}

