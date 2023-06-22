using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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
    public List<Log> Logs = new List<Log>();

    public StockpileModel(){
        Id = "";
        name = "Default";
        location = "Unmarked";
        passcode = "000000";
    }

    public static void CreateNewStockpile(string name, string loc, string pass){

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
        
        DatabaseManager.AddStockpile(stockpile);

    }

    public void AddCratesToStockpile(StockpileModel stockpile, int index, int num, string username){

        foreach (var item in stockpile.crates) {

            Debug.Log(stockpile.crates.IndexOf(item) + "--" + index);

            if(stockpile.crates.IndexOf(item) == index){

                item.amount += num;
                stockpile.Logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Added", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "added" + ": " + num + " " + item.name + " crates");

            }

        }

        DatabaseManager.UpdateStockpile(stockpile);

    }

    public void RemoveCratesFromStockpile(StockpileModel stockpile, int index, int num, string username){

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount -= num;
                stockpile.Logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Removed", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "removed" + ": " + num + " " + item.name + " crates");

            }

        }

        DatabaseManager.UpdateStockpile(stockpile);

    }

    public void SetCratesInStockpile(StockpileModel stockpile, int index, int num, string username){

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount = num;
                stockpile.Logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Set", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "set" + ": " + num + " " + item.name + " crates");

            }

        }

        DatabaseManager.UpdateStockpile(stockpile);

    }

}
