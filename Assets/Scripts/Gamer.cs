using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamer : MonoBehaviour
{
    public float Hunger
    {
        get
        {
            if (timeRemaining == 0)
            {
                return 1;
            }
            else
            {
                return 1 - ((float)timeRemaining / (float)maxDepletionTime);
            }
        }
        private set { }
    }
    public int maxDepletionTime = 10;
    public int timeRemaining;
    public ProgressBar progressBar;

    private void Start()
    {
        timeRemaining = maxDepletionTime;
        StartCoroutine("DepleteHunger");
    }
    IEnumerator DepleteHunger()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            progressBar.UpdateFill(timeRemaining, maxDepletionTime);
            if (timeRemaining <= 0)
            {
                StopCoroutine("DepleteHunger");
                break;
            }
        }
    }
    public void GetFood()
    {
        StopCoroutine("DepleteHunger");
        var money = ((GameController.Instance.dollarsPerSec / 2) * (this.maxDepletionTime - this.timeRemaining)) * VendingMachine.Instance.GetHungerBonus(this.Hunger);

        GameController.Instance.dollars += money;

        timeRemaining = maxDepletionTime;
        progressBar.UpdateFill(timeRemaining, maxDepletionTime);
        StartCoroutine("DepleteHunger");
    }
}