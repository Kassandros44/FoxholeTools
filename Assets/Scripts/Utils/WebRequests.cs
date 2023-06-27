using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

namespace FoxholeTools.Utils{

    public static class WebRequests
    {

        private class WebRequestsMonoBehaviour : MonoBehaviour{}
        private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;
        private static void Init(){
            if(webRequestsMonoBehaviour == null){
                GameObject gameObject = new GameObject("WebRequests");
                webRequestsMonoBehaviour = gameObject.AddComponent<WebRequestsMonoBehaviour>();
            }
        }

        public static void Get(string url, Action<string> onError, Action<string> onReceived){
            Init();
            webRequestsMonoBehaviour.StartCoroutine(GetRequestEnumerator(url, onError, onReceived));
        }

        private static IEnumerator GetRequestEnumerator(string url, Action<string> onError, Action<string> onReceived){
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url)){
                yield return webRequest.SendWebRequest();
                if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError){
                    Debug.Log(webRequest.error);

                }else{
                    onReceived?.Invoke(webRequest.downloadHandler.text);
                }
            }
        }

        public static void Put(string url, object obj, Action<string> onError, Action<string> onReceived){
            string json = JObject.FromObject(obj).ToString();
            Debug.Log(json);
            Init();
            webRequestsMonoBehaviour.StartCoroutine(PutRequestEnumerator(url, json));
        }

        private static IEnumerator PutRequestEnumerator(string url, string data){
            using(UnityWebRequest webRequest = UnityWebRequest.Put(url, data)){
                webRequest.SetRequestHeader("Content-Type", "application/json");
                yield return webRequest.SendWebRequest();
                if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError){
                    Debug.Log(webRequest.error);
                }else{
                    Debug.Log(webRequest.result);
                }
            }
        }
    }
}
