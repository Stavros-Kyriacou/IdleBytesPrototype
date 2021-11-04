using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Perk : MonoBehaviour, IPointerDownHandler
{
    public string title;
    public string description;
    public int maxLevel;
    [HideInInspector] public int currentLevel;
    [SerializeField] private List<Perk> perkRequirements;
    [SerializeField] private int[] requirementLevels;
    [HideInInspector] public bool isUnlocked = false;
    [SerializeField] private Text titleText;

    private void Awake()
    {
        currentLevel = 0;
    }
    private void Start()
    {
        titleText.text = title;
        if (perkRequirements.Count > 0)
        {
            description += "\nRequirements:\n";
            for (int i = 0; i < perkRequirements.Count; i++)
            {
                description += perkRequirements[i].title + "\n";
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PerkTree.Instance.SelectPerk(this);
    }
    public void UnlockPerk()
    {
        //check if the required perks have been unlocked
        //if even one of the is not unlocked, do not unlock the perk
        //print a list of the perks that havent been unlocked

        List<Perk> lockedPerks = new List<Perk>();

        if (perkRequirements.Count > 0)
        {
            foreach (Perk perk in perkRequirements)
            {
                if (!perk.isUnlocked)
                {
                    lockedPerks.Add(perk);
                }
            }
        }
        if (lockedPerks.Count > 0)
        {
            Debug.Log("Perk cannot be unlocked, requirements not met");
            foreach (var perk in lockedPerks)
            {
                Debug.Log(perk.title + " still locked");
            }
        }
        else
        {
            if (PerkTree.Instance.perkPoints > 0 && this.currentLevel < this.maxLevel)
            {
                PerkTree.Instance.perkPoints--;
                this.isUnlocked = true;
                this.currentLevel++;
            }
            else if (this.currentLevel >= this.maxLevel)
            {
                Debug.Log("Perk is max level");
            }
            else
            {

                Debug.Log("Not enough perk points available");
            }
        }
    }
}
