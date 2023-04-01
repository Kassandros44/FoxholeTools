using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Item {

    [XmlAttribute("Name")]
    public string name;

    [XmlAttribute("Image")]
    public string image;

    public Item(){

    }

}
