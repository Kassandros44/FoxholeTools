using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

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


}
