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
    private UnityEvent<ViewArgs> updateView;

    public class ViewArgs { 
        public List<Crate> args1;
        public string args2;

        public ViewArgs() { }

        public ViewArgs(List<Crate> crates, string name)
        {
            args1 = crates;
            args2 = name;
        }
    }

    private void Start()
    {

        StockpileListItem.OnViewChange += (object sender, string Id) => { 
            _Id = Id;
            //updateView?.Invoke(new ViewArgs(GetStockpileCrates(Id), GetStockpileName(Id)));
            StartCoroutine(ViewChangeEnumerator(Id));
        };

        //GetStockpileCrates("64a7ca27767c2783748ab63f");
    }

    private IEnumerator ViewChangeEnumerator(string id)
    {
        ViewArgs args = new ViewArgs();
        yield return StartCoroutine(GetStockpileCrates(id));
        args.args1 = crates;
        yield return StartCoroutine(GetStockpileName(id));
        args.args2 = stockpileName;
        Debug.Log(args.ToJson());
        updateView?.Invoke(args);
    }

    public IEnumerator GetStockpileCrates(string Id)
    {
        string url = $"{Helper.apiHost + Helper.apiPort}/stockpile/crates/{Id}";
        WebRequests.Get(url, (error) => { }, (data) => { 
            crates = JsonConvert.DeserializeObject<List<Crate>>(data);
        });
        yield return crates;
        yield return new WaitUntil(() => crates != null);
    }

    //needs further thought
    public IEnumerator GetStockpileName(string Id)
    {
        string url = $"{Helper.apiHost + Helper.apiPort}/stockpile/name/{Id}";
        WebRequests.Get(url, (error) => { }, (data) => {
            stockpileName = JsonConvert.DeserializeObject<string>(data);
        });
        yield return stockpileName;
        yield return new WaitUntil(() => stockpileName != "");
    }

}
