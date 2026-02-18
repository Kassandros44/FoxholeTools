using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public static class LocalUser
{

    private static GameObject localUser;
    private static string username;
    private static UserModel localUserData;

    public static void SetLocalUserData(UserModel user) {  localUserData = user; }
    public static UserModel GetLocalUserData() {  return localUserData; }

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
