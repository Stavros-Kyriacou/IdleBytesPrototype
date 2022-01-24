using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CancelCraftMenu : MonoBehaviour
{
    public TextMeshProUGUI GraceTimeText;
    public TextMeshProUGUI DescriptionText;
    // public CraftingSlot currentTimer;
    public float offset;
    public Vector2 OffScreen
    {
        get
        {
            return Vector2.up * this.offset;
        }
        private set { }
    }

    private bool _isVisible = false;
    private RectTransform _rectTransform;
    private CraftingSlot _craftingSlot;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Cancel(CraftingSlot craftingSlot)
    {
        if (!craftingSlot.CraftTimer.IsAvailable)
        {
            //Craft is not available => craft is happening
            ToggleMenu();
            if (_isVisible)
            {
                _craftingSlot = craftingSlot;
                _craftingSlot?.GraceTimer.OnTimerCountdown.AddListener(UpdateTimeRemaining);
                _craftingSlot?.GraceTimer.OnTimerCountdown.AddListener(UpdateDescription);
                _craftingSlot?.GraceTimer.OnTimerComplete.AddListener(UpdateDescription);
                UpdateTimeRemaining();
                UpdateDescription();
            }
        }
    }
    public void ToggleMenu()
    {
        if (_rectTransform.anchoredPosition == Vector2.zero)
        {
            _rectTransform.anchoredPosition = OffScreen;
            _isVisible = false;
        }
        else
        {
            _rectTransform.anchoredPosition = Vector2.zero;
            _isVisible = true;
        }
    }
    public void UpdateTimeRemaining()
    {
        if (_craftingSlot != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(_craftingSlot.GraceTimer.TimeRemaining);
            GraceTimeText.text = "Grace Time: " + time.ToString(@"hh\:mm\:ss");
        }
    }
    public void UpdateDescription()
    {
        if (this._craftingSlot.GraceTimer.TimeRemaining > 0)
        {
            //grace timer is still active
            this.DescriptionText.text = $"If you cancel now, you will get your components back and x scrap back ";
        }
        else
        {
            //grace timer finished
            this.DescriptionText.text = $"Grace Time over. If you cancel now, you will not get your components back but will receive x scrap";
        }
    }
}