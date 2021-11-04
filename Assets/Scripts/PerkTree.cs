using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PerkTree : MonoBehaviour
{
    public static PerkTree Instance;
    public int perkPoints = 5;
    [SerializeField] public List<Perk> perks;

    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text levelText;
    [HideInInspector] public Perk selectedPerk;
    public UnityEvent Unlock;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Unlock.AddListener(PlayerStats.Instance.UpdateStats);
    }
    public void SelectPerk(Perk perk)
    {
        this.selectedPerk = perk;
        UpdateText();
    }
    public void UpdateText()
    {
        if (selectedPerk != null)
        {
            titleText.text = selectedPerk.title;
            descriptionText.text = selectedPerk.description;
            levelText.text = selectedPerk.currentLevel.ToString() + "/" + selectedPerk.maxLevel.ToString();
        }
    }
    public void UnlockPerk()
    {
        if (selectedPerk != null)
        {
            selectedPerk.UnlockPerk();
        }
        UpdateText();
        
        Unlock?.Invoke();
    }
}
