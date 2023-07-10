using Newtonsoft.Json.Linq;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

[Serializable]
public class UserModel {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id;
    public string username;
    private enum rank{
        RIP,
        Member,
        KR,
        Officer,
        HighCommand
    }

    public UserModel(){}

    public UserModel(JObject jobject){
        if(jobject.ContainsKey("id")){
            Id = (string)jobject["id"];
        }
        if(jobject.ContainsKey("username")){
            username = (string)jobject["username"];
        }

    }

}
