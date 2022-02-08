using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public double dollars;
    public double dollarsPerSec;
    private double dollarsThisPrestige;
    public List<Computer> computerList;

    [Header("Main UI")]
    public TextMeshProUGUI dollarsText;
    public TextMeshProUGUI dollarsPerSecText;
    public TextMeshProUGUI gemsText;
    [SerializeField] private Text dateTimeText;

    [Header("Prestige Menu")]
    public TextMeshProUGUI DollarsEarnedText;
    public TextMeshProUGUI NewTokensText;

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
        UpdateGemsText();
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
        var dollarsThisFrame = this.dollarsPerSec * Time.deltaTime;
        this.dollars += dollarsThisFrame;
        this.dollarsThisPrestige += dollarsThisFrame;

        this.dollarsText.text = $"${this.dollars.ToString("F0")}";
        this.dollarsPerSecText.text = $"${this.dollarsPerSec.ToString("F0")}/s";
        this.DollarsEarnedText.text = $"Dollars this prestige: ${this.dollarsThisPrestige.ToString("F0")}";
    }
    public void CalculateDollarsPerSec()
    {
        // Debug.Log("Calculating $/s");
        this.dollarsPerSec = 0;

        foreach (Computer c in computerList)
        {
            this.dollarsPerSec += c.totalDPS;
        }
    }
    public void UpdateGemsText()
    {
        gemsText.text = $"Gems: {Inventory.Instance.Gems}";

    }
}
