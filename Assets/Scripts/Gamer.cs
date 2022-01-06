using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamer : MonoBehaviour
{
    public float hunger;
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
            progressBar.GetCurrentFill(timeRemaining, maxDepletionTime);
            if (timeRemaining <= 0)
            {
                StopCoroutine("DepleteHunger");
                break;
            }
        }
    }
    public void GetFood()
    {
        if (timeRemaining <= 0)
        {
            GameController.Instance.dollars += 10000;
            timeRemaining = maxDepletionTime;
            progressBar.GetCurrentFill(timeRemaining, maxDepletionTime);
            StartCoroutine("DepleteHunger");
        }
        else
        {
            Debug.Log("The gamer is not hungry enough yet");
        }
    }
}