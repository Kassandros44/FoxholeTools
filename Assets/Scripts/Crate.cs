using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

public class Crate {

    [XmlAttribute("Name")]
    public string name;

    [XmlAttribute("Amount")]
    public int amount;

    public Crate(){}

    public Crate(JObject jobject){
        if(jobject.ContainsKey("name")){
            name = (string)jobject["name"];
        }
        if(jobject.ContainsKey("amount")){
            amount = (int)jobject["amount"];
        }
    }

}
