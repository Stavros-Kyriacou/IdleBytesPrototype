using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private int _duration;
    private int _timeRemaining;
    public int TimeRemaining
    {
        get
        {
            return this._timeRemaining;
        }
        private set { }
    }
    [HideInInspector] public bool IsAvailable;
    [HideInInspector] public bool IsComplete;
    public UnityEvent OnTimerCountdown;
    public UnityEvent OnTimerStarted;
    public UnityEvent OnTimerComplete;
    private void Awake()
    {
        IsAvailable = true;
        IsComplete = false;
    }
    public void StartTimer(int durationInSeconds)
    {
        if (this.IsAvailable)
        {
            this._duration = durationInSeconds;
            this._timeRemaining = this._duration;
            this.IsAvailable = false;
            this.IsComplete = false;
            OnTimerStarted.Invoke();
            StartCoroutine("Countdown");
        }
        else
        {
            Debug.Log("Timer not available");
        }
    }
    IEnumerator Countdown()
    {
        while (this._timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            this._timeRemaining--;
            OnTimerCountdown.Invoke();

            if (this._timeRemaining <= 0)
            {
                this.IsComplete = true;
                OnTimerComplete.Invoke();
                StopCoroutine("Countdown");
                Debug.Log("Timer finished");
                break;
            }
        }
    }
    public void ResetTimer()
    {
        StopCoroutine("Countdown");
        this.IsAvailable = true;
        this.IsComplete = false;
        this._timeRemaining = this._duration;
    }
}