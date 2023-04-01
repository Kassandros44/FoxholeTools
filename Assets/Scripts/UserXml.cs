using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class UserXml {

    [XmlAttribute("Username")]
    public string username;

    public UserXml(){

        username = "Default";

    }

}
