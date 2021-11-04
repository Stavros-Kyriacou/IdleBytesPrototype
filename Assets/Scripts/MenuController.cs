using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum MenuStates
    {
        Closed,
        Main,
        Research,
        Perks,
        Settings,
        DailyChallenges,
        ComputerStats,
        ComponentSelection
    }
    private GameObject currentState;
    public GameObject closedMenu;
    public GameObject mainMenu;
    public GameObject researchMenu;
    public GameObject perkMenu;
    public GameObject settingsMenu;
    public GameObject dailyChallengesMenu;
    public GameObject computerStatsMenu;
    public GameObject componentSelectionMenu;

    private void Awake()
    {
        researchMenu.SetActive(false);
        perkMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        computerStatsMenu.SetActive(false);
        componentSelectionMenu.SetActive(false);
    }
    public void OnMainMenu()
    {
        ChangeMenu(MenuStates.Main);
    }
    public void CloseMenu()
    {
        ChangeMenu(MenuStates.Closed);
    }
    public void OnResearch()
    {
        ChangeMenu(MenuStates.Research);
    }
    public void OnPerks()
    {
        ChangeMenu(MenuStates.Perks);
    }
    public void OnSettings()
    {
        ChangeMenu(MenuStates.Settings);
    }
    public void OnDailyChallenges()
    {
        ChangeMenu(MenuStates.DailyChallenges);
    }
    public void OnComputerStats()
    {
        ChangeMenu(MenuStates.ComputerStats);
    }
    public void OnComponentSelection()
    {
        ChangeMenu(MenuStates.ComponentSelection);
    }
    public void ChangeMenu(MenuStates menu)
    {
        GameObject newState = null;

        switch (menu)
        {
            case MenuStates.Closed:
                newState = this.closedMenu;
                break;
            case MenuStates.Main:
                newState = this.mainMenu;
                break;
            case MenuStates.Research:
                newState = this.researchMenu;
                break;
            case MenuStates.Perks:
                newState = this.perkMenu;
                break;
            case MenuStates.Settings:
                newState = this.settingsMenu;
                break;
            case MenuStates.DailyChallenges:
                newState = this.dailyChallengesMenu;
                break;
            case MenuStates.ComputerStats:
                newState = this.computerStatsMenu;
                break;
            case MenuStates.ComponentSelection:
                newState = this.componentSelectionMenu;
                break;
            default:
                break;
        }
        currentState?.SetActive(false);
        currentState = newState;
        currentState.SetActive(true);
    }
    // public GameObject[] groups;

    // public GameObject currentGroup;

    // void Awake()
    // {
    //     ChangeGroup(currentGroup);
    // }

    // public void ChangeGroup(GameObject groupToActivate)
    // {

    //     GameObject newGroup = groupToActivate;

    //     foreach (GameObject group in groups)
    //     {
    //         if (group.name == newGroup.name)
    //         {
    //             group.SetActive(true);
    //         }
    //         else
    //         {
    //             group.SetActive(false);
    //         }
    //     }
    // }
}
