using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Serialization;

[XmlRoot("ServerData"), Serializable]
public class DataManager{

    [XmlElement("UserData")]
    public UserData userData = new UserData();

    [XmlElement("StockpileData")]
    public StockpileData stockpileData = new StockpileData();

    [XmlElement("RequestData")]
    public RequestData requestData = new RequestData();

    [XmlElement("MapPinData")]
    public MapPinData mapPinData = new MapPinData();


    public void Save(string path){

        var serializer = new XmlSerializer(typeof(DataManager));
        using (var stream = new FileStream(path, FileMode.Create)){

            serializer.Serialize(stream, this);

        }

    }

    public static DataManager Load(string path){

        if(File.Exists(path)){
            var serializer = new XmlSerializer(typeof(DataManager));
            using (var stream = new FileStream(path, FileMode.Open)){

                return serializer.Deserialize(stream) as DataManager;

            }
        } else {
            return null;
        }
    }

    //Json Code Below.

    public void JSave (DataManager data){

        ObjectToJson objectToJson = new ObjectToJson(data);

        //string jsonString = JsonUtility.ToJson(data);
        string jsonString = objectToJson.SerializeObjectMethod();

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "serverdata.json"), jsonString);

    }

}

[Serializable]
public class UserData{

    [XmlArray("Users"), XmlArrayItem("User")]
    public List<UserXml> Users = new List<UserXml>();

}

[Serializable]
public class StockpileData{

    [XmlArray("Stockpiles"), XmlArrayItem("Stockpile")]
    public List<StockpileXml> Stockpiles = new List<StockpileXml>();

}

[Serializable]
public class RequestData{

    [XmlArray("Requests"), XmlArrayItem("Request")]
    public List<RequestXml> Requests = new List<RequestXml>();

}

[Serializable]
public class MapPinData{

    [XmlArray("MapPins"), XmlArrayItem("MapPin")]
    public List<MapPinXml> MapPins = new List<MapPinXml>();

}

//Json classes
public class ObjectToJson{

    private readonly System.Object _data;

    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings(){

        Formatting = Newtonsoft.Json.Formatting.Indented,
        ContractResolver = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()},

    };

    public ObjectToJson(System.Object data){

        _data = data;

    }

    public string SerializeObjectMethod(){

        return JsonConvert.SerializeObject(_data, _settings);

    }

}
