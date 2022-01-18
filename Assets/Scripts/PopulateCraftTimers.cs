using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateCraftTimers : MonoBehaviour
{
    [SerializeField] private Transform ContentContainer;
    [SerializeField] private GameObject CraftingTimerPrefab;
    [SerializeField] private int ItemsToGenerate;
    private int maxCraftSlots = 10;
    [SerializeField] private WorkbenchTierMenu TierMenu;
    public int upgradeSlotCost;

    void Start()
    {
        for (int i = 0; i < ItemsToGenerate; i++)
        {
            // var item_go = Instantiate(CraftingTimerPrefab);
            // //parent the item to the content container
            // item_go.transform.SetParent(ContentContainer);
            // //reset the item's scale -- this can get munged with UI prefabs
            // item_go.transform.localScale = Vector2.one;
            AddTimer();
        }
    }
    public void AddTimer()
    {
        var timer = Instantiate(CraftingTimerPrefab);
        timer.transform.SetParent(ContentContainer);
        timer.transform.localScale = Vector2.one;

        TierMenu.craftingTimers.Add(timer.GetComponent<CraftingTimer>());
    }
    public void BuyUpgradeSlot()
    {
        if (TierMenu.craftingTimers.Count < maxCraftSlots)
        {
            if (upgradeSlotCost <= Inventory.Instance.Gems)
            {
                Inventory.Instance.Gems -= upgradeSlotCost;
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
