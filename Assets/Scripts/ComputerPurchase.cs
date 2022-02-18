using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComputerPurchase : MonoBehaviour
{
    //click on a computer
    //if it is not purchased
    //show popup menu

    //popup menu
    //click purchase
    //change the plus model to the computer model
    //show the next purchasable computer
    public ComputerHandler ComputerHandler;
    public ComputerStats ComputerStats;
    public RectTransform ComputerStatsMenu;
    private Computer selectedComputer;
    private int ComputersOwned;

    [Header("Purchase Computer Popup Menu")]
    public RectTransform PurchaseComputerPopup;
    public TextMeshProUGUI CostText;
    public double Cost;
    public float CostIncremenent = 1.5f;

    private void Start()
    {
        this.ComputersOwned = 1;
        //go through list of computers

        //first computer already puyrchased
        GameController.Instance.computerList[0].IsPurchased = true;
        GameController.Instance.computerList[0].ComputerModel.SetActive(true);
        GameController.Instance.computerList[0].PurchaseModel.SetActive(false);

        //second computer enabled, but show the purchase model
        GameController.Instance.computerList[1].ComputerModel.SetActive(false);
        GameController.Instance.computerList[1].PurchaseModel.SetActive(true);

        //disable the rest
        for (int i = 0; i < GameController.Instance.computerList.Count; i++)
        {
            if (i > 1)
            {
                GameController.Instance.computerList[i].IsPurchased = false;
                GameController.Instance.computerList[i].gameObject.SetActive(false);
            }
        }
    }

    public void ClickComputer(Computer selectedComputer)
    {
        //if it has been purchased
        if (selectedComputer.IsPurchased)
        {
            //show the computer stats screen
            MenuController.Instance.ChangeGroup(this.ComputerStatsMenu);
            this.ComputerHandler.Select(selectedComputer);
        }

        if (selectedComputer.PurchaseModel.activeInHierarchy == true)
        {
            MenuController.Instance.ChangeGroup(this.PurchaseComputerPopup);
            UpdateUI();
            this.selectedComputer = selectedComputer;
        }
        //else if it has the purchase model
        //show the popup menu
    }
    public void ConfirmPurchase()
    {
        //check if enough bandwidth
        if (this.ComputersOwned < Wifi.Instance.SupportedComputers)
        {
            if (GameController.Instance.RemoveDollars(this.Cost))
            {
                this.Cost *= this.CostIncremenent;
                this.ComputersOwned++;
                //change the model of the selected computer
                this.selectedComputer.IsPurchased = true;
                this.selectedComputer.ComputerModel.SetActive(true);
                this.selectedComputer.PurchaseModel.SetActive(false);

                foreach (var computer in GameController.Instance.computerList)
                {
                    if (!computer.IsPurchased)
                    {
                        computer.gameObject.SetActive(true);
                        computer.ComputerModel.SetActive(false);
                        computer.PurchaseModel.SetActive(true);
                        break;
                    }
                }
                MenuController.Instance.CloseMenu();
                GameController.Instance.CalculateDollarsPerSec();
            }
            else
            {
                Debug.Log("Not enough money to purchase new computer");
            }
        }
        else
        {
            Debug.Log("Not enough bandwidth, upgrade your wifi!!");
        }
    }
    public void UpdateUI()
    {
        this.CostText.text = $"Cost: ${this.Cost.ToString("F0")}";
    }

}
