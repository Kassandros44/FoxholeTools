using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Mirror;
using System;

public class CrateInteractWindow : NetworkBehaviour {

    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private InputField inputField;

    private GameObject localUser;

    private StockpileXml stockpile;

    public int index;

    private void Start() {
        
        localUser = NetworkClient.localPlayer.gameObject;
        stockpile = localUser.GetComponent<User>().currentlyViewedStockpile;

    }

    public void SubmitButton(){

        switch (dropdown.value)
        {
            
            case 0:
                localUser.GetComponent<User>().AddCratesToStockpile(stockpile, index, int.Parse(inputField.text), localUser.GetComponent<User>().uIManager.username);
                CloseWindow();
                break;
            case 1:
                localUser.GetComponent<User>().RemoveCratesFromStockpile(stockpile, index, int.Parse(inputField.text), localUser.GetComponent<User>().uIManager.username);
                CloseWindow();
                break;
            case 2:
                localUser.GetComponent<User>().SetCratesInStockpile(stockpile, index, int.Parse(inputField.text), localUser.GetComponent<User>().uIManager.username);
                CloseWindow();
                break;
        }

    }

    private void CloseWindow(){

        Destroy(this.gameObject);

    }

}
