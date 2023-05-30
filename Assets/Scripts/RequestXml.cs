using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;

[Serializable]
public class RequestXml
{

    [XmlAttribute("MapPinCoords")]
    public float[] mapPinCoords;


    [XmlAttribute("Location")]
    public string location;

    [XmlAttribute("User")]
    public string username;

    [XmlAttribute("Timestamp")]
    public string timeStamp;

    [XmlArray("Items"), XmlArrayItem("Item")]
    public List<ItemListData> itemList = new List<ItemListData>();

}
