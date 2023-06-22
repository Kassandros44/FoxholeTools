using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Mirror;

public class StockpileListItem : MonoBehaviour {

    public Text nameText;
    public Text locationText;
    public Text passcodeText;
    public Button viewButton;

    private UIManager uIManager;

    public StockpileModel stockpileXml;

    private void Start() {
        
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }

    public void SetItemUI(StockpileModel stockpile){

        nameText.text = stockpile.name;
        locationText.text = stockpile.location;
        passcodeText.text = stockpile.passcode;

        stockpileXml = stockpile;

    }

    public static event EventHandler<OnViewEventArgs> OnStockViewChange;
    public class OnViewEventArgs : EventArgs{
        public StockpileModel stockpile;
    }

    public void ViewStockpile(){

        OnStockViewChange?.Invoke(this, new OnViewEventArgs{
            stockpile = stockpileXml
            });
        Debug.Log(stockpileXml.Id);

    }
    
}
