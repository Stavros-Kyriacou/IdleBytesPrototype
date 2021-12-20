using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private Text dateTimeText;
    public double dollars;
    public double dollarsPerSec;
    public Text dollarsText;
    public Text dollarsPerSecText;
    public List<Computer> computerList;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        CalculateDollarsPerSec();
    }

    public void SetTime()
    {
        if (WorldTimeAPI.Instance.IsTimeLoaded)
        {
            dateTimeText.text = WorldTimeAPI.Instance.GetCurrentDateTime().ToString() + ", Day of week: " + WorldTimeAPI.Instance.dayOfWeek;
        }
    }
    private void Update()
    {
        this.dollars += this.dollarsPerSec * Time.deltaTime;
        this.dollarsText.text = $"${this.dollars.ToString("F0")}";
        this.dollarsPerSecText.text = $"${this.dollarsPerSec.ToString("F0")}/s";
    }
    public void CalculateDollarsPerSec()
    {
        Debug.Log("Calculating $/s");
        this.dollarsPerSec = 0;

        foreach (Computer c in computerList)
        {
            this.dollarsPerSec += c.totalDPS;
        }
    }
}
