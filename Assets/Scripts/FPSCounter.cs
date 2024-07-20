using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI FPSDisplay;
    private float time;
    private float waitTime = 1f;
    private int frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= waitTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FPSDisplay.text = frameRate.ToString() + " FPS";

            time -= waitTime;
            frameCount = 0;
        }
    }
}
