using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ComponentInventory : MonoBehaviour
{
    [HideInInspector]
    public int SelectedComponent { get; private set; }
    [HideInInspector]
    public int SelectedTier { get; private set; }
    [HideInInspector]
    public int SelectedLevel { get; private set; }
    public Image previousComponentImage;
    public Image previousTierImage;
    public Image previousLevelImage;
    public List<Image> componentButtonImages;
    public List<Image> tierButtonImages;
    public List<Image> levelButtonImages;
    public List<TextMeshProUGUI> componentAmountTexts;

    [Header("Component Selection")]
    [SerializeField]
    [Tooltip("Whether a component type should be selected by default")]
    private bool componentSelected;
    [SerializeField]
    [Tooltip("Whether a component button can be deselcted")]
    private bool canDeselectComponent;


    [Header("Tier Selection")]
    [SerializeField]
    [Tooltip("Whether a component tier should be selected by default")]
    private bool tierSelected;
    [SerializeField]
    [Tooltip("Whether a tier button can be deselcted")]
    private bool canDeselectTier;


    [Header("Level Selection")]
    [Tooltip("Whether a component level should be selected by default")]
    [SerializeField]
    private bool levelSelected;
    [SerializeField]
    [Tooltip("Whether a level button can be deselcted")]
    private bool canDeselectLevel;
    public UnityEvent OnComponentChanged;
    public UnityEvent OnTierChanged;
    public UnityEvent OnLevelChanged;

    private void Start()
    {
        if (componentSelected)
        {
            SelectComponent(1);
        }
        if (tierSelected)
        {
            SelectTier(1);
        }
        if (levelSelected)
        {
            SelectLevel(1);
        }
    }
    public void SelectComponent(int index)
    {
        if (canDeselectComponent)
        {
            if (this.previousComponentImage == null)
            {
                //if a component isnt selected, select it
                componentButtonImages[index - 1].color = Color.green;
                this.previousComponentImage = componentButtonImages[index - 1];
                this.SelectedComponent = index;
            }
            else if (this.previousComponentImage == componentButtonImages[index - 1])
            {
                //if the previous is the same as the current, deselect it
                this.previousComponentImage.color = Color.white;
                this.previousComponentImage = null;
                this.SelectedComponent = 0;
            }
            else
            {
                //if a new button is pressed, select it, deselecct previous
                componentButtonImages[index -1].color = Color.green;
                this.previousComponentImage.color = Color.white;
                this.previousComponentImage = componentButtonImages[index -1];
                this.SelectedComponent = index;
            }
        }
        else
        {
            this.SelectedComponent = index;

            if (this.previousComponentImage == null || this.previousComponentImage == componentButtonImages[index - 1])
            {
                //if there isnt a previous image OR the previous is the same as the current => select it
                componentButtonImages[index - 1].color = Color.green;
                this.previousComponentImage = componentButtonImages[index - 1];
            }
            else
            {
                //select a different button from the previous, set a new previous
                componentButtonImages[index - 1].color = Color.green;
                this.previousComponentImage.color = Color.white;
                this.previousComponentImage = componentButtonImages[index - 1];
            }
        }
        OnComponentChanged.Invoke();
    }
    public void SelectTier(int index)
    {
        if (canDeselectTier)
        {
            if (this.previousTierImage == null)
            {
                //if a component isnt selected, select it
                tierButtonImages[index - 1].color = Color.green;
                this.previousTierImage = tierButtonImages[index - 1];
                this.SelectedTier = index;
            }
            else if (this.previousTierImage == tierButtonImages[index - 1])
            {
                //if the previous is the same as the current, deselect it
                this.previousTierImage.color = Color.white;
                this.previousTierImage = null;
                this.SelectedTier = 0;
            }
            else
            {
                //if a new button is pressed, select it, deselecct previous
                tierButtonImages[index -1].color = Color.green;
                this.previousTierImage.color = Color.white;
                this.previousTierImage = tierButtonImages[index -1];
                this.SelectedTier = index;
            }
        }
        else
        {
            this.SelectedTier = index;

            if (this.previousTierImage == null || this.previousTierImage == tierButtonImages[index - 1])
            {
                //if there isnt a previous image OR the previous is the same as the current => select it
                tierButtonImages[index - 1].color = Color.green;
                this.previousTierImage = tierButtonImages[index - 1];
            }
            else
            {
                //select a different button from the previous, set a new previous
                tierButtonImages[index - 1].color = Color.green;
                this.previousTierImage.color = Color.white;
                this.previousTierImage = tierButtonImages[index - 1];
            }
        }
        OnTierChanged.Invoke();
    }
    public void SelectLevel(int index)
    {
        if (canDeselectLevel)
        {
            if (this.previousLevelImage == null)
            {
                //if a component isnt selected, select it
                levelButtonImages[index - 1].color = Color.green;
                this.previousLevelImage = levelButtonImages[index - 1];
                this.SelectedLevel = index;
            }
            else if (this.previousLevelImage == levelButtonImages[index - 1])
            {
                //if the previous is the same as the current, deselect it
                this.previousLevelImage.color = Color.white;
                this.previousLevelImage = null;
                this.SelectedLevel = 0;
            }
            else
            {
                //if a new button is pressed, select it, deselecct previous
                levelButtonImages[index -1].color = Color.green;
                this.previousLevelImage.color = Color.white;
                this.previousLevelImage = levelButtonImages[index -1];
                this.SelectedLevel = index;
            }
        }
        else
        {
            this.SelectedLevel = index;

            if (this.previousLevelImage == null || this.previousLevelImage == levelButtonImages[index - 1])
            {
                //if there isnt a previous image OR the previous is the same as the current => select it
                levelButtonImages[index - 1].color = Color.green;
                this.previousLevelImage = levelButtonImages[index - 1];
            }
            else
            {
                //select a different button from the previous, set a new previous
                levelButtonImages[index - 1].color = Color.green;
                this.previousLevelImage.color = Color.white;
                this.previousLevelImage = levelButtonImages[index - 1];
            }
        }
        OnLevelChanged.Invoke();
    }
    public void UpdateText()
    {
        if (this.SelectedComponent != 0 && this.SelectedTier != 0)
        {
            switch (this.SelectedComponent)
            {
                case 1:
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.cpuInventory[this.SelectedTier - 1, i].ToString()}";
                    }
                    break;
                case 2:
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.gpuInventory[this.SelectedTier - 1, i].ToString()}";
                    }
                    break;
                case 3:
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.ramInventory[this.SelectedTier - 1, i].ToString()}";
                    }
                    break;
                case 4:
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.hddInventory[this.SelectedTier - 1, i].ToString()}";
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void PrintSelection()
    {
        Debug.Log($"Type: {this.SelectedComponent} Tier: {this.SelectedTier} Level: {this.SelectedLevel}");
    }
}
