using UnityEngine;

public class Timer
{

    private float totalTime;
    private float timer = 0f;
    private bool isTimerActivated = false;

    public float GetCurrentTime()
    {
        return totalTime - timer;
    }

    public Timer(float totalTime)
    {
        this.totalTime = totalTime;
        timer = totalTime;
    }

    public void SubtractTimerByValue(float amount)
    {
        if (IsTimerEnded()) return;
        timer -= amount;
    }

    public bool IsTimerEnded()
    {
        if (!isTimerActivated) return false;
        if (timer <= 0f)
        {
            isTimerActivated = false;
            return true;
        }
        return false;
    }

    public bool IsTimerActive()
    {
        if (isTimerActivated && timer > 0f) return true;
        return false;
    }

    public void ActivateTimer()
    {
        isTimerActivated = true;
        timer = totalTime;
    }
    public void SetTimerTime(float newTime)
    {
        this.totalTime = newTime;
    }
}

