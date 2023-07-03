using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StkText : MonoBehaviour
{

    void Start(){
        StockpileListItem.OnStockViewChange += (object sender, StockpileListItem.OnViewEventArgs e) => {
            this.GetComponent<TMP_Text>().text = e.stockpile.name;
        };
    }

    void Update(){
        
    }

}
