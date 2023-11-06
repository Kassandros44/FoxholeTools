using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxholeTools.Utils;

public class StockDltWindow : MonoBehaviour
{
    
    public StockpileModel stockpile;

    public void ConfirmYes(){
        string id = stockpile.Id;
        Debug.Log($"Confirm ID: {id}");
        WebRequests.Delete($"{Helper.apiHost+Helper.apiPort}/stockpiles/delete/{id}", onError=>{}, onSuccess=>{});
        GameObject.Destroy(this.gameObject);
    }
    public void ConfirmNo(){
        GameObject.Destroy(this.gameObject);
    }

}
