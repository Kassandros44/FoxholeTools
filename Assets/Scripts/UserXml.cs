using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;

[Serializable]
public class UserXml {

    [XmlAttribute("Username")]
    public string username;
    [XmlAttribute("Passkey")]
    private string passkey;

    public UserXml(){

        username = "Default";
        passkey = "";

    }

}
