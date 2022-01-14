using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

public class WorkbenchManager : MonoBehaviour
{
    public GameObject tierUpgradeMenu;
    public GameObject levelUpgradeMenu;
    public TextMeshProUGUI changeMenuButtonText;

    private void OnEnable()
    {
        tierUpgradeMenu.SetActive(true);
        levelUpgradeMenu.SetActive(false);
    }
    public void SwapMenu()
    {
        if (tierUpgradeMenu.activeInHierarchy)
        {
            tierUpgradeMenu.SetActive(false);
            levelUpgradeMenu.SetActive(true);
            changeMenuButtonText.text = "Combine Components";
        }
        else
        {
            tierUpgradeMenu.SetActive(true);
            levelUpgradeMenu.SetActive(false);
            changeMenuButtonText.text = "Upgrade Component Levels";
        }
    }
}