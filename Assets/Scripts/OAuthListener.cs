using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OAuthListener : MonoBehaviour
{

    public static OAuthListener Instance {  get; private set; }

    private HttpListener listener;
    private bool isRunning;

    public event Action<string> OnTokenReceived;

    private const string CALLBACK_URL = "https://localhost:7253/callback/";

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartListening()
    {
        if (isRunning) return;

        listener = new HttpListener();
        listener.Prefixes.Add(CALLBACK_URL);
        listener.Start();

        isRunning = true;
        _ = ListenAsync();
    }

    private async Task ListenAsync()
    {
        try
        {
            var context = await listener.GetContextAsync();
            var request = context.Request;

            string token = request.QueryString["token"];

            byte[] responseBytes = Encoding.UTF8.GetBytes(
                "<html><body>You can colse this window.</body></html>");

            context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            context.Response.Close();

            StopListening();

            OnTokenReceived?.Invoke(token);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void StopListening()
    {
        if(isRunning) return;

        listener.Stop();
        listener.Close();
        isRunning = false;
    }

    private void OnApplicationQuit()
    {
        StopListening();
    }

}
