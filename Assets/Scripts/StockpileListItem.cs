using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Mirror;
using UnityEngine.Events;

public class StockpileListItem : MonoBehaviour {

    public Text nameText;
    public Text locationText;
    public Text passcodeText;
    public Button viewButton;

    private UIManager uIManager;

    public StockpileModel stockpileXml;

    private ContentPresenter presenter;

    [SerializeField]
    private StockpileModel.simpleData stockpileData;
    public StockpileModel.simpleData StockpileData { get {  return stockpileData; }  }


    private void Awake()
    {
        presenter = transform.parent.GetComponent<ContentPresenter>();
        presenter.OnContentDrawn.AddListener(DrawUI);
    }

    private void Start() {
        
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }

    private void DrawUI(StockpileModel.simpleData data)
    {
        nameText.text = data.name;
        locationText.text = data.location;
        passcodeText.text = data.passcode;

        stockpileData = data;

        presenter.OnContentDrawn.RemoveListener(DrawUI);
    }

    public void SetItemUI(StockpileModel stockpile){

        nameText.text = stockpile.name;
        locationText.text = stockpile.location;
        passcodeText.text = stockpile.passcode;

        stockpileXml = stockpile;

    }

    public static event EventHandler<OnViewEventArgs> OnStockViewChange;
    public static event EventHandler<string> OnViewChange;
    public class OnViewEventArgs : EventArgs{
        public StockpileModel stockpile;
    }

    public void ViewStockpile(){

        //OnStockViewChange?.Invoke(this, new OnViewEventArgs{
        //    stockpile = stockpileXml
        //    });
        //Debug.Log(stockpileXml.Id);

        OnViewChange?.Invoke(this, stockpileData.Id);

    }
    
}
