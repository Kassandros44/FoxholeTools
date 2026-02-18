using FoxholeTools.Utils;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class StockpileData : MonoBehaviour
{

    [SerializeField]
    private List<Crate> crates;

    [SerializeField]
    private string stockpileName;

    [SerializeField]
    private UnityEvent<List<Crate>> crateEvents;

    private void Start()
    {

        StockpileListItem.OnViewChange += (object sender, string Id) => { 
            GetStockpileCrates(Id);
            
        };

        //GetStockpileCrates("64a7ca27767c2783748ab63f");
    }

    public void GetStockpileCrates(string Id)
    {
        string url = $"{Helper.apiHost + Helper.apiPort}/stockpile/crates/{Id}";
        WebRequests.Get(url, (error) => { }, (data) => { 
            crates = JsonConvert.DeserializeObject<List<Crate>>(data);
            crateEvents?.Invoke(crates);
        });
    }

    //needs further thought
    public void GetStockpileName(string Id)
    {
        string url = $"{Helper.apiHost + Helper.apiPort}/stockpile/name/{Id}";
        WebRequests.Get(url, (error) => { }, (data) => {
            stockpileName = JsonConvert.DeserializeObject<string>(data);
            crateEvents?.Invoke(crates);
        });
    }

}
