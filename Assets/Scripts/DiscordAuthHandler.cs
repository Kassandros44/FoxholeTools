using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VoltstroStudios.UnityWebBrowser.Core;
using FoxholeTools.Utils;
using MongoDB.Bson;
using UnityEngine.EventSystems;

public class DiscordAuthHandler : MonoBehaviour
{

    private string discordClientId = Helper.discordClientID;
    private string redirectUri = Helper.discordRedrectURI;
    private string discordOAuthUrl = "https://discord.com/oauth2/authorize";

    [SerializeField]
    private Canvas webCanvas;
    [SerializeField]
    private BaseUwbClientManager clientManager;
    [SerializeField]
    private TMP_InputField urlField;

    private WebBrowserClient webBrowserClient;

    public static event EventHandler<AuthEventArgs> OnAuthComplete;
    public class AuthEventArgs : EventArgs
    {
        public UserModel userModel;
    }

    private void Start()
    {
        webBrowserClient = clientManager.browserClient;
        webCanvas.sortingOrder = 0;
        webCanvas.gameObject.SetActive(false);
    }

    public void StartOAuthFlow()
    {
        string authUrl = $"{discordOAuthUrl}?client_id={discordClientId}&redirect_uri={redirectUri}&response_type=code&scope=identify%20guilds%20guilds.members.read";
        webCanvas.gameObject.SetActive(true);
        webCanvas.sortingOrder = 1;
        webBrowserClient.LoadUrl(authUrl);

        StartCoroutine(HandleCallback());
    }

    private void RequestUserData(string code)
    {
        UserModel returnedUserModel;
        string url = $"{Helper.apiHost+Helper.apiPort}/discord-login/{code}";
        WebRequests.Get(url, (data) => { }, (data) =>
        {
            returnedUserModel = Helper.GetObjectFromData<UserModel>(data);
            Debug.Log($"{returnedUserModel.discordId},{returnedUserModel.username},{returnedUserModel.rank}");
            OnAuthComplete?.Invoke(this, new AuthEventArgs
            {
                userModel = returnedUserModel
            });
        });
        
    }

    private IEnumerator HandleCallback()
    {
        while (!urlField.text.StartsWith(Helper.discordRedrectURI))
        {
            yield return null;
        }

        string[] urlParts = urlField.text.Split('=');
        if(urlParts.Length == 2 )
        {
            string userCode = urlParts[1];
            Debug.Log(userCode);
            RequestUserData(userCode);
            webCanvas.gameObject.SetActive(false);
        } else
        {
            Debug.Log("Failed to retrieve user access code");
            webCanvas.gameObject.SetActive(false);        
        }
    }

}
