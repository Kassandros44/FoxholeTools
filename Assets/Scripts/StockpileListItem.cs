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

    private StockpileXml stockpileXml;

    private void Start() {
        
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }

    public void SetItemUI(StockpileXml stockpile){

        nameText.text = stockpile.name;
        locationText.text = stockpile.location;
        passcodeText.text = stockpile.passcode;

        stockpileXml = stockpile;

    }

    public void ViewStockpile(){

        NetworkClient.localPlayer.GetComponent<User>().RequestStockpileData(passcodeText.text);

    }
    
}
