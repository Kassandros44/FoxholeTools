using FoxholeTools.Utils;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class StockpileData : MonoBehaviour
{
    [SerializeField]
    private string _Id;

    [SerializeField]
    private List<Crate> crates;

    [SerializeField]
    private string stockpileName;

    [SerializeField]
    private UnityEvent<List<Crate>, string> updateView;

    private void Start()
    {

        StockpileListItem.OnViewChange += (object sender, StockpileModel.idenityData idenityData) =>
        {
            GetStockpileCrates(idenityData, (callback) =>
            {
                _Id = idenityData.Id;
                crates = callback;
                stockpileName = idenityData.name;
                updateView?.Invoke(callback, idenityData.name);
            });
        };
    }

    public void GetStockpileCrates(StockpileModel.idenityData idenityData, Action<List<Crate>> callback)
    {
        string url = $"{Helper.apiHost + Helper.apiPort}/stockpile/crates/{idenityData.Id}";
        WebRequests.Get(url, (error) => { }, (data) => {
            callback?.Invoke(JsonConvert.DeserializeObject<List<Crate>>(data));
        });
    }

}
