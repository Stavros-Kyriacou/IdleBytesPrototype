using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkbenchManager : MonoBehaviour
{
    public RectTransform tierUpgradeMenu;
    public RectTransform levelUpgradeMenu;
    public RectTransform deconstructionMenu;
    public Vector2 offScreen = 1000 * Vector2.up;
    public TextMeshProUGUI changeMenuButtonText;
    public List<Image> menuButtonImages;
    public TextMeshProUGUI scrapText;

    private void OnEnable()
    {
        ChangeMenu(1);
    }
    public void ChangeMenu(int index)
    {
        switch (index)
        {
            //Tier Upgrade Menu
            case 1:
                tierUpgradeMenu.anchoredPosition = Vector2.zero;
                levelUpgradeMenu.anchoredPosition = offScreen;
                deconstructionMenu.anchoredPosition = offScreen;
                menuButtonImages[0].color = Color.green;
                menuButtonImages[1].color = Color.white;
                menuButtonImages[2].color = Color.white;
                break;
            //Level Upgrade Menu
            case 2:
                tierUpgradeMenu.anchoredPosition = offScreen;
                levelUpgradeMenu.anchoredPosition = Vector2.zero;
                deconstructionMenu.anchoredPosition = offScreen;
                menuButtonImages[0].color = Color.white;
                menuButtonImages[1].color = Color.green;
                menuButtonImages[2].color = Color.white;
                break;
            //Deconstruction Menu
            case 3:
                tierUpgradeMenu.anchoredPosition = offScreen;
                levelUpgradeMenu.anchoredPosition = offScreen;
                deconstructionMenu.anchoredPosition = Vector2.zero;
                menuButtonImages[0].color = Color.white;
                menuButtonImages[1].color = Color.white;
                menuButtonImages[2].color = Color.green;
                break;
            default:
                break;
        }
    }
    public void UpdateText()
    {
        scrapText.text = $"Scrap: {Inventory.Instance.Scrap}";
    }
}