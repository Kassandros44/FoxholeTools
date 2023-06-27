using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using FoxholeTools.Utils;

[Serializable]
public class StockpileModel {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id;

    [XmlAttribute("Name")]
    public string name;

    [XmlAttribute("Location")]
    public string location;

    [XmlAttribute("Passcode")]
    public string passcode;

    [XmlArray("Crates"), XmlArrayItem("Crate")]
    public List<Crate> crates = new List<Crate>();

    [XmlArray("Logs"), XmlArrayItem("Log")]
    public List<Log> logs = new List<Log>();

    public StockpileModel(){}

    public StockpileModel(JObject jobject){
        if(jobject.ContainsKey("id")){
            Id = (string)jobject["id"];
        }
        if(jobject.ContainsKey("name")){
            name = (string)jobject["name"];
        }
        if(jobject.ContainsKey("location")){
            location = (string)jobject["location"];
        }
        if(jobject.ContainsKey("passcode")){
            passcode = (string)jobject["passcode"];
        }
        if(jobject.ContainsKey("crates")){
            JArray jArray = (JArray)jobject["crates"];
            foreach(var i in jArray){
                JObject item = JObject.Parse(i.ToString());
                Crate crate = new Crate(item);
                crates.Add(crate);
            }
        }
        if(jobject.ContainsKey("logs")){
            JArray jArray = (JArray)jobject["logs"];
            foreach(var i in jArray){
                JObject item = JObject.Parse(i.ToString());
                Log log = new Log(item);
                logs.Add(log);
            }
        }
    }

    public static void CreateNewStockpile(string name, string loc, string pass){

        string url = "http://localhost:5191/stockpiles";

        var stockpile = new StockpileModel{
            name=name,
            location=loc,
            passcode=pass
        };

        var itemCollection = ItemContainer.Load();

        foreach (var cItem in itemCollection.Items) {

            Crate crate = new Crate();
            crate.name = cItem.name;
            crate.amount = 0;
            stockpile.crates.Add(crate);

        }

        Debug.Log(stockpile.name + " was Saved");
        
        //DatabaseManager.AddStockpile(stockpile);
        WebRequests.Put(url, stockpile, (i) => {}, (i) => {});

    }

    public void AddCratesToStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = "http://localhost:5191/stockpiles";

        foreach (var item in stockpile.crates) {

            Debug.Log(stockpile.crates.IndexOf(item) + "--" + index);

            if(stockpile.crates.IndexOf(item) == index){

                item.amount += num;
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Added", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "added" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

    public void RemoveCratesFromStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = "http://localhost:5191/stockpiles";

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount -= num;
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Removed", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "removed" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

    public void SetCratesInStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = "http://localhost:5191/stockpiles";

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount = num;
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Set", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "set" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

}
