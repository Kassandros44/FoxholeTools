using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public static class LocalUser
{

    private static GameObject localUser;
    private static string username;

    public static void SetLocalUser(GameObject obj){
        localUser = obj;
    }

    public static GameObject GetLocalUser(){
        return localUser;
    }

    public static void SetLocalUsername(string text){
        username = text;
    }

    public static string GetUsername(){
        return username;
    }

    public static User GetUserComponent(){
        return GetLocalUser().GetComponent<User>();
    }

}
