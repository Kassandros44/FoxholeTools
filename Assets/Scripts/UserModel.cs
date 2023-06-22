using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

[Serializable]
public class UserModel {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id;
    [XmlAttribute("Username")]
    public string username;
    [XmlAttribute("Passkey")]
    private string passkey;
    public string Passkey { get; set; }
    private enum rank{
        RIP,
        Member,
        KR,
        Officer,
        HighCommand
    }

    public UserModel(){

        Id = "";
        username = "Default";
        passkey = "";

    }

}
