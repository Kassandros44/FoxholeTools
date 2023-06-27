using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

public class Log {

    [XmlAttribute("Username")]
    public string username;

    [XmlAttribute("Timestamp")]
    public string timestamp;

    [XmlAttribute("Action")]
    public string action;

    [XmlAttribute("Item")]
    public string item;

    [XmlAttribute("Amount")]
    public string amount;

    public Log(){}

    public Log(string u, string t, string a, string i, string am){

        username = u;
        timestamp = t;
        action = a;
        item = i;
        amount = am;

    }

    public Log(JObject jobject){
        if(jobject.ContainsKey("username")){
            username = (string)jobject["username"];
        }
        if(jobject.ContainsKey("timestamp")){
            timestamp = (string)jobject["timestamp"];
        }
        if(jobject.ContainsKey("action")){
            action = (string)jobject["action"];
        }
        if(jobject.ContainsKey("item")){
            item = (string)jobject["item"];
        }
        if(jobject.ContainsKey("amount")){
            amount = (string)jobject["amount"];
        }
    }


}
