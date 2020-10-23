using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public AudioSource audio;

    void Update()
    {
        Debug.Log(Time.timeScale);
    }

    public void SlowTime()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        audio.pitch = Mathf.Lerp(1, 0.35f,25 * Time.timeScale);
    }

    public void SpeedupTime()
    {
        Time.timeScale += (100)* Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        audio.pitch = Mathf.Lerp(0.35f, 1, 25 * Time.timeScale);
    }
}
