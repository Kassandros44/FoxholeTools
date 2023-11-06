using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordSignInBtn : MonoBehaviour
{

    public void Clicked()
    {
        Application.OpenURL("https://discordapp.com/api/oauth2/authorize?response_type=code&client_id=" + Helper.discordClientID + "&scope=identify%20guilds.join%20guilds&redirect_uri=" + Helper.discordRedrectURI + "");
    }

}
