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

        string url = $"{Helper.apiHost + Helper.apiPort}/stockpiles/new";

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
            crate.quota = 0;
            stockpile.crates.Add(crate);

        }

        Debug.Log(stockpile.name + " was Saved");
        
        //DatabaseManager.AddStockpile(stockpile);
        WebRequests.Put(url, stockpile, (i) => {}, (i) => {});

    }

    public void AddCratesToStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = $"{Helper.apiHost+Helper.apiPort}/stockpiles/update";

        foreach (var item in stockpile.crates) {

            Debug.Log(stockpile.crates.IndexOf(item) + "--" + index);

            if(stockpile.crates.IndexOf(item) == index){

                item.amount += Math.Abs(num);
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Added", item.name, num.ToString()));
                if(item.amount > 9999){
                    item.amount = 9999;
                    stockpile.logs.Add(new Log("System", DateTime.UtcNow.ToString(), "Clamped", item.name, "9999"));
                }
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "added" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

    public void RemoveCratesFromStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = $"{Helper.apiHost+Helper.apiPort}/stockpiles/update";

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount -= Math.Abs(num);
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Removed", item.name, num.ToString()));
                if(item.amount < 0){
                    item.amount = 0;
                    stockpile.logs.Add(new Log("System", DateTime.UtcNow.ToString(), "Clamped", item.name, "0"));
                }
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "removed" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

    public void SetCratesInStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = $"{Helper.apiHost+Helper.apiPort}/stockpiles/update";

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount = num;
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Set", item.name, num.ToString()));
                if(item.amount < 0){
                    item.amount = 0;
                    stockpile.logs.Add(new Log("System", DateTime.UtcNow.ToString(), "Clamped", item.name, "0"));
                } else if(item.amount > 9999){
                    item.amount = 9999;
                    stockpile.logs.Add(new Log("System", DateTime.UtcNow.ToString(), "Clamped", item.name, "9999"));
                }
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "set" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

    public void SetQuotaInStockpile(StockpileModel stockpile, int index, int num, string username){

        string url = $"{Helper.apiHost+Helper.apiPort}/stockpiles/update";

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.quota = num;
                stockpile.logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Set Quota", item.name, num.ToString()));
                if(item.quota < 0){
                    item.quota = 0;
                    stockpile.logs.Add(new Log("System", DateTime.UtcNow.ToString(), "Clamped", item.name, "0"));
                } else if(item.quota > 9999){
                    item.quota = 9999;
                    stockpile.logs.Add(new Log("System", DateTime.UtcNow.ToString(), "Clamped", item.name, "9999"));
                }
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "set quota" + ": " + num + " " + item.name + " crates");

            }

        }

        WebRequests.Put($"{url}/{stockpile.Id}", stockpile, (i) => {}, (i) => {});

    }

}
