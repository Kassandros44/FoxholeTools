using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public static class LocalUser
{

    private static GameObject localUser;

    public static void SetLocalUser(){
        localUser = NetworkClient.localPlayer.gameObject;
    }

    public static GameObject GetLocalUser(){
        return localUser;
    }

}
