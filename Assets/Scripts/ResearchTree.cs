using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTree : MonoBehaviour
{
    //handle counting down the research time
    //get the difference between login time and research start time in seconds
    //convert the seconds to hours and seconds
    public static ResearchTree Instance;
    public List<Research> researches;
    public int scrap = 100;

    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text levelText;
    public Research selectedResearch;
    public ResearchCounter researchCounter;
    private void Awake()
    {
        Instance = this;
    }
    public void SelectResearch(Research research)
    {
        this.selectedResearch = research;
        UpdateDescriptionBox();
    }
    public void UpdateDescriptionBox()
    {
        if (selectedResearch != null)
        {
            titleText.text = selectedResearch.title;
            descriptionText.text = selectedResearch.description;
            levelText.text = selectedResearch.currentLevel.ToString() + "/" + selectedResearch.maxLevel.ToString();
        }
    }
    public void Research()
    {
        if (selectedResearch != null)
        {
            selectedResearch.StartResearch();
            researchCounter.selectedResearch = this.selectedResearch;
        }
        UpdateDescriptionBox();
    }
}
