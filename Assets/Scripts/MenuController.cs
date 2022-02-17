using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public RectTransform[] groups;
    private Vector2 offset = 1000 * Vector2.up;
    public RectTransform currentGroup;
    public bool IsMenuOpen
    {
        get
        {
            if (this.currentGroup != this.ClosedMenu)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private set { }
    }
    public RectTransform ClosedMenu;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        ChangeGroup(currentGroup);
    }
    public void ChangeGroup(RectTransform groupToActivate)
    {
        RectTransform newGroup = groupToActivate;

        foreach (RectTransform group in groups)
        {
            if (group.name == newGroup.name)
            {
                //set current group
                group.anchoredPosition = Vector2.zero;
                this.currentGroup = groupToActivate;
            }
            else
            {
                group.anchoredPosition = offset;
            }
        }
    }
    public void CloseMenu()
    {
        ChangeGroup(this.ClosedMenu);
    }
}