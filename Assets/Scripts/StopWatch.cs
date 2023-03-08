using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting.FullSerializer;

public class StopWatch : MonoBehaviour
{

    bool StopWatchActive = true;
    static float staticTime = 0;
    public float currentTime = staticTime; 
    public TMP_Text currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = staticTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (StopWatchActive){
            currentTime = currentTime + Time.deltaTime;
            staticTime = currentTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:ff");
        }

    public void StartStopWatch(){
        StopWatchActive = true;
    }

    public void StopStopWatch(){
        StopWatchActive = false;
    }
}
