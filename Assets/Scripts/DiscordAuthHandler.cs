using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FoxholeTools.Utils;
using MongoDB.Bson;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Video;
using UnityEngine.Events;

public class DiscordAuthHandler : MonoBehaviour
{
    public UnityEvent<UserModel> OnAuth;
    public static event EventHandler<AuthEventArgs> OnAuthComplete;
    public class AuthEventArgs : EventArgs
    {
        public UserModel userModel;
    }

    private class AuthStartDto
    {
        public string sessionId;
        public string url;
    }

    private class AuthSatausDto
    {
        public bool completed;
        public UserModel user;
    }

    private string _sessionId;

    private void Start()
    {
        
    }

    public void LoginBtnDown()
    {
        StartCoroutine(StartOAuthFlow());
    }

    public IEnumerator StartOAuthFlow()
    {
        var req = UnityWebRequest.Get(Helper.discordRedrectURI);

        yield return req.SendWebRequest();

        var data = JsonUtility.FromJson<AuthStartDto>(req.downloadHandler.text);

        _sessionId = data.sessionId;
        Debug.Log(_sessionId);
        Application.OpenURL(data.url);

        StartCoroutine(PollAuth());
    }

    public IEnumerator PollAuth()
    {
        while (true)
        {
            var req = UnityWebRequest.Get(
                $"https://localhost:7253/discord-login/status?sessionId={_sessionId}"
                );

            yield return req.SendWebRequest();

            var status = JsonUtility.FromJson<AuthSatausDto>(
                req.downloadHandler.text
                );

            if (status.completed)
            {
                Debug.Log(status.user.ToJson());
                OnAuthComplete(this, new AuthEventArgs() { userModel = status.user });
                OnAuth?.Invoke(status.user);
                yield break;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void HandleToken(string jwt)
    {
        UnityEngine.Debug.Log($"Handle Token Called, JWT Received: {jwt}");

        //PlayerPrefs.SetString("auth_token", jwt);
    }

}
