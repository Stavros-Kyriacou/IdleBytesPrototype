using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int Duration;
    private int TimeRemaining;
    [HideInInspector]
    public bool IsComplete;
    // [HideInInspector]
    public CancelCraftMenu cancelCraftMenu;
    public void StartTimer(int duration)
    {
        this.Duration = duration;
        this.TimeRemaining = this.Duration;
        this.IsComplete = false;
        StartCoroutine("CountdownTimer");
    }
    IEnumerator CountdownTimer()
    {
        while (this.TimeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            this.TimeRemaining--;

            UpdateText();
            if (this.TimeRemaining <= 0)
            {
                this.IsComplete = true;
                StopCoroutine("Timer");
                Debug.Log("Grace Time finished");
                break;
            }
        }
    }
    public void UpdateText()
    {
        if (this.cancelCraftMenu != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(this.TimeRemaining);
            this.cancelCraftMenu.GraceTimeText.text = "Grace Time: " + time.ToString(@"hh\:mm\:ss");
            if (this.TimeRemaining > 0)
            {
                //grace timer is still active
                this.cancelCraftMenu.DescriptionText.text = $"If you cancel now, you will get your components back and x scrap back ";
            }
            else
            {
                //grace timer finished
                this.cancelCraftMenu.DescriptionText.text = $"Grace Time over. If you cancel now, you will not get your components back but will receive x scrap";
            }
        }
    }
}
