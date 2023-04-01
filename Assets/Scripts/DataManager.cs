using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("ServerData")]
public class DataManager{

    [XmlElement("UserData")]
    public UserData userData;

    [XmlElement("StockpileData")]
    public StockpileData stockpileData;

    [XmlElement("RequestData")]
    public RequestData requestData;

    [XmlElement("MapPinData")]
    public MapPinData mapPinData;


    public void Save(string path){

        var serializer = new XmlSerializer(typeof(DataManager));
        using (var stream = new FileStream(path, FileMode.Create)){

            serializer.Serialize(stream, this);

        }

    }

    public static DataManager Load(string path){

        var serializer = new XmlSerializer(typeof(DataManager));
        using (var stream = new FileStream(path, FileMode.Open)){

            return serializer.Deserialize(stream) as DataManager;

        }

    }

}

public class UserData{

    [XmlArray("Users"), XmlArrayItem("User")]
    public List<UserXml> Users = new List<UserXml>();

}

public class StockpileData{

    [XmlArray("Stockpiles"), XmlArrayItem("Stockpile")]
    public List<StockpileXml> Stockpiles = new List<StockpileXml>();

}

public class RequestData{

    [XmlArray("Requests"), XmlArrayItem("Request")]
    public List<RequestXml> Requests = new List<RequestXml>();

}

public class MapPinData{

    [XmlArray("MapPins"), XmlArrayItem("MapPin")]
    public List<MapPinXml> MapPins = new List<MapPinXml>();

}
