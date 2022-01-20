using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] groups;
    [SerializeField]
    private Image[] buttons;
    [SerializeField]
    private float offset;
    [SerializeField]
    private int defaultGroupIndex;
    private Vector2 offScreen
    {
        get
        {
            return this.offset > 0 ? this.offset * Vector2.up : Vector2.up;
        }
    }
    void Awake()
    {
        ChangeGroup(groups[defaultGroupIndex]);
    }
    public void ChangeGroup(RectTransform groupToActivate)
    {
        RectTransform newGroup = groupToActivate;
        for (int i = 0; i < groups.Length; i++)
        {
            if (groups[i].name == newGroup.name)
            {
                groups[i].anchoredPosition = Vector2.zero;
                buttons[i].color = Color.green;
            }
            else
            {
                groups[i].anchoredPosition = offScreen;
                buttons[i].color = Color.white;
            }
        }
    }
}