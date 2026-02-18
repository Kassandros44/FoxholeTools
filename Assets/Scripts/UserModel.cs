using Newtonsoft.Json.Linq;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


[Serializable]
public class UserModel {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id;
    public string discordId;
    public string avatar;
    public string username;
    public string globalName;
#nullable enable
    public string? guildName;
#nullable disable
    public string rank;

    public UserModel(){}

    public UserModel(JObject jobject)
    {
        if(jobject.ContainsKey("id"))
        {
            Id = (string)jobject["id"];
        }
        if (jobject.ContainsKey("discordId"))
        {
            discordId = (string)jobject["discordId"];
        }
        if (jobject.ContainsKey("avatar"))
        {
            avatar = (string)jobject["avatar"];
        }
        if (jobject.ContainsKey("username"))
        {
            username = (string)jobject["username"];
        }
        if (jobject.ContainsKey("globalName"))
        {
            globalName = (string)jobject["globalName"];
        }
        if(jobject.ContainsKey("rank"))
        {
            rank = (string)jobject["rank"];
        }

    }

    public void CopyFrom(UserModel other)
    {
        Id = other.Id;
        discordId = other.discordId;
        avatar = other.avatar;
        username = other.username;
        globalName = other.globalName;
        guildName = other.guildName;
        rank = other.rank;
    }

}
