using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Crate {

    [XmlAttribute("Name")]
    public string name;

    [XmlAttribute("Amount")]
    public int amount;

    public Crate(){}

}
