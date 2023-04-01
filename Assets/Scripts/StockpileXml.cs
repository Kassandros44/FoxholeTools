using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


public class StockpileXml {

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

    public StockpileXml(){
        name = "Default";
        location = "Unmarked";
        passcode = "000000";
    }

}
