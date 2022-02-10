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
    public List<Computer> computerList;
    public PrestigeMenu PrestigeMenu;

    [Header("Main UI")]
    public TextMeshProUGUI dollarsText;
    public TextMeshProUGUI dollarsPerSecText;
    public TextMeshProUGUI gemsText;
    [SerializeField] private Text dateTimeText;
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
        PrestigeMenu.UpdateUI(dollarsThisFrame);

        this.dollarsText.text = $"${this.dollars.ToString("F0")}";
        this.dollarsPerSecText.text = $"${this.dollarsPerSec.ToString("F0")}/s";
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
