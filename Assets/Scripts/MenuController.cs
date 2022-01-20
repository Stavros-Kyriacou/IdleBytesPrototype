using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public RectTransform[] groups;
    private Vector2 offset = 1000 * Vector2.up;
    public RectTransform currentGroup;
    void Awake()
    {
        ChangeGroup(currentGroup);
    }
    public void ChangeGroup(RectTransform groupToActivate)
    {
        RectTransform newGroup = groupToActivate;

        foreach (RectTransform group in groups)
        {
            if (group.name == newGroup.name)
            {
                group.anchoredPosition = Vector2.zero;
            }
            else
            {
                group.anchoredPosition = offset;
            }
        }
    }
}