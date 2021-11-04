using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComputerHandler : MonoBehaviour
{
    public ComputerStats compStats;
    public Computer selectedComputer;
    public UnityEvent OnStatsChanged = new UnityEvent();

    private void Awake()
    {
        this.compStats = GetComponent<ComputerStats>();
        Debug.Log(this.compStats);
    }
    public void Select(Computer computer)
    {
        this.selectedComputer = computer;
        OnStatsChanged?.Invoke();
    }
    public void Upgrade(int upgradeType)
    {
        this.selectedComputer?.Upgrade(upgradeType);
        OnStatsChanged?.Invoke();
    }
}
