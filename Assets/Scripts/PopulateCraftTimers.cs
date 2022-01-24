using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateCraftTimers : MonoBehaviour
{
    [SerializeField] private Transform ContentContainer;
    [SerializeField] private GameObject CraftSlotPrefab;
    [SerializeField] private int ItemsToGenerate;
    private int maxCraftSlots = 10;
    [SerializeField] private WorkbenchTierMenu TierMenu;
    public CancelCraftMenu CancelCraftMenu;
    public int upgradeSlotCost;
    public TextMeshProUGUI upgradeCostText;

    void Start()
    {
        for (int i = 0; i < ItemsToGenerate; i++)
        {
            AddTimer();
        }
        upgradeCostText.text = $"Cost: {this.upgradeSlotCost} Gems";
    }
    public void AddTimer()
    {
        //Instantiate crafting slot, set its parent and scale
        var go = Instantiate(CraftSlotPrefab);
        go.transform.SetParent(ContentContainer);
        go.transform.localScale = Vector2.one;

        //Add onClick method to the cancel button
        var craftingSlot = go.GetComponent<CraftingSlot>();
        craftingSlot.CancelButton.onClick.AddListener(()=> CancelCraftMenu.Cancel(craftingSlot));

        //Add to list of crafting slots
        TierMenu.CraftingSlots.Add(craftingSlot);
    }
    public void BuyUpgradeSlot()
    {
        if (TierMenu.CraftingSlots.Count < maxCraftSlots)
        {
            if (upgradeSlotCost <= Inventory.Instance.Gems)
            {
                Inventory.Instance.Gems -= upgradeSlotCost;
                GameController.Instance.UpdateGemsText();
                AddTimer();
            }
            else
            {
                Debug.Log("Not enough gems! Buy more from the shop please and make us rich thx <3");
            }
        }
        else
        {
            Debug.Log("Cannot buy anymore craft slots");
        }
    }
}
