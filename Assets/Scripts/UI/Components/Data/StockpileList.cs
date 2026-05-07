using FoxholeTools.Utils;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StockpileList : MonoBehaviour
{

    private string lastSync;
    List<StockpileModel.idenityData> stockpiles = new List<StockpileModel.idenityData>();

    public UnityEvent<List<StockpileModel.idenityData>> stockpilesLoaded;
    public UnityEvent<List<StockpileModel.idenityData>, List<string>> stockpilesUpdated;



    private void Start()
    {
        InitialiseData();
        TickTimerSystem.OnTick_60 += (object sender, TickTimerSystem.OnTickEventArgs e) => { UpdateData(); };
    }

    private class InitialiseRoot
    {
        public List<StockpileModel.idenityData> activeStockpiles {  get; set; }
        public string serverTime;
    }

    private class UpdateRoot
    {
        [JsonProperty ("added")]
        public List<StockpileModel.idenityData> addedStockpiles { get; set; }
        [JsonProperty ("removed")]
        public List<string> removedStockpiles { get; set; }
        public string serverTime;
    }

    void InitialiseData()
    {
        string url = $"{Helper.apiHost+Helper.apiPort}/stockpile/initsync";
        WebRequests.Get(url, (error) => { }, (data) =>
        {
            var root = JsonConvert.DeserializeObject<InitialiseRoot>(data);
            Debug.Log(root.ToJson());
            var list = root.activeStockpiles;
            list.ForEach(data => { Debug.Log(data.Id); });
            lastSync = root.serverTime;
            stockpilesLoaded?.Invoke(list);
        });
    }

    void UpdateData()
    {
        string url = $"{Helper.apiHost+Helper.apiPort}/stockpile/sync?lastSync={lastSync}";
        WebRequests.Get(url, (error) => { }, (data) => 
        {
            Debug.Log(data);
            Debug.Log(lastSync);
            var root = JsonConvert.DeserializeObject<UpdateRoot>(data);
            lastSync = root.serverTime;
            stockpilesUpdated?.Invoke(root.addedStockpiles, root.removedStockpiles);
        });
    }
}
