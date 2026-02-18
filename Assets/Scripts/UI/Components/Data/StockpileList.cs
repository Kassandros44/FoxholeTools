using FoxholeTools.Utils;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StockpileList : MonoBehaviour
{

    List<StockpileModel.simpleData> stockpiles = new List<StockpileModel.simpleData>();

    public UnityEvent<List<StockpileModel.simpleData>> stockpilesLoaded;



    private void Start()
    {
        TickTimerSystem.OnTick_5 += (object sender, TickTimerSystem.OnTickEventArgs e) => { UpdateData(); };
    }

    void UpdateData()
    {
        string url = $"{Helper.apiHost+Helper.apiPort}/stockpiles/simpledata";
        WebRequests.Get(url, (error) => { }, (data) => {
            JsonConvert.DeserializeObject<List<StockpileModel.simpleData>>(data).ForEach(stockpile => 
            {
                if (stockpiles.Contains(stockpile))
                {
                    return;
                }
                else
                {
                    stockpiles.Add(stockpile);
                }
            });
            stockpilesLoaded?.Invoke(stockpiles);
        });
    }
}
