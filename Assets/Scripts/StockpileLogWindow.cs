using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class StockpileLogWindow : NetworkBehaviour {

    //NOTE: REFACTOR THIS

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject logElement;

    private StockpileModel stockpile;

    private void Start() {
        
        if(LocalUser.GetLocalUser().GetComponent<User>().currentlyViewedStockpile != null){

            stockpile = LocalUser.GetLocalUser().GetComponent<User>().currentlyViewedStockpile;
            UpdateContent();

        }

    }

    public void UpdateContent(){

        var currentLog = stockpile;

        if(currentLog == null){

            GameObject newLogElement = Instantiate<GameObject>(logElement, content.transform);
            newLogElement.GetComponentInChildren<Text>().text = "No Currently viewed Log, Please Refresh by clicking the View button for a Stockpile";

        } else {

            foreach (var item in currentLog.logs) {

                GameObject newLogElement = Instantiate<GameObject>(logElement, content.transform);
                newLogElement.GetComponentInChildren<Text>().text = item.username + ": " + item.timestamp + " " + item.action + ": " + item.amount + " " + item.item + " crates";

            }
        
        }

    }

}
