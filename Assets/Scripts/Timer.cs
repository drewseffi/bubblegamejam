using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text text;

    private bool timerRunning = true;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (time >= 0)
            {
                time += Time.deltaTime;
                DisplayTime(time);
            }
        }
    }

    void DisplayTime(float display)
    {
        display += 1;
        float minutes = Mathf.FloorToInt (display / 60);
        float seconds = Mathf.FloorToInt (display % 60);

        text.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
    }
}
