using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

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
    public List<UserModel> Users = new List<UserModel>();

}

[Serializable]
public class StockpileData{

    [XmlArray("Stockpiles"), XmlArrayItem("Stockpile")]
    public List<StockpileModel> Stockpiles = new List<StockpileModel>();

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

public static class DatabaseManager{

    private const string connectionURI = "mongodb://127.0.0.1:27017";
    private const string databaseName = "FoxholeTools";
    private const string UserCollection = "Users";
    private const string StockpileCollection = "Stockpiles";

    private static IMongoCollection<T> ConnectToMongo<T>(in string collection){

        var client = new MongoClient(connectionURI);
        var db = client.GetDatabase(databaseName);
        return db.GetCollection<T>(collection);

    }

#region Stockpiles
    public static Task AddStockpile(StockpileModel stockpile){
        var stockpileCollection = ConnectToMongo<StockpileModel>(StockpileCollection);
        return stockpileCollection.InsertOneAsync(stockpile);
    }

    public static Task UpdateStockpile(StockpileModel stockpile){
        var stockpileCollection = ConnectToMongo<StockpileModel>(StockpileCollection);
        var filter = Builders<StockpileModel>.Filter.Eq("Id", stockpile.Id);
        Debug.Log($"Stockpile ID for update: {stockpile.Id}");
        return stockpileCollection.ReplaceOneAsync(filter, stockpile);
    }

    public static Task DeleteStockpile(StockpileModel stockpile){
        var stockpileCollection = ConnectToMongo<StockpileModel>(StockpileCollection);
        var filter = Builders<StockpileModel>.Filter.Eq("Id", stockpile.Id);
        return stockpileCollection.DeleteOneAsync(filter);
    }

    public static List<StockpileModel> GetAllStockpiles(){
        var stockpileCollection = ConnectToMongo<StockpileModel>(StockpileCollection);
        var results = stockpileCollection.Find(_ => true);
        return results.ToList();
    }

    public static StockpileModel GetStockpile(StockpileModel stockpile){
        var stockpileCollection = ConnectToMongo<StockpileModel>(StockpileCollection);
        var filter = Builders<StockpileModel>.Filter.Eq("Id", stockpile.Id);
        var result = stockpileCollection.Find(filter).FirstOrDefault();
        return result;
    }
#endregion
#region Users
    public static Task AddUser(UserModel user){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        return userCollection.InsertOneAsync(user);
    }

    public static Task UpdateUser(UserModel user){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
        return userCollection.ReplaceOneAsync(filter, user);
    }

    public static Task DeleteUser(UserModel user){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
        return userCollection.DeleteOneAsync(filter);
    }

    public static List<UserModel> GetAllUsers(){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var results = userCollection.Find(_ => true);
        return results.ToList();
    }

    public static UserModel GetUser(string username, string passkey){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var builder = Builders<UserModel>.Filter;
        var filter = builder.Eq(u => u.username, username) & builder.Eq(u => u.Passkey, passkey);
        var results = userCollection.Find<UserModel>(filter).FirstOrDefault();
        return results;
    }

    public static UserModel GetUser(string username){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var builder = Builders<UserModel>.Filter;
        var filter = builder.Eq(u => u.username, username);
        var results = userCollection.Find<UserModel>(filter).FirstOrDefault();
        return results;
    }

    public static bool UserExists(string username, string passkey){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var builder = Builders<UserModel>.Filter;
        var filter = builder.Eq(u => u.username, username) & builder.Eq(u => u.Passkey, passkey);
        var results = userCollection.Find<UserModel>(filter).FirstOrDefault();
        if(results != null){
            return true;
        }else{
            return false;
        }
    }

    public static bool UserExists(string username){
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var builder = Builders<UserModel>.Filter;
        var filter = builder.Eq(u => u.username, username);
        var results = userCollection.Find<UserModel>(filter).FirstOrDefault();
        if(results != null){
            return true;
        }else{
            return false;
        }
        
    }
#endregion
}